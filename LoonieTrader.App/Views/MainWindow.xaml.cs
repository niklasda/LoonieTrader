using System;
using System.ComponentModel;
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

            Closing += Window_Closing;

            var ls = ServiceLocator.Current.GetInstance<LayoutService>();
            base.SourceInitialized += (s, e) => ls.Tracker.Configure(this).Apply();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var vm = base.DataContext as MainWindowViewModel;
            vm?.ExitApplicationCommand.Execute(e);
        }
    }
}