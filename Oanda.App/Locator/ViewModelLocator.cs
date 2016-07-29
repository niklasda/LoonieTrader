using LoonieTrader.App.ViewModels;
using StructureMap;

namespace LoonieTrader.App.Locator
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            _container = new Container(c =>
            {
                c.ForSingletonOf<MainViewModel>().Use<MainViewModel>();
                c.ForSingletonOf<LoginViewModel>().Use<LoginViewModel>();
                c.ForSingletonOf<AboutViewModel>().Use<AboutViewModel>();
            });

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}
        }

        private Container _container;

        public MainViewModel Main { get { return _container.GetInstance<MainViewModel>(); } }

        public LoginViewModel Login { get { return _container.GetInstance<LoginViewModel>(); } }

        public AboutViewModel About { get { return _container.GetInstance<AboutViewModel>(); } }

    }
}