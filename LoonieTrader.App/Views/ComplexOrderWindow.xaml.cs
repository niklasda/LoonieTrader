using System.Windows;
using Syncfusion.Windows.Tools.Controls;

namespace LoonieTrader.App.Views
{
    public partial class CompositeOrderWindow : Window
    {
        public CompositeOrderWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TaskBar.SetIsOpened(Section1, true);
            TaskBar.SetIsOpened(Section2, false);
            TaskBar.SetIsOpened(Section3, false);
            TaskBar.SetIsOpened(Section4, false);
            TaskBar.SetIsOpened(Section5, false);
        }
    }
}
