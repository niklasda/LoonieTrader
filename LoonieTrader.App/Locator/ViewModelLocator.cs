using LoonieTrader.App.ViewModels.Windows;
using LoonieTrader.RestLibrary.Locator;
using StructureMap;

namespace LoonieTrader.App.Locator
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            _container = new Container(c =>
            {
                c.AddRegistry<ServiceRegistry>();
            });

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////}
        }

        private readonly Container _container;

        public MainWindowViewModel Main { get { return _container.GetInstance<MainWindowViewModel>(); } }

        public LoginWindowViewModel Login { get { return _container.GetInstance<LoginWindowViewModel>(); } }

        public AboutWindowViewModel About { get { return _container.GetInstance<AboutWindowViewModel>(); } }

    }
}