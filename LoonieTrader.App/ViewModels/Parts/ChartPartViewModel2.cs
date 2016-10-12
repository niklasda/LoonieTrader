//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Threading;
//using System.Windows.Input;
//using System.Windows.Media;
//using AutoMapper;
//using GalaSoft.MvvmLight;
//using GalaSoft.MvvmLight.CommandWpf;
//using LiveCharts;
//using LiveCharts.Configurations;
//using LiveCharts.Defaults;
//using LiveCharts.Wpf;
//using LoonieTrader.Library.HistoricalData;
//using LoonieTrader.Library.Interfaces;
//using LoonieTrader.Library.RestApi.Interfaces;
//using LoonieTrader.Library.RestApi.Responses;

//namespace LoonieTrader.App.ViewModels.Parts
//{
//    public class ChartPartViewModel2 : ViewModelBase
//    {
//        public ChartPartViewModel2(IMapper mapper, ISettings settings, IPricingStreamingRequester priceStreamer)
//        {
//            if (IsInDesignMode)
//            {
//            }
//            else
//            {
//               // IObservable<PricesResponse.Price> strm = priceStreamer.GetPriceStream(settings.DefaultAccountId, "EUR_USD").Result;
//               // var sub = strm.Subscribe(x => AddPoint(x));
//            }

//            var dayConfig = Mappers.Financial<CandleDataViewModel>()
//                .X(dayModel => (double) dayModel.DatePlusTime.Ticks/TimeSpan.FromHours(1).Ticks)
//                .Open(dayModel => (double) dayModel.Open)
//                .High(dayModel => (double) dayModel.High)
//                .Low(dayModel => (double) dayModel.Low)
//                .Close(dayModel => (double) dayModel.Close);

//            SeriesCollection = new SeriesCollection(dayConfig);

//            Formatter = value => new DateTime((long) (value*TimeSpan.FromHours(1).Ticks)).ToString("s");

//            UpdateCommand = new RelayCommand(Update);

//            Update();
//        }

//        public SeriesCollection SeriesCollection { get; set; }

//        public List<string> Labels { get; set; }

//        public Func<double, string> Formatter { get; set; }

//        public ICommand UpdateCommand { get; set; }

//        private SynchronizationContext _uiContext = SynchronizationContext.Current;

//        private void AddPoint(PricesResponse.Price price)
//        {
//            _uiContext.Post(o =>
//            {
//                if (price.asks?.Length > 0)
//                {
//                    CandleDataViewModel p = new CandleDataViewModel();

//                  //  double ask = double.Parse(price.asks[0].price, CultureInfo.CurrentUICulture);
//                    decimal ask = decimal.Parse(price.asks[0].price, CultureInfo.CurrentUICulture);
//                    //var p = new OhlcPoint(ask, ask + 2, ask - 1, ask + 1);

//                    p.Open = ask;
//                    p.High = ask + 2;
//                    p.Low = ask - 1;
//                    p.Close = ask + 1;

//                    p.Date = DateTime.Now.ToString("yyyyMMdd");
//                    p.Time = DateTime.Now.ToString("HHmmss");

//                    SeriesCollection[0].Values.Add(p);
//                    SeriesCollection[1].Values.Add((double)ask);
//                    Labels.Add(DateTime.Now.AddDays(Labels.Count).ToString("dd MMM"));
//                }
//            }, null);

//            //System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke((Action)(() =>
//            //{
//            //    //your code here...
//            //double ask = double.Parse(price.asks[0].price, CultureInfo.CurrentUICulture);
//            //    var p = new OhlcPoint(ask, ask+2, ask-1, ask+1);
//            //SeriesCollection[0].Values.Add(p);
//            //SeriesCollection[1].Values.Add(p);
//            //Labels.Add(DateTime.Now.AddDays(Labels.Count).ToString("dd MMM"));
//            //}));
//        }

//        private void Update()
//        {
//            string instrument = "USD/SEK";

//            if (SeriesCollection.Count < 1)
//            {
//                SeriesCollection.Add(
//                    new OhlcSeries()
//                    {
//                        Title = string.Format("Reloaded {0}", instrument),
//                        IncreaseBrush = Brushes.Green,
//                        DecreaseBrush = Brushes.Red,
//                        Values = new ChartValues<CandleDataViewModel>
//                        {
//                            new CandleDataViewModel()
//                            {
//                                Open = 1.1m,
//                                High = 1.3m,
//                                Low = 1.0m,
//                                Close = 1.2m,
//                                Date = DateTime.Now.ToString("yyyyMMdd"),
//                                Time = DateTime.Now.ToString("HHmmss"),
//                                Ticker = instrument
//                            },
//                        }
//                    });

//                SeriesCollection.Add(
//                    new LineSeries
//                    {
//                        Title = "Line",
//                        Values = new ChartValues<double> { 1.0, 1.0, 1.0, 1.0 },
//                        Fill = Brushes.Transparent
//                    }
//                      );

//                Labels = new List<string>()
//                {
//                    DateTime.Now.ToString("dd MMM"),
//                    DateTime.Now.AddDays(1).ToString("dd MMM"),
//                    DateTime.Now.AddDays(2).ToString("dd MMM"),
//                    DateTime.Now.AddDays(3).ToString("dd MMM"),
//                    DateTime.Now.AddDays(4).ToString("dd MMM"),
//                };
//            }
//            else
//            {
//                SeriesCollection[0].Values.Add(new CandleDataViewModel()
//                {
//                    Open = 1.1m,
//                    High = 1.3m,
//                    Low = 1.0m,
//                    Close = 1.2m,
//                    Date = DateTime.Now.ToString("yyyyMMdd"),
//                    Time = DateTime.Now.ToString("HHmmss"),
//                    Ticker = instrument
//                });
//                Labels.Add("New");
//            }

//        }
//    }
//}