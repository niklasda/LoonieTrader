using System.Windows;
using LoonieTrader.App.ViewModels;
using LoonieTrader.App.ViewModels.Windows;

namespace LoonieTrader.App.Views
{
    public partial class ChartWindow : Window
    {
        public ChartWindow()
        {
            InitializeComponent();
        }

        public void ShowInstrument(InstrumentViewModel instrument)
        {
            var vm = DataContext as ChartWindowViewModel;
            vm.Instrument = instrument;
            
            Show();
        }
    }
}
