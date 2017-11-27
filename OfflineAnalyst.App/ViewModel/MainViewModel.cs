using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;
using System;
using System.Collections.ObjectModel;
using SciChart.Data.Model;
using SciChart.Charting.Model.ChartSeries;
using SciChart.Charting.Model.DataSeries;

namespace OfflineAnalyst.App.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            NewChartCommand = new RelayCommand(OpenChartWindow);

          //  MovingAverage sma50 = new MovingAverage(3);
            var ds1 = new XyDataSeries<DateTime, double> { SeriesName = "3-Period SMA" };

            //ds1.Append(PriceData.XValues.Select(x => x), PriceData.CloseValues.Select(y => sma50.Push(y).Current));


            RenderableSeriesViewModels = new ObservableCollection<BaseRenderableSeriesViewModel>();

            //  RenderableSeriesViewModels.Add( new OhlcRenderableSeriesViewModel() {DataSeries = PriceData });
            RenderableSeriesViewModels.Add(new CandlestickRenderableSeriesViewModel() { DataSeries = PriceData });
           // RenderableSeriesViewModels.Add(new LineRenderableSeriesViewModel() { DataSeries = ds1 });
        }

        public ICommand NewChartCommand { get; set; }

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

        public string StatusBarLeft
        {
            get { return "Left part"; }
        }

        private string _rightStatusText;
        public string StatusBarRight
        {
            get { return _rightStatusText; }
            set
            {
                if (_rightStatusText != value)
                {
                    _rightStatusText = value;
                    RaisePropertyChanged();
                }
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

        private IndexRange _xVisibleRange;
        public IndexRange XVisibleRange
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

        private DoubleRange _yVisibleRange;
        public DoubleRange YVisibleRange
        {
            get { return _yVisibleRange; }
            set
            {
                if (!Equals(_yVisibleRange, value))
                {
                    _yVisibleRange = value;
                    RaisePropertyChanged();
                }
            }
        }

        private ObservableCollection<BaseRenderableSeriesViewModel> _seriesViewModels;

        public ObservableCollection<BaseRenderableSeriesViewModel> RenderableSeriesViewModels
        {
            get { return _seriesViewModels; }
            set
            {
                _seriesViewModels = value;
                RaisePropertyChanged();
            }
        }

        private void OpenChartWindow()
        {
            
        }
    }
}