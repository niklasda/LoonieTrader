using System;
using System.ComponentModel;
using System.Windows.Media;
using Fluent;
using FontAwesome.WPF;
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

            OrderButton.Icon = ImageAwesome.CreateImageSource(FontAwesomeIcon.Flag, Brushes.Black);
            OrderButton.LargeIcon = ImageAwesome.CreateImageSource(FontAwesomeIcon.Flag, Brushes.Black);
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            var vm = base.DataContext as MainWindowViewModel;
            vm?.ExitApplicationCommand.Execute(e);
        }
    }
}