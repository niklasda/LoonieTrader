using System.Windows;
using Xceed.Wpf.DataGrid;
using Xceed.Wpf.DataGrid.Views;

namespace LoonieTrader.App.Views
{
    public partial class WorkbenchWindow : Window
    {
        public WorkbenchWindow()
        {
            InitializeComponent();
        }

        private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        {
            var g = sender as DataGridControl;
            var tv = g.View as TableView;
            
            
        }
    }
}
