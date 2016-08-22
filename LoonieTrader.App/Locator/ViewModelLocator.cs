using AutoMapper;
using LoonieTrader.App.ViewModels.Windows;
using LoonieTrader.RestLibrary.Locator;
using StructureMap;

namespace LoonieTrader.App.Locator
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            AutoMappings ams = new AutoMappings();
            IMapper mapper = ams.CreateMapper();

            _container = new Container(c =>
            {
                c.ForSingletonOf<IMapper>().Use(mapper);

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

        public MainWindowViewModel Main
        {
            get { return _container.GetInstance<MainWindowViewModel>(); }
        }

        public ChartWindowViewModel Chart
        {
            get { return _container.GetInstance<ChartWindowViewModel>(); }
        }

        public MarketOrderWindowViewModel MarketOrder
        {
            get { return _container.GetInstance<MarketOrderWindowViewModel>(); }
        }
        public CompositeOrderWindowViewModel CompositeOrder
        {
            get { return _container.GetInstance<CompositeOrderWindowViewModel>(); }
        }

        public LoginWindowViewModel Login
        {
            get { return _container.GetInstance<LoginWindowViewModel>(); }
        }
        public LoginWindowViewModel Workbench
        {
            get { return _container.GetInstance<LoginWindowViewModel>(); }
        }
        public LoginWindowViewModel Settings
        {
            get { return _container.GetInstance<LoginWindowViewModel>(); }
        }

        public AboutWindowViewModel About
        {
            get { return _container.GetInstance<AboutWindowViewModel>(); }
        }
    }
}