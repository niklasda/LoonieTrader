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
        public WorkbenchWindowViewModel Workbench
        {
            get { return _container.GetInstance<WorkbenchWindowViewModel>(); }
        }
        public SettingsWindowViewModel Settings
        {
            get { return _container.GetInstance<SettingsWindowViewModel>(); }
        }

        public AboutWindowViewModel About
        {
            get { return _container.GetInstance<AboutWindowViewModel>(); }
        }
    }
}