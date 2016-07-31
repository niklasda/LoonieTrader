using System.Collections.Generic;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LoonieTrader.App.Windows;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace LoonieTrader.App.ViewModels.Windows
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            AboutCommand = new RelayCommand(About);
            TradeTicketCommand = new RelayCommand(TradeTicket);

            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            if (IsInDesignMode)
            {
                var plotModel = new PlotModel {Title = "Sample 1", Subtitle = "Graph"};
                plotModel.Axes.Add(new LinearAxis {Position = AxisPosition.Left, Minimum = -1, Maximum = 10});
                plotModel.Series.Add(new LineSeries {LineStyle = LineStyle.Solid});

                GraphData = plotModel;
            }
            else
            {
                 var plotModel = new PlotModel {Title = "Example Live", Subtitle = "Graph"};
                plotModel.Axes.Add(new LinearAxis {Position = AxisPosition.Left, Minimum = -1, Maximum = 10});
                plotModel.Series.Add(new LineSeries {LineStyle = LineStyle.Solid});

                GraphData = plotModel;
            }
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

        public RelayCommand TradeTicketCommand { get; set; }

        public IList<InstrumentModel> InstrumentList
        {
            get
            {
                return new[]
                {
                    new InstrumentModel() {Instrument = "EURUSD"},
                    new InstrumentModel() {Instrument = "EUR_USD"},
                    new InstrumentModel() {Instrument = "EUR/USD"},
                    new InstrumentModel() {Instrument = "EUR.USD"},
                    new InstrumentModel() {Instrument = "Brent Crude Oil"}
                };
            }
        }

        public IList<PositionModel> SomeDataTable
        {
            /*
             * *  AutoMapper
             * */
            get { return new[]
            {
                new PositionModel() {Instrument = "EURUSD", ProfitLoss = "1.22" },
                new PositionModel() {Instrument = "EURUSD", ProfitLoss = "1.22" },
                new PositionModel() {Instrument = "EURUSD", ProfitLoss = "1.22" },
                new PositionModel() {Instrument = "EURUSD", ProfitLoss = "1.22" },
                new PositionModel() {Instrument = "EURUSD", ProfitLoss = "1.22" },
                new PositionModel() {Instrument = "EURUSD", ProfitLoss = "1.22" },
                new PositionModel() {Instrument = "EURUSD", ProfitLoss = "1.22" },
                new PositionModel() {Instrument = "EURUSD", ProfitLoss = "1.22" },
                new PositionModel() {Instrument = "EURUSD", ProfitLoss = "1.22" },
                new PositionModel() {Instrument = "EURUSD", ProfitLoss = "1.22" },
                new PositionModel() {Instrument = "EURUSD", ProfitLoss = "1.22" },
                new PositionModel() {Instrument = "EURUSD", ProfitLoss = "1.22" },
                new PositionModel() {Instrument = "EURUSD", ProfitLoss = "1.22" },
            }; }
        }

        public void About()
        {
            AboutWindow aw = new AboutWindow();
            aw.Owner = Application.Current.MainWindow;
            aw.ShowDialog();

        }

        public void TradeTicket()
        {
            TradeTicketWindow tw = new TradeTicketWindow();
            tw.Show();
        }
    }
}