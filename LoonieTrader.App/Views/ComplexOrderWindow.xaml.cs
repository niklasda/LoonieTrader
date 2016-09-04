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
            TaskBar.SetIsOpened(SectionOrderData, true);
            TaskBar.SetIsOpened(SectionMarketDepth, false);
            TaskBar.SetIsOpened(SectionTakeProfit, false);
            TaskBar.SetIsOpened(SectionStopLoss, false);
            TaskBar.SetIsOpened(SectionChart, false);
        }

        private void TaskBar_OnIsOpenedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var ctrl = d as TaskBar;
            if (ctrl != null)
            {
                TaskBarItem item = ctrl.SelectedItem;

                bool willBeOpen;
                if (item != null && bool.TryParse(e.NewValue.ToString(), out willBeOpen))
                {
                    if (item == SectionStopLoss)
                    {
                        item.Header = string.Format("Stop-Loss {0}", willBeOpen ? "" : "[Not specified]");
                    }
                    else if (item == SectionTakeProfit)
                    {
                        item.Header = string.Format("Take-Profit {0}", willBeOpen ? "" : "[Not specified]");
                    }
                }
            }
        }
    }
}
