using System.ComponentModel;
using Fluent;
using FontAwesome.WPF;
using LoonieTrader.App.Constants;
using LoonieTrader.App.Services;
using LoonieTrader.App.ViewModels.Windows;
using Microsoft.Practices.ServiceLocation;

namespace LoonieTrader.App.Views
{
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            Closing += Window_Closing;

            var ls = ServiceLocator.Current.GetInstance<LayoutService>();
            base.SourceInitialized += (s, e) => ls.Tracker.Configure(this).Apply();

            var fore = ColorConfig.ForeGround;

            NewOrderButton.Icon = ImageAwesome.CreateImageSource(FontAwesomeIcon.Plus, fore);
            NewOrderButton.LargeIcon = ImageAwesome.CreateImageSource(FontAwesomeIcon.Plus, fore);

            ChartButton.Icon = ImageAwesome.CreateImageSource(FontAwesomeIcon.BarChart, fore);
            ChartButton.LargeIcon = ImageAwesome.CreateImageSource(FontAwesomeIcon.BarChart, fore);

            InstrumentPickerButton.Icon = ImageAwesome.CreateImageSource(FontAwesomeIcon.Money, fore);
            InstrumentPickerButton.LargeIcon = ImageAwesome.CreateImageSource(FontAwesomeIcon.Money, fore);

            BlotterButton.Icon = ImageAwesome.CreateImageSource(FontAwesomeIcon.ListAlt, fore);
            BlotterButton.LargeIcon = ImageAwesome.CreateImageSource(FontAwesomeIcon.ListAlt, fore);

            IndicatorWorkbenchButton.Icon = ImageAwesome.CreateImageSource(FontAwesomeIcon.Wrench, fore);
            IndicatorWorkbenchButton.LargeIcon = ImageAwesome.CreateImageSource(FontAwesomeIcon.Wrench, fore);

            MachineLearningButton.Icon = ImageAwesome.CreateImageSource(FontAwesomeIcon.GraduationCap, fore);
            MachineLearningButton.LargeIcon = ImageAwesome.CreateImageSource(FontAwesomeIcon.GraduationCap, fore);


            AccountDetailsButton.Icon = ImageAwesome.CreateImageSource(FontAwesomeIcon.NewspaperOutline, fore);
            AccountDetailsButton.LargeIcon = ImageAwesome.CreateImageSource(FontAwesomeIcon.NewspaperOutline, fore);
            TransactionHistoryButton.Icon = ImageAwesome.CreateImageSource(FontAwesomeIcon.List, fore);
            TransactionHistoryButton.LargeIcon = ImageAwesome.CreateImageSource(FontAwesomeIcon.List, fore);
            OpenOrdersButton.Icon= ImageAwesome.CreateImageSource(FontAwesomeIcon.AlignLeft, fore);
            OpenOrdersButton.LargeIcon = ImageAwesome.CreateImageSource(FontAwesomeIcon.AlignLeft, fore);
            OpenPositionsButton.Icon = ImageAwesome.CreateImageSource(FontAwesomeIcon.AlignRight, fore);
            OpenPositionsButton.LargeIcon = ImageAwesome.CreateImageSource(FontAwesomeIcon.AlignRight, fore);

            // --
            SettingsButton.Icon = ImageAwesome.CreateImageSource(FontAwesomeIcon.Cogs, fore);
            SettingsButton.LargeIcon = ImageAwesome.CreateImageSource(FontAwesomeIcon.Cogs, fore);

            LogOutButton.Icon = ImageAwesome.CreateImageSource(FontAwesomeIcon.PowerOff, fore);
            LogOutButton.LargeIcon = ImageAwesome.CreateImageSource(FontAwesomeIcon.PowerOff, fore);

            LogsButton.Icon = ImageAwesome.CreateImageSource(FontAwesomeIcon.AlignJustify, fore);
            LogsButton.LargeIcon = ImageAwesome.CreateImageSource(FontAwesomeIcon.AlignJustify, fore);

            ServiceStatusButton.Icon = ImageAwesome.CreateImageSource(FontAwesomeIcon.ExclamationTriangle, fore);
            ServiceStatusButton.LargeIcon = ImageAwesome.CreateImageSource(FontAwesomeIcon.ExclamationTriangle, fore);

            WebLinksButton.Icon = ImageAwesome.CreateImageSource(FontAwesomeIcon.Link, fore);
            WebLinksButton.LargeIcon = ImageAwesome.CreateImageSource(FontAwesomeIcon.Link, fore);

            AboutButton.Icon = ImageAwesome.CreateImageSource(FontAwesomeIcon.QuestionCircle, fore);
            AboutButton.LargeIcon = ImageAwesome.CreateImageSource(FontAwesomeIcon.QuestionCircle, fore);
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            var vm = base.DataContext as MainWindowViewModel;
            vm?.ExitApplicationCommand.Execute(e);
        }
    }
}