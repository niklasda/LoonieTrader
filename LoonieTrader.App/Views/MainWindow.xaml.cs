using System.Windows;
using Fluent;
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

            var ls = ServiceLocator.Current.GetInstance<LayoutService>();
            base.SourceInitialized += (s, e) => ls.Tracker.Configure(this).Apply();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var mvm = base.DataContext as MainWindowViewModel;
            mvm?.ExitApplicationCommand.Execute(e);
        }
    }
}