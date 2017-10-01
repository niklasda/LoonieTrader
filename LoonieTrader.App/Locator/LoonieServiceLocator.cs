using System.ComponentModel.Composition.Hosting;
using AutoMapper;
using LoonieTrader.App.Mapper;
using LoonieTrader.App.Services;
using LoonieTrader.App.ViewModels.Parts;
using LoonieTrader.App.ViewModels.Windows;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.Locator;
using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace LoonieTrader.App.Locator
{
    public class LoonieServiceLocator
    {
        public LoonieServiceLocator()
        {
            AutoMappings ams = new AutoMappings();
            IMapper mapper = ams.CreateMapper();

            _container = new Container(c =>
            {
                c.ForSingletonOf<IMapper>().Use(mapper);
                c.For<IDialogService>().Use<DialogService>();
                c.ForSingletonOf<MainWindowViewModel>().Use<MainWindowViewModel>();
                c.ForSingletonOf<LayoutService>().Use<LayoutService>();
                //c.For<SciChartPartViewModel>().Use<SciChartPartViewModel>();

                c.AddRegistry<LibraryRegistry>();
                //c.AddRegistry<SciChartRegistry>();
            });

            var exporter = LoadAll();
            _container.Configure(c=>c.For<CompositionContainer>().Use(exporter));

            ServiceLocator.SetLocatorProvider(() => new StructureLocator(_container));
        }

        private CompositionContainer LoadAll()
        {
            var frw = _container.GetInstance<IFileReaderWriterService>();
            var appSettingsFolder = frw.GetIndicatorFolderPath();

            var catalog = new DirectoryCatalog(appSettingsFolder);
            var container = new CompositionContainer(catalog);

            return container;
        }

        private readonly Container _container;

        public MainWindowViewModel Main => _container.GetInstance<MainWindowViewModel>();

        public SciChartPartViewModel Chart => _container.GetInstance<SciChartPartViewModel>();

        public ComplexOrderWindowViewModel ComplexOrder => _container.GetInstance<ComplexOrderWindowViewModel>();

        public LoginWindowViewModel Login => _container.GetInstance<LoginWindowViewModel>();

        public WorkbenchWindowViewModel Workbench => _container.GetInstance<WorkbenchWindowViewModel>();

        public SettingsWindowViewModel Settings => _container.GetInstance<SettingsWindowViewModel>();

        public AboutWindowViewModel About => _container.GetInstance<AboutWindowViewModel>();

        public BlotterWindowViewModel Blotter => _container.GetInstance<BlotterWindowViewModel>();

        public MachineLearningWindowViewModel MachineLearning => _container.GetInstance<MachineLearningWindowViewModel>();

        public InstrumentsWindowViewModel Instruments => _container.GetInstance<InstrumentsWindowViewModel>();

        public LogWindowViewModel Log => _container.GetInstance<LogWindowViewModel>();

        public ServiceStatusWindowViewModel ServiceStatus => _container.GetInstance<ServiceStatusWindowViewModel>();

    }
}