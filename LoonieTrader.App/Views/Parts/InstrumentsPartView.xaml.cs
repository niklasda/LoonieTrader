using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using LoonieTrader.App.ViewModels;
using LoonieTrader.App.ViewModels.Windows;
using Microsoft.Practices.ServiceLocation;

namespace LoonieTrader.App.Views.Parts
{
    /// <summary>
    /// Interaction logic for InstrumentPartView.xaml
    /// </summary>
    public partial class InstrumentPartView : UserControl
    {
        public InstrumentPartView()
        {
            InitializeComponent();
        }

        private void InstrumentTree_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TreeView tree = sender as TreeView;
            InstrumentViewModel instrument = tree?.SelectedItem as InstrumentViewModel;
            if (instrument != null)
            {
                var mvm = ServiceLocator.Current.GetInstance<MainWindowViewModel>();

//                var mvm = base.DataContext as MainWindowViewModel;
                mvm?.ChangeChartInstrument(instrument);
            }
        }

        private void InstrumentTree_OnPreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem treeViewItem = VisualUpwardSearch(e.OriginalSource as DependencyObject);

            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                treeViewItem.IsSelected = true;
                var dci = treeViewItem.DataContext as InstrumentViewModel;

                var tree = e.Source as TreeView;

                if (dci != null && tree != null)
                {
                    var itypes = tree.ItemsSource as Collection<InstrumentTypeViewModel>;

                    // todo need better source of favourites
                    if (itypes[0].Instruments.Any(x => x.Name == dci.Name))
                    {
                        tree.ContextMenu = tree.Resources["MenuWithRemove"] as ContextMenu;
                    }
                    else
                    {
                        tree.ContextMenu = tree.Resources["MenuWithAdd"] as ContextMenu;
                    }
                }
                else if (tree != null)
                {
                    tree.ContextMenu = null;
                }

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
