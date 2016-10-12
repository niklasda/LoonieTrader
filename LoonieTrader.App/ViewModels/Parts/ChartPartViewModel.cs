using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using System.Windows.Media;
using AutoMapper;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Wpf;
using LoonieTrader.Library.HistoricalData;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.App.ViewModels.Parts
{
    public class ChartPartViewModel : ViewModelBase
    {
        public ChartPartViewModel(IMapper mapper, ISettings settings, IPricingStreamingRequester priceStreamer)
        {
            if (IsInDesignMode)
            {
            }
            else
            {
                IObservable<PricesResponse.Price> strm = priceStreamer.GetPriceStream(settings.DefaultAccountId, "EUR_USD");
                var sub = strm.Subscribe(x => AddPoint(x));
            }

            var dayConfig = Mappers.Financial<CandleDataViewModel>()
              // .X(dayModel => (double)dayModel.DatePlusTime.Ticks / TimeSpan.FromHours(1).Ticks)
               .Open(dayModel => (double)dayModel.Open)
               .High(dayModel => (double)dayModel.High)
               .Low(dayModel => (double)dayModel.Low)
               .Close(dayModel => (double)dayModel.Close);

            var ohlc = new OhlcSeries()
            {
                Values = new ChartValues<CandleDataViewModel>
                {
                    new CandleDataViewModel {Open= 1.11m,High= 1.13m, Low=1.10m,Close= 1.12m,Date = DateTime.Now.ToString("yyyyMMdd"),Time = DateTime.Now.ToString("HHmmss")},
                    new CandleDataViewModel {Open= 1.11m, High=1.13m, Low=1.10m,Close= 1.12m,Date = DateTime.Now.ToString("yyyyMMdd"),Time = DateTime.Now.ToString("HHmmss")}
                }
            };

            
            var ls = new LineSeries(Mappers.Xy<CandleDataViewModel>().Y(v => (double) v.Open))
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

            Labels = new List<string>()
            {
                DateTime.Now.ToString("dd MMM"),
                DateTime.Now.AddDays(1).ToString("dd MMM"),
            };

            UpdateCommand = new RelayCommand(UpdateAllOnClick);
        }

        //public ObservableCollection<CandleDataViewModel> GraphData { get; set; }
        public InstrumentViewModel Instrument { get; set; }

        public ICommand UpdateCommand { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        private List<string> _labels;

        private SynchronizationContext _uiContext = SynchronizationContext.Current;

        private void AddPoint(PricesResponse.Price price)
        {
            _uiContext.Post(o =>
            {
                if (price.asks?.Length > 0)
                {
                    double ask = double.Parse(price.asks[0].price, CultureInfo.CurrentUICulture);
                    var p = new CandleDataViewModel { Open=(decimal) ask, High=(decimal) (ask + 0.02), Low=(decimal) (ask - 0.01),Close= (decimal) (ask + 0.01),
                        Date = DateTime.Now.ToString("yyyyMMdd"), Time = DateTime.Now.ToString("HHmmss") };
                    SeriesCollection[0].Values.Add(p);
                    SeriesCollection[1].Values.Add(p);
                    Labels.Add(DateTime.Now.AddDays(Labels.Count).ToString("dd MMM"));
                }
            }, null);

            //System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke((Action)(() =>
            //{
            //    //your code here...
            //double ask = double.Parse(price.asks[0].price, CultureInfo.CurrentUICulture);
            //    var p = new OhlcPoint(ask, ask+2, ask-1, ask+1);
            //SeriesCollection[0].Values.Add(p);
            //SeriesCollection[1].Values.Add(p);
            //Labels.Add(DateTime.Now.AddDays(Labels.Count).ToString("dd MMM"));
            //}));
        }

        private void UpdateAllOnClick()
        {
            var r = new Random();

            foreach (var point in SeriesCollection[0].Values.Cast<CandleDataViewModel>())
            {
                point.Open = r.Next((int)point.Low, (int)point.High);
                point.Close = r.Next((int)point.Low, (int)point.High);
            }

            var p = new CandleDataViewModel { Open=1.2m, High=1.5m, Low=1.0m, Close=1.2m, Date = DateTime.Now.ToString("yyyyMMdd"), Time = DateTime.Now.ToString("HHmmss") };
            SeriesCollection[0].Values.Add(p);
            SeriesCollection[1].Values.Add(p);
            Labels.Add(DateTime.Now.AddDays(Labels.Count).ToString("dd MMM"));
        }

        public List<string> Labels
        {
            get { return _labels; }
            set
            {
                _labels = value;
                RaisePropertyChanged();
                //       OnPropertyChanged("Labels");
            }
        }
    }
}