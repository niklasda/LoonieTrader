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
using LoonieTrader.Library.HistoricalData;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.Models;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.App.ViewModels.Parts
{
    [UsedImplicitly]
    public class ChartPartViewModel : ViewModelBase
    {
        public ChartPartViewModel(IMapper mapper, ISettings settings, IPricingStreamingRequester priceStreamer)
        {
            if (IsInDesignMode)
            {
            }
            else
            {
                var strm = priceStreamer.GetPriceStream(settings.DefaultAccountId, "EUR_USD");
                strm.NewValue += Strm_NewPrice;
            }

            _dateOffset = DateTime.Now;

            XFormatter = XAxisLabelFormatter;
            YFormatter = YAxisLabelFormatter;

            var dayConfig = Mappers.Financial<CandleDataViewModel>()
                .X(dayModel => (double)dayModel.DatePlusTime.Subtract(_dateOffset).TotalSeconds)
               // .Y(dayModel => (double)dayModel.Open)
               .Open(dayModel => (double)dayModel.Open)
               .High(dayModel => (double)dayModel.High)
               .Low(dayModel => (double)dayModel.Low)
               .Close(dayModel => (double)dayModel.Close);

            var ohlc = new OhlcSeries()
            {
                Values = new ChartValues<CandleDataViewModel>
                {
                    new CandleDataViewModel {Open= 1.11m, High=1.13m, Low=1.10m, Close= 1.12m, Date = DateTime.Now.ToString("yyyyMMdd"), Time = DateTime.Now.AddSeconds(-50).ToString("HHmmss")},
                    new CandleDataViewModel {Open= 1.12m, High=1.14m, Low=1.11m, Close= 1.13m, Date = DateTime.Now.ToString("yyyyMMdd"), Time = DateTime.Now.AddSeconds(-40).ToString("HHmmss")},
                    new CandleDataViewModel {Open= 1.13m, High=1.15m, Low=1.12m, Close= 1.14m, Date = DateTime.Now.ToString("yyyyMMdd"), Time = DateTime.Now.AddSeconds(-30).ToString("HHmmss")},
                    new CandleDataViewModel {Open= 1.14m, High=1.16m, Low=1.13m, Close= 1.15m, Date = DateTime.Now.ToString("yyyyMMdd"), Time = DateTime.Now.AddSeconds(-20).ToString("HHmmss")},
                    new CandleDataViewModel {Open= 1.15m, High=1.17m, Low=1.14m, Close= 1.16m, Date = DateTime.Now.ToString("yyyyMMdd"), Time = DateTime.Now.AddSeconds(-10).ToString("HHmmss")}
                }
            };

            var lsMapper = Mappers.Xy<CandleDataViewModel>().Y(v => (double) v.Open).X(v => v.DatePlusTime.Subtract(_dateOffset).TotalSeconds);
            var ls = new LineSeries(lsMapper)
            {
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

        public InstrumentViewModel Instrument { get; set; }

        public ICommand UpdateCommand { get; set; }
        public SeriesCollection SeriesCollection { get; set; }

        public Func<double, string> XFormatter { get; private set; }
        //public Func<double, string> XFormatterUnder { get; private set; }
        public Func<double, string> YFormatter { get; private set; }

        private void AddPoint(PricesResponse.Price price)
        {
            _uiContext.Post(o =>
            {
                if (price.asks?.Length > 0)
                {
                    double ask = double.Parse(price.asks[0].price, CultureInfo.CurrentUICulture);
                    var p = new CandleDataViewModel
                    {
                        Open = (decimal)ask,
                        High = (decimal)(ask + 0.02),
                        Low = (decimal)(ask - 0.01),
                        Close = (decimal)(ask + 0.01),
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

            foreach (var point in SeriesCollection[0].Values.Cast<CandleDataViewModel>())
            {
                point.Open = r.Next((int)point.Low, (int)point.High);
                point.Close = r.Next((int)point.Low, (int)point.High);
            }

            var p = new CandleDataViewModel { Open = 1.2m, High = 1.5m, Low = 1.0m, Close = 1.2m, Date = DateTime.Now.ToString("yyyyMMdd"), Time = DateTime.Now.ToString("HHmmss") };
            SeriesCollection[0].Values.Add(p);
            SeriesCollection[1].Values.Add(p);
         //   Labels.Add(DateTime.Now.AddDays(Labels.Count).Ticks);
        }

    }
}