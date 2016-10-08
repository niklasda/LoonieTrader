using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using LoonieTrader.App.ViewModels;
using LoonieTrader.App.ViewModels.Windows;

namespace LoonieTrader.App.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var mvm = this.DataContext as MainWindowViewModel;
            if (mvm != null)
            {
                mvm.ExitApplicationCommand.Execute(e);
            }
        }

        private void InstrumentTree_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TreeView tree = sender as TreeView;
            InstrumentViewModel instrument = tree?.SelectedItem as InstrumentViewModel;
            if (instrument != null)
            {
                var mvm = this.DataContext as MainWindowViewModel;
                if (mvm != null)
                {
                    mvm.ChangeChartInstrument(instrument);
                }
            }
        }

        private void InstrumentTree_OnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem treeViewItem = VisualUpwardSearch(e.OriginalSource as DependencyObject);

            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                treeViewItem.IsSelected = true;
                e.Handled = true;
            }
        }

        private TreeViewItem VisualUpwardSearch(DependencyObject source)
        {
            while (source != null && !(source is TreeViewItem))
            {
                source = VisualTreeHelper.GetParent(source);
            }

            return source as TreeViewItem;
        }
    }
}