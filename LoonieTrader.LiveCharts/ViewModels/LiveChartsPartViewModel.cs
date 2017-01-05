using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using System.Windows.Media;
using AutoMapper;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using JetBrains.Annotations;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using LoonieTrader.Library.Constants;
using LoonieTrader.Library.Extensions;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.Models;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;
using LoonieTrader.Library.ViewModels;
using LoonieTrader.Shared.Models;

namespace LoonieTrader.LiveCharts.ViewModels
{
    [UsedImplicitly]
    public class LiveChartsPartViewModel : ChartBaseViewModel
    {
        public LiveChartsPartViewModel(IMapper mapper, ISettingsService settings, IPricingStreamingRequester priceStreamer)
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

            _dateOffset = DateTime.Now;

            XFormatter = XAxisLabelFormatter;
            YFormatter = YAxisLabelFormatter;

            var dayConfig = Mappers.Financial<CandleDataViewModel>()
                .X(dayModel => dayModel.DatePlusTime.Subtract(_dateOffset).TotalSeconds)
               // .Y(dayModel => (double)dayModel.Open)
               .Open(dayModel => (double)dayModel.Open)
               .High(dayModel => (double)dayModel.High)
               .Low(dayModel => (double)dayModel.Low)
               .Close(dayModel => (double)dayModel.Close);

            var ohlc = new OhlcSeries()
            {
                Title = "Instrument #0",
                Values = new ChartValues<CandleDataViewModel>
                {
                    new CandleDataViewModel {Open= 1.11, High=1.13, Low=1.10, Close= 1.12, Date = DateTime.Now.ToString("yyyyMMdd"), Time = DateTime.Now.AddSeconds(-50).ToString("HHmmss")},
                    new CandleDataViewModel {Open= 1.12, High=1.14, Low=1.11, Close= 1.13, Date = DateTime.Now.ToString("yyyyMMdd"), Time = DateTime.Now.AddSeconds(-40).ToString("HHmmss")},
                    new CandleDataViewModel {Open= 1.13, High=1.15, Low=1.12, Close= 1.14, Date = DateTime.Now.ToString("yyyyMMdd"), Time = DateTime.Now.AddSeconds(-30).ToString("HHmmss")},
                    new CandleDataViewModel {Open= 1.14, High=1.16, Low=1.13, Close= 1.15, Date = DateTime.Now.ToString("yyyyMMdd"), Time = DateTime.Now.AddSeconds(-20).ToString("HHmmss")},
                    new CandleDataViewModel {Open= 1.15, High=1.17, Low=1.14, Close= 1.16, Date = DateTime.Now.ToString("yyyyMMdd"), Time = DateTime.Now.AddSeconds(-10).ToString("HHmmss")}
                }
            };

            var lsMapper = Mappers.Xy<CandleDataViewModel>().Y(v => (double)v.Open).X(v => v.DatePlusTime.Subtract(_dateOffset).TotalSeconds);
            var ls = new LineSeries(lsMapper)
            {
                Title = "Line #1",
                Values = new ChartValues<CandleDataViewModel>(),
                Fill = Brushes.Transparent
            };

            ls.Values.Clear();
            foreach (CandleDataViewModel ohlcValue in ohlc.Values)
            {
                ls.Values.Add(ohlcValue);
            }

            SeriesCollection = new SeriesCollection(dayConfig)
            {
                ohlc,
                ls
            };

            UpdateCommand = new RelayCommand(UpdateAllOnClick);
        }

        private readonly IMapper _mapper;
        private readonly DateTime _dateOffset;
        private readonly SynchronizationContext _uiContext = SynchronizationContext.Current;

        private string YAxisLabelFormatter(double val)
        {
            var valLbl = val.ToString("F5");
            Console.WriteLine($@"Y: {val} - {valLbl}");
            return valLbl;
        }

        private string XAxisLabelFormatter(double val)
        {
            var valLbl = _dateOffset.AddSeconds(val).ToString("yyyy-MM-dd HH:mm:ss ");
            Console.WriteLine($@"X: {val} -> {valLbl}");
            return valLbl;
        }

        private void Strm_NewPrice(object sender, StreamEventArgs<PricesResponse.Price> e)
        {
            Console.WriteLine(@"addPoint: {0}", e.Obj);
            AddPoint(e.Obj);
        }

        //private InstrumentViewModel _instrument;
        //public InstrumentViewModel Instrument {
        //    get { return _instrument;}
        //    set
        //    {
        //        if (_instrument != value)
        //        {
        //            _instrument = value;
        //            RaisePropertyChanged();
        //        }
        //    }
        //}

        public ICommand UpdateCommand { get; set; }
        public SeriesCollection SeriesCollection { get; set; }

        public Func<double, string> XFormatter { get; private set; }
        public Func<double, string> YFormatter { get; private set; }

        //public void AddIndicator(Func<OhlciPoint, double> ohlcMapper)
        //{
        //    Func<CandleDataViewModel, double> candleMapper = v => ohlcMapper(_mapper.Map<OhlciPoint>(v));

        //    var lsMapper = Mappers.Xy<CandleDataViewModel>().Y(candleMapper).X(v => v.DatePlusTime.Subtract(_dateOffset).TotalSeconds);

        //    int c = SeriesCollection.Count;

        //    var ls = new LineSeries(lsMapper)
        //    {
        //        Title = $"Indi #{c}",
        //        Values = new ChartValues<CandleDataViewModel>(),
        //        Fill = Brushes.Transparent
        //    };


        //    foreach(var v in SeriesCollection[0].Values)
        //    {
        //        ls.Values.Add(v);
        //    }

        //    SeriesCollection.Add(ls);
        //}

        private void AddPoint(PricesResponse.Price price)
        {
            // needed to parse price string since they use us separators
            // var c = CultureInfo.GetCultureInfo("en-US");
            var serverCulture = AppProperties.ServerCulture;

            _uiContext.Post(o =>
            {
                if (price.asks?.Length > 0)
                {
                    double ask = double.Parse(price.asks[0].price, serverCulture);
                    var p = new CandleDataViewModel
                    {
                        Open = ask,
                        High = (ask + 0.02),
                        Low = (ask - 0.01),
                        Close = (ask + 0.01),
                        Date = DateTime.Now.ToString("yyyyMMdd"),
                        Time = DateTime.Now.ToString("HHmmss")
                    };

                    SeriesCollection[0].Values.Add(p);
                    SeriesCollection[1].Values.Add(p);
                    //  Labels.Add(DateTime.Now.AddDays(Labels.Count).Ticks);
                }
            }, null);

        }

        private void UpdateAllOnClick()
        {
            var r = new Random();

            double seriesLow = 1000;
            double seriesHigh = -1;

            foreach (var point in SeriesCollection[0].Values.Cast<CandleDataViewModel>())
            {
                seriesLow = Math.Min(seriesLow, point.Low);
                seriesHigh = Math.Max(seriesHigh, point.High);

                point.Open = r.NextDouble(point.Low, point.High);
                point.Close = r.NextDouble(point.Low, point.High);
            }

            var p = new CandleDataViewModel { Open = r.NextDouble(seriesLow, seriesHigh), High = seriesHigh, Low = seriesLow, Close = r.NextDouble(seriesLow, seriesHigh), Date = DateTime.Now.ToString("yyyyMMdd"), Time = DateTime.Now.ToString("HHmmss") };
            SeriesCollection[0].Values.Add(p);
            SeriesCollection[1].Values.Add(p);
            //   Labels.Add(DateTime.Now.AddDays(Labels.Count).Ticks);
        }

    }
}