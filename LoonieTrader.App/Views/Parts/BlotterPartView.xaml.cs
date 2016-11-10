using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Xceed.Wpf.DataGrid;
using Xceed.Wpf.DataGrid.Views;

namespace LoonieTrader.App.Views.Parts
{
    /// <summary>
    /// Interaction logic for BlotterPartView.xaml
    /// </summary>
    public partial class BlotterPartView : UserControl
    {
        public BlotterPartView()
        {
            InitializeComponent();
        }

        private void previewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var dataCell = sender as DataCell;
            if (dataCell == null)
                return;

            var parentRow = dataCell.ParentRow;

            var dataGridHost = VisualTreeHelper.GetParent(parentRow) as TableViewItemsHost;
            var dataGrid = dataGridHost?.TemplatedParent as DataGridControl;
            if (dataGrid != null)
            {
                var dataItem = dataGrid.GetItemFromContainer(parentRow);
                dataGrid.SelectedItem = dataItem;
            }

            e.Handled = true;
        }

    }
}
