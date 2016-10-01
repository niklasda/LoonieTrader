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
            /* Default, Office2007Blue, Office2007Black, Office2007Silver, Blend, VS2010, Office2010Blue, Office2010Black, Office2010Silver, Office2003, Metro */
            // SkinStorage.SetVisualStyle(this, "Default");
            //var mvm = this.DataContext as MainWindowViewModel;
            //if (mvm != null)
           // {
                //mvm.MainChart = this.MainChart;
            //}
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
            if (tree != null)
            {
                InstrumentViewModel instrument = tree.SelectedItem as InstrumentViewModel;
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
}