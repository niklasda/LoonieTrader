using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Oanda.App.Windows;
using OxyPlot;

namespace Oanda.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            AboutCommand = new RelayCommand(About);

            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }

        private PlotModel _graphData;

        public PlotModel GraphData
        {
            get { return _graphData; }
            set
            {
                if (_graphData != value)
                {
                    _graphData = value;
                    RaisePropertyChanged();
                }
            }
        }

        public RelayCommand AboutCommand { get; set; }

        public void About()
        {
            AboutWindow mw = new AboutWindow();
            mw.Show();
        }
    }
}