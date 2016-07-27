using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Oanda.App.Windows;
using Oanda.RestLibrary.Responses;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace Oanda.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
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

        public IList<ListViewItem> InstrumentList
        {
            get
            {
                return new[]
                {
                    new ListViewItem() {Content = "EURUSD",Background = Brushes.GreenYellow},
                    new ListViewItem() {Content = "EUR_USD"},
                    new ListViewItem() {Content = "EUR/USD"},
                    new ListViewItem() {Content = "EUR.USD"},
                    new ListViewItem() {Content = "Brent Crude Oil"}
                };
            }
        }

        public IList<Position> SomeDataTable
        {
            /*
             * *  AutoMapper
             * */
            get { return new[]
            {
                new Position() {instrument = "EURUSD", pl = "1.22" },
                new Position() {instrument = "EURUSD", pl = "1.22" },
                new Position() {instrument = "EURUSD", pl = "1.22" },
                new Position() {instrument = "EURUSD", pl = "1.22" },
                new Position() {instrument = "EURUSD", pl = "1.22" },
                new Position() {instrument = "EURUSD", pl = "1.22" },
                new Position() {instrument = "EURUSD", pl = "1.22" },
                new Position() {instrument = "EURUSD", pl = "1.22" },
                new Position() {instrument = "EURUSD", pl = "1.22" },
                new Position() {instrument = "EURUSD", pl = "1.22" },
                new Position() {instrument = "EURUSD", pl = "1.22" },
                new Position() {instrument = "EURUSD", pl = "1.22" },
                new Position() {instrument = "EURUSD", pl = "1.22" },
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