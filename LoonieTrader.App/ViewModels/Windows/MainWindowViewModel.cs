using System.Collections.Generic;
using System.Windows;
using AutoMapper;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using LoonieTrader.App.Windows;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.Models.Responses;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace LoonieTrader.App.ViewModels.Windows
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(ISettings settings, IMapper mapper, IAccountsRequester accountsRequester)
        {
            _settings = settings;
            _mapper = mapper;
            _accountsRequester = accountsRequester;

            AboutCommand = new RelayCommand(About);
            TradeTicketCommand = new RelayCommand(TradeTicket);

            if (IsInDesignMode)
            {
                var plotModel = new PlotModel {Title = "Sample 1", Subtitle = "Graph"};
                plotModel.Axes.Add(new LinearAxis {Position = AxisPosition.Left, Minimum = -1, Maximum = 10});
                plotModel.Axes.Add(new LinearAxis {Position = AxisPosition.Bottom, Minimum = -1, Maximum = 10});

                var lineSeries = new LineSeries { LineStyle = LineStyle.Solid };
                lineSeries.Points.AddRange(new[] { new DataPoint(1, 2), new DataPoint(2, 3) });

                plotModel.Series.Add(lineSeries);

                GraphData = plotModel;
            }
            else
            {
                 var plotModel = new PlotModel {Title = "Example Live", Subtitle = "Graph"};
                plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = -1, Maximum = 10 });
                plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = -1, Maximum = 10 });

                var lineSeries = new LineSeries {LineStyle = LineStyle.Solid};
                lineSeries.Points.AddRange(new [] { new DataPoint(1, 2) , new DataPoint(2.1, 3) });

                plotModel.Series.Add(lineSeries);

                GraphData = plotModel;
            }

            AccountInstrumentsResponse im =_accountsRequester.GetInstruments(settings.DefaultAccountId);
            _instrumentList = mapper.Map<IList<InstrumentModel>>(im.instruments);
        }

        private readonly ISettings _settings;
        private readonly IMapper _mapper;
        private readonly IAccountsRequester _accountsRequester;
        private PlotModel _graphData;
        private IList<InstrumentModel> _instrumentList;

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
                return _instrumentList;
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

        private InstrumentModel _selectedItem;

        public object SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value as InstrumentModel; }
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