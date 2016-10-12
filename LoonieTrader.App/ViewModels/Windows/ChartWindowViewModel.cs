using AutoMapper;
using GalaSoft.MvvmLight;
using JetBrains.Annotations;
using LoonieTrader.App.ViewModels.Parts;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;

namespace LoonieTrader.App.ViewModels.Windows
{
    [UsedImplicitly]
    public class ChartWindowViewModel : ViewModelBase
    {
        public ChartWindowViewModel(IMapper mapper, ISettings settings, IPricingStreamingRequester priceStreamer, ChartPartViewModel chartPart)
        {
            if (IsInDesignMode)
            {
            }
            else
            {
             //   IObservable<PricesResponse.Price> strm = priceStreamer.GetPriceStream(settings.DefaultAccountId, "EUR_USD");
             //   var sub = strm.Subscribe(x => AddPoint(x));
            }

            ChartPart = chartPart;

           /* var ohlc = new OhlcSeries()
            {
                Values = new ChartValues<OhlcPoint>
                {
                    new OhlcPoint(3.2, 3.5, 3.0, 3.2),
                    new OhlcPoint(3.3, 3.8, 3.1, 3.7),
                    new OhlcPoint(3.5, 4.2, 3.0, 4.0),
                    new OhlcPoint(3.7, 4.0, 3.5, 3.8),
                    new OhlcPoint(3.5, 3.8, 3.2, 3.3)
                }
            };


            var ls = new LineSeries(Mappers.Xy<OhlcPoint>().Y(v => v.Open))
            {
                Values = new ChartValues<OhlcPoint>(),
                Fill = Brushes.Transparent
            };

            ls.Values.Clear();
            foreach (OhlcPoint ohlcValue in ohlc.Values)
            {
                ls.Values.Add(ohlcValue);
            }

            SeriesCollection = new SeriesCollection
            {
                ohlc,
                ls
            };

            Labels = new List<string>()
            {
                DateTime.Now.ToString("dd MMM"),
                DateTime.Now.AddDays(1).ToString("dd MMM"),
                DateTime.Now.AddDays(2).ToString("dd MMM"),
                DateTime.Now.AddDays(3).ToString("dd MMM"),
                DateTime.Now.AddDays(4).ToString("dd MMM"),
            };

            UpdateCommand = new RelayCommand(UpdateAllOnClick);*/
        }

        //public ObservableCollection<CandleDataViewModel> GraphData { get; set; }
        public InstrumentViewModel Instrument { get; set; }
        public ChartPartViewModel ChartPart { get; private set; } 

       // public ICommand UpdateCommand { get; set; }
        //public SeriesCollection SeriesCollection { get; set; }
        //private List<string> _labels;

      //  private SynchronizationContext _uiContext = SynchronizationContext.Current;

       /* private void AddPoint(PricesResponse.Price price)
        {
            _uiContext.Post(o =>
            {
                if (price.asks?.Length>0)
                {
                    double ask = double.Parse(price.asks[0].price, CultureInfo.CurrentUICulture);
                    var p = new OhlcPoint(ask, ask + 2, ask - 1, ask + 1);
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
        }*/

      /*  private void UpdateAllOnClick()
        {
            var r = new Random();

            foreach (var point in SeriesCollection[0].Values.Cast<OhlcPoint>())
            {
                point.Open = r.Next((int) point.Low, (int) point.High);
                point.Close = r.Next((int) point.Low, (int) point.High);
            }

            var p = new OhlcPoint(3.2, 3.5, 3.0, 3.2);
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
        }*/
    }
}