using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using AutoMapper;
using LoonieTrader.App.Mapper;
using LoonieTrader.App.Services;
using LoonieTrader.App.ViewModels.Windows;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.Locator;
using LoonieTrader.Shared.Interfaces;
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
                c.For<IDialogService>().Use<DialogService>();
                c.ForSingletonOf<MainWindowViewModel>().Use<MainWindowViewModel>();

                c.AddRegistry<ServiceRegistry>();
            });

            LoadAll();
        }

        private void LoadAll()
        {
            var frw = _container.GetInstance<IFileReaderWriterService>();
            var appSettingsFolder = frw.GetIndicatorFolderPath();

            var catalog = new DirectoryCatalog(appSettingsFolder);
            var container = new CompositionContainer(catalog);

            catalog.Refresh();

            var laggers = container.GetExportedValues<ILaggingIndicator>();
            var leaders = container.GetExportedValues<ILeadingIndicator>();
            var algos = container.GetExportedValues<IAlgorithmicTrader>();
        }

        private readonly IContainer _container;

        public MainWindowViewModel Main => _container.GetInstance<MainWindowViewModel>();

        public ChartWindowViewModel Chart => _container.GetInstance<ChartWindowViewModel>();

        public ComplexOrderWindowViewModel ComplexOrder => _container.GetInstance<ComplexOrderWindowViewModel>();

        public LoginWindowViewModel Login => _container.GetInstance<LoginWindowViewModel>();

        public WorkbenchWindowViewModel Workbench => _container.GetInstance<WorkbenchWindowViewModel>();

        public SettingsWindowViewModel Settings => _container.GetInstance<SettingsWindowViewModel>();

        public AboutWindowViewModel About => _container.GetInstance<AboutWindowViewModel>();

        public LogWindowViewModel Log => _container.GetInstance<LogWindowViewModel>();

        public ServiceStatusWindowViewModel ServiceStatus => _container.GetInstance<ServiceStatusWindowViewModel>();

    }
}