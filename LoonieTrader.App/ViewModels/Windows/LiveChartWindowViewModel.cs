using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using AutoMapper;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;

namespace LoonieTrader.App.ViewModels.Windows
{
    public class LiveChartWindowViewModel : ViewModelBase
    {
        public LiveChartWindowViewModel(IMapper mapper, ISettings settings, IPricingRequester pricingRequester, IOrdersRequester orderRequester, IExtendedLogger logger)
        {
            _mapper = mapper;
            _settings = settings;
            _pricingRequester = pricingRequester;
            _orderRequester = orderRequester;
            _logger = logger;

            SeriesCollection = new SeriesCollection
            {
                new OhlcSeries()
                {
                    Values = new ChartValues<OhlcPoint>
                    {
                        new OhlcPoint(32, 35, 30, 32),
                        new OhlcPoint(33, 38, 31, 37),
                        new OhlcPoint(35, 42, 30, 40),
                        new OhlcPoint(37, 40, 35, 38),
                        new OhlcPoint(35, 38, 32, 33)
                    }
                },
                new LineSeries
                {
                    Values = new ChartValues<double> {30, 32, 35, 30},
                    Fill = Brushes.Transparent
                }
            };

            Labels = new List<string>()
           {
                DateTime.Now.ToString("dd MMM"),
                DateTime.Now.AddDays(1).ToString("dd MMM"),
                DateTime.Now.AddDays(2).ToString("dd MMM"),
                DateTime.Now.AddDays(3).ToString("dd MMM"),
                DateTime.Now.AddDays(4).ToString("dd MMM"),
            };

            Update = new RelayCommand(UpdateAllOnClick);
        }

        public RelayCommand BuyCommand { get; set; }
        public RelayCommand SellCommand { get; set; }
        //public RelayCommand IsTrailingToggleCommand { get; set; }
        // public RelayCommand IsGtcToggleCommand { get; set; }
        // public RelayCommand IsMarketToggleCommand { get; set; }

        private readonly IMapper _mapper;
        private readonly ISettings _settings;
        private readonly IPricingRequester _pricingRequester;
        private readonly IOrdersRequester _orderRequester;
        private readonly IExtendedLogger _logger;

        //private IList<InstrumentViewModel> _allInstruments;
        private List<string> _labels;
        

        public SeriesCollection SeriesCollection { get; set; }

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

        public RelayCommand Update { get; set; }

        private void UpdateAllOnClick()
        {
            var r = new Random();

            foreach (var point in SeriesCollection[0].Values.Cast<OhlcPoint>())
            {
                point.Open = r.Next((int)point.Low, (int)point.High);
                point.Close = r.Next((int)point.Low, (int)point.High);
            }

            SeriesCollection[0].Values.Add(new OhlcPoint(32, 35, 30, 32));
            Labels.Add(   DateTime.Now.AddDays(Labels.Count).ToString("dd MMM"));
        }
    }
}