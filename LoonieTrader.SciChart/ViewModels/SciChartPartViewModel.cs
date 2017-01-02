using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using AutoMapper;
using JetBrains.Annotations;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.Models;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;
using LoonieTrader.Library.ViewModels;
using SciChart.Charting.Model.DataSeries;
using SciChart.Data.Model;

namespace LoonieTrader.SciChart.ViewModels
{
    [UsedImplicitly]
    public class SciChartPartViewModel : ChartBaseViewModel
    {
        public SciChartPartViewModel(IMapper mapper, ISettingsService settings, IPricingStreamingRequester priceStreamer)
        {
            _mapper = mapper;
            if (IsInDesignMode)
            {
            }
            else
            {
                var cfg = settings.CachedSettings.SelectedEnvironment;
                var strm = priceStreamer.GetPriceStream(cfg.DefaultAccountId, "EUR_USD");
                strm.NewValue += Strm_NewPrice;
            }

            Instrument instrument = ViewModels.Instrument.EurUsd;
            TimeFrame timeFrame = TimeFrame.Daily;

            PriceSeries priceData = DataManager.Instance.GetPriceData(instrument.Symbol, timeFrame);

            this.PriceData = new OhlcDataSeries<DateTime, double>();
            PriceData.SeriesName = priceData.Symbol;
            PriceData.Append(priceData.TimeData, priceData.OpenData, priceData.HighData, priceData.LowData, priceData.CloseData);

            PriceData.InvalidateParentSurface(RangeMode.ZoomToFit);

        }

        private readonly IMapper _mapper;
        private readonly SynchronizationContext _uiContext = SynchronizationContext.Current;

        public ICommand UpdateCommand { get; set; }

        private IOhlcDataSeries<DateTime, double> _priceSeries;

        public IOhlcDataSeries<DateTime, double> PriceData
        {
            get { return _priceSeries; }
            set
            {
                _priceSeries = value;
                RaisePropertyChanged();
            }
        }

        public string XAxisTitle
        {
            get { return "Time"; }
        }

        public string YAxisTitle
        {
            get { return "Price"; }
        }

        private DoubleRange _xVisibleRange;
        public DoubleRange XVisibleRange
        {
            get { return _xVisibleRange; }
            set
            {
                if (!Equals(_xVisibleRange, value))
                {
                    _xVisibleRange = value;
                    RaisePropertyChanged();
                }
            }
        }

        private void Strm_NewPrice(object sender, StreamEventArgs<PricesResponse.Price> e)
        {
            Console.WriteLine(@"addPoint: {0}", e.Obj);
            AddPoint(e.Obj);
        }

        private void AddPoint(PricesResponse.Price price)
        {
            // needed to parse price string since they use us separators
            var c = CultureInfo.GetCultureInfo("en-US");

            _uiContext.Post(o =>
            {
                var bps =   PriceData;// as BoxPlotDataSeries<double, double>;

                if (price.asks?.Length > 0)
                {
                    double ask = double.Parse(price.asks[0].price, c);
                    var p = new CandleDataViewModel
                    {
                        Open = ask,
                        High = (ask + 0.02),
                        Low = (ask - 0.01),
                        Close = (ask + 0.01),
                        Date = DateTime.Now.ToString("yyyyMMdd"),
                        Time = DateTime.Now.ToString("HHmmss")
                    };

                    using (bps.SuspendUpdates())
                    {
                        var x = DateTime.Now;
                        bps.Append(x, p.Open, p.High, p.Low, p.Close);

                    }
                }
            }, null);
        }

    }

    public class PriceSeries : List<CandleDataViewModel>
    {
        public string Symbol { get; set; }

        public PriceSeries()
        {
        }

        public PriceSeries(int capacity) : base(capacity)
        {
        }

        /// <summary>
        /// Extracts the DateTime column of the PriceSeries as an array
        /// </summary>
        public IList<DateTime> TimeData { get { return this.Select(x => x.DatePlusTime).ToArray(); } }

        /// <summary>
        /// Extracts the Open column of the PriceSeries as an array
        /// </summary>
        public IList<double> OpenData { get { return this.Select(x => x.Open).ToArray(); } }

        /// <summary>
        /// Extracts the High column of the PriceSeries as an array
        /// </summary>
        public IList<double> HighData { get { return this.Select(x => x.High).ToArray(); } }

        /// <summary>
        /// Extracts the Low column of the PriceSeries as an array
        /// </summary>
        public IList<double> LowData { get { return this.Select(x => x.Low).ToArray(); } }

        /// <summary>
        /// Extracts the Close column of the PriceSeries as an array
        /// </summary>
        public IList<double> CloseData { get { return this.Select(x => x.Close).ToArray(); } }

