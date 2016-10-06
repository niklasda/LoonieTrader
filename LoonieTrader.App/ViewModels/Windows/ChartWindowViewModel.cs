using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LoonieTrader.Library.HistoricalData;

namespace LoonieTrader.App.ViewModels.Windows
{
    public class ChartWindowViewModel : ViewModelBase
    {
        public ChartWindowViewModel()
        {
            if (IsInDesignMode)
            {
            }
            else
            {
            }

            var ohlc = new OhlcSeries()
            {
                Values = new ChartValues<OhlcPoint>
                {
                    new OhlcPoint(32, 35, 30, 32),
                    new OhlcPoint(33, 38, 31, 37),
                    new OhlcPoint(35, 42, 30, 40),
                    new OhlcPoint(37, 40, 35, 38),
                    new OhlcPoint(35, 38, 32, 33)
                }
            };

            var mapper = Mappers.Xy<OhlcPoint>().Y(v => v.Open);
            var ls = new LineSeries(mapper)
            {
                Values = new ChartValues<OhlcPoint> {},
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

            UpdateCommand = new RelayCommand(UpdateAllOnClick);

        }

        //public ObservableCollection<CandleDataViewModel> GraphData { get; set; }
        public InstrumentViewModel Instrument { get; set; }

        public ICommand UpdateCommand { get; set; }
        public SeriesCollection SeriesCollection { get; set; }
        private List<string> _labels;

        private void UpdateAllOnClick()
        {
            var r = new Random();

            foreach (var point in SeriesCollection[0].Values.Cast<OhlcPoint>())
            {
                point.Open = r.Next((int)point.Low, (int)point.High);
                point.Close = r.Next((int)point.Low, (int)point.High);
            }

            var p = new OhlcPoint(32, 35, 30, 32);
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