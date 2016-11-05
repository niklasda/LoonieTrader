using System.Windows;
using System.Windows.Controls;

namespace LoonieTrader.App.Views.Parts
{
    /// <summary>
    /// Interaction logic for NewsPartView.xaml
    /// </summary>
    public partial class NewsPartView : UserControl
    {
        public NewsPartView()
        {
            InitializeComponent();
        }

        private void CalendarBrowser_OnLoaded(object sender, RoutedEventArgs e)
        {
            CalendarBrowser.Navigate("http://www.marketpulse.com/economic-events/");
        }

        private void NewsBrowser_OnLoaded(object sender, RoutedEventArgs e)
        {
            NewsBrowser.Navigate("http://www.marketpulse.com/");
        }
    }
}
