using System.Windows;
using LoonieTrader.App.ViewModels.Windows;

namespace LoonieTrader.App.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var mvm = this.DataContext as MainWindowViewModel;
            if (mvm != null)
            {
                mvm.MainChart = this.MainChart;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var mvm = this.DataContext as MainWindowViewModel;
            if (mvm != null)
            {
                mvm.ExitApplicationCommand.Execute(e);
            }
        }
    }
}
