using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace LoonieTrader.App.Views
{
    public partial class CompositeOrderWindow : Window
    {
        public CompositeOrderWindow()
        {
            InitializeComponent();
        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    TaskBar.SetIsOpened(SectionOrderData, true);
        //    TaskBar.SetIsOpened(SectionMarketDepth, false);
        //    TaskBar.SetIsOpened(SectionTakeProfit, false);
        //    TaskBar.SetIsOpened(SectionStopLoss, false);
        //    TaskBar.SetIsOpened(SectionChart, false);
        //}

        //private void TaskBar_OnIsOpenedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    var ctrl = d as TaskBar;
        //    if (ctrl != null)
        //    {
        //        TaskBarItem item = ctrl.SelectedItem;

        //        bool willBeOpen;
        //        if (item != null && bool.TryParse(e.NewValue.ToString(), out willBeOpen))
        //        {
        //            if (item == SectionStopLoss)
        //            {
        //                item.Header = $"Stop-Loss {(willBeOpen ? "" : "[Not specified]")}";
        //            }
        //            else if (item == SectionTakeProfit)
        //            {
        //                item.Header = $"Take-Profit {(willBeOpen ? "" : "[Not specified]")}";
        //            }
        //        }
        //    }
        //}

        //private void UIElement_OnKeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Enter)
        //    {
        //        TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
        //        base.MoveFocus(request);
        //    }
        //}

        //private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
        //{
        //    var autoComplete = sender as AutoComplete;
        //    if (autoComplete != null)
        //    {
        //        Popup pop = autoComplete.Template.FindName("PART_Popup", autoComplete) as Popup;
        //        if (pop != null)
        //        {
        //            pop.AllowsTransparency = true;
        //            pop.Child.Opacity = 0.8;
        //        }
        //    }
        //}
        //private void UIElement_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        //{
        //    throw new System.NotImplementedException();
        //}
    }
}
