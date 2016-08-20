using System.Windows;
using Syncfusion.Windows.Tools.Controls;

namespace LoonieTrader.App.Views
{
    /// <summary>
    /// Interaction logic for CompositeOrderWindow.xaml
    /// </summary>
    public partial class CompositeOrderWindow : Window
    {
        public CompositeOrderWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TaskBar.SetIsOpened(Section1, true);
        }
    }
}
