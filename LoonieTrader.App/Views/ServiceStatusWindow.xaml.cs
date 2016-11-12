using System.Windows;
using LoonieTrader.App.Services;
using Microsoft.Practices.ServiceLocation;

namespace LoonieTrader.App.Views
{
    public partial class ServiceStatusWindow : Window
    {
        public ServiceStatusWindow()
        {
            InitializeComponent();

            var ls = ServiceLocator.Current.GetInstance<LayoutService>();
            base.SourceInitialized += (s, e) => ls.Tracker.Configure(this).Apply();
        }
    }
}
