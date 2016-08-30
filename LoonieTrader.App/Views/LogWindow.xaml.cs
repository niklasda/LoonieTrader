using System;
using System.Drawing;
using System.Windows;
using Syncfusion.UI.Xaml.Grid;

namespace LoonieTrader.App.Views
{
    public partial class LogWindow : Window
    {
        public LogWindow()
        {
            InitializeComponent();
        }

        private void SfDataGrid_OnQueryRowHeight(object sender, QueryRowHeightEventArgs e)
        {
            var dataGrid = sender as SfDataGrid;

            GridRowSizingOptions gridRowResizingOptions = new GridRowSizingOptions();
            double autoHeight;

            if (dataGrid != null && dataGrid.GridColumnSizer.GetAutoRowHeight(e.RowIndex, gridRowResizingOptions, out autoHeight))
            {
                if (autoHeight > 24)
                {
                    e.Height = Math.Min(autoHeight, 100);
                    e.Handled = true;
                }
            }
        }

    }
}
