using System.Windows;
using LoonieTrader.App.ViewModels;
using LoonieTrader.App.ViewModels.Windows;

namespace LoonieTrader.App.Views
{
    public partial class CompositeOrderWindow : Window
    {
        public CompositeOrderWindow()
        {
            InitializeComponent();
        }

        public void ShowInstrument(InstrumentViewModel instrument)
        {
            var vm = DataContext as ComplexOrderWindowViewModel;
            vm.Instrument = instrument;
            vm.SelectedInstrument = instrument;
            Show();
        }
    }
}
