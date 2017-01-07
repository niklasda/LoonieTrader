using System;
using System.Windows;
using LoonieTrader.App.ViewModels.Windows;

namespace LoonieTrader.App.Views
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();

            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var vm = base.DataContext as SettingsWindowViewModel;
            vm.CloseWindow = this.Close;
        }
    }
}