        /// <summary>
        /// Extracts the Volume column of the PriceSeries as an array
        /// </summary>
        public IList<long> VolumeData { get { return this.Select(x => x.Volume).ToArray(); } }

    }

    public class TimeFrame 
    {
        private TimeFrame(string value, string displayname) //: base(value)
        {
            Displayname = displayname;
            _value = value;
        }

        private readonly string _value;
        public static readonly TimeFrame Daily = new TimeFrame("Daily", "Daily");
        public static readonly TimeFrame Hourly = new TimeFrame("Hourly", "1 Hour");
        public static readonly TimeFrame Minute15 = new TimeFrame("Minute15", "15 Minutes");

        public string Displayname { get; private set; }

        public override string ToString()
        {
            return _value.ToString();
        }
    }

    public class Instrument
    {
        private readonly string _value;

        public string InstrumentName { get; private set; }
        public string Symbol { get { return _value; } }
        public int DecimalPlaces { get; private set; }

        public Instrument(string value, string instrumentName, int decimalPlaces) //: base(value)
        {
            _value = value;
            InstrumentName = instrumentName;
            DecimalPlaces = decimalPlaces;
        }

        public static readonly Instrument EurUsd = new Instrument("EURUSD", "FX Euro US Dollar", 4);
        public static readonly Instrument Indu = new Instrument("INDU", "Dow Jones Industrial Average", 0);
        public static readonly Instrument Spx500 = new Instrument("SPX500", "S&P500 Index", 0);
        public static readonly Instrument CrudeOil = new Instrument("CL", "Light Crude Oil", 0);
        public static readonly Instrument Test = new Instrument("TEST", "Test data only", 0);


        public override string ToString()
        {
            return _value.ToString();
        }
    }

    public class DataManager
    {
        private static readonly DataManager _instance = new DataManager();

        public static DataManager Instance
        {
            get { return _instance; }
        }

        public PriceSeries GetPriceData(string dataset, TimeFrame timeFrame)
        {
            return GetPriceData(string.Format("{0}_{1}", dataset, timeFrame));
        }

        private PriceSeries GetPriceData(string dataset)
        {
            var priceSeries = new PriceSeries();
            priceSeries.Symbol = dataset;

            string prices = "2009.07.14,1.41661,1.42816,1.41148,1.41411,251619,0" + Environment.NewLine +
                            "2010.07.15,1.41416,1.41984,1.40917,1.41533,185183,0" + Environment.NewLine +
                            "2010.07.18,1.411,1.41333,1.40134,1.41113,185742,0" + Environment.NewLine +
                            "2011.07.19,1.41106,1.42169,1.40679,1.41543,197040,0" + Environment.NewLine +
                            "2011.07.20,1.41543,1.42389,1.41324,1.42142,171481,0" + Environment.NewLine +
                            "2012.07.21,1.42142,1.44338,1.41383,1.44233,224940,0" + Environment.NewLine +
                            "2012.07.22,1.4424,1.44371,1.43232,1.43553,179866,0" + Environment.NewLine +
                            "2013.07.25,1.43867,1.44161,1.43244,1.4377,165474,0" + Environment.NewLine +
                            "2013.07.26,1.43763,1.45249,1.43565,1.45098,182901,0" + Environment.NewLine +
                            "2014.07.27,1.45101,1.45353,1.43378,1.43678,178612,-1" + Environment.NewLine +
                            "2014.07.28,1.4368,1.43999,1.42531,1.43326,179569,0" + Environment.NewLine +
                            "2015.07.29,1.43325,1.44126,1.42289,1.43975,211814,0" + Environment.NewLine +
                            "2015.08.01,1.43798,1.4453,1.41841,1.42492,203152,0" + Environment.NewLine +
                            "2016.08.02,1.42491,1.42822,1.41503,1.42018,228836,0" + Environment.NewLine +
                            "2016.08.03,1.42016,1.43437,1.41425,1.43206,207584,0";

            using (var streamReader = new StringReader(prices))
            {
                string line = streamReader.ReadLine();
                while (line != null)
                {
                    var tokens = line.Split(',');

                    CandleDataViewModel candle = new CandleDataViewModel();
                    candle.DatePlusTime= DateTime.Parse(tokens[0], DateTimeFormatInfo.InvariantInfo);
                    candle.Open= double.Parse(tokens[1], NumberFormatInfo.InvariantInfo);
                    candle.High = double.Parse(tokens[2], NumberFormatInfo.InvariantInfo);
                    candle.Low = double.Parse(tokens[3], NumberFormatInfo.InvariantInfo);
                    candle.Close = double.Parse(tokens[4], NumberFormatInfo.InvariantInfo);
                    candle.Volume = long.Parse(tokens[5], NumberFormatInfo.InvariantInfo);
                    priceSeries.Add(candle);

                    line = streamReader.ReadLine();
                }
            }

            return priceSeries;
        }

    }
}