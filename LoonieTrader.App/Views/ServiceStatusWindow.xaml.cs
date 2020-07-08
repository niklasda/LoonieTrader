using System.Windows;
using CommonServiceLocator;
using LoonieTrader.App.Services;

namespace LoonieTrader.App.Views
{
    public partial class ServiceStatusWindow : Window
    {
        public ServiceStatusWindow()
        {
            InitializeComponent();

            var ls = ServiceLocator.Current.GetInstance<LayoutService>();
            base.SourceInitialized += (s, e) => ls.Tracker.Configure(this);
        }
    }
}
