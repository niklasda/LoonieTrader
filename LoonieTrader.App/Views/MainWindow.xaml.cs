using System.Windows;
using LoonieTrader.App.ViewModels.Windows;
using Syncfusion.Windows.Shared;

namespace LoonieTrader.App.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            /* Default, Office2007Blue, Office2007Black, Office2007Silver, Blend, VS2010, Office2010Blue, Office2010Black, Office2010Silver, Office2003, Metro */
            // SkinStorage.SetVisualStyle(this, "Default");
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
