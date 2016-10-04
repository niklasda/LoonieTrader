using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LoonieTrader.App.ViewModels;
using LoonieTrader.App.ViewModels.Windows;

namespace LoonieTrader.App.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var mvm = this.DataContext as MainWindowViewModel;
            if (mvm != null)
            {
                mvm.ExitApplicationCommand.Execute(e);
            }
        }

        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TreeView tree = sender as TreeView;
            InstrumentViewModel instrument = tree?.SelectedItem as InstrumentViewModel;
            if (instrument != null)
            {
                var mvm = this.DataContext as MainWindowViewModel;
                if (mvm != null)
                {
                    mvm.ChangeChartInstrument(instrument);
                }
            }
        }
    }
}