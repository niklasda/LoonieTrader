using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using FontAwesome.WPF;
using LoonieTrader.App.Constants;
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

            var fore = ColorConfig.ForeGround;

            OpenPositionsTabIcon.Source = ImageAwesome.CreateImageSource(FontAwesomeIcon.AlignRight, fore);
            OpenOrdersTabIcon.Source = ImageAwesome.CreateImageSource(FontAwesomeIcon.AlignLeft, fore);
            TransactionHistoryTabIcon.Source = ImageAwesome.CreateImageSource(FontAwesomeIcon.List, fore);
            AccountDetailTabIcon.Source = ImageAwesome.CreateImageSource(FontAwesomeIcon.NewspaperOutline, fore);
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
