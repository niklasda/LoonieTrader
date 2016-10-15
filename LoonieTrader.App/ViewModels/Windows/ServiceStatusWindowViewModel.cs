using System.Collections.ObjectModel;
using AutoMapper;
using GalaSoft.MvvmLight;
using JetBrains.Annotations;
using LoonieTrader.Library.RestApi.Requesters;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.App.ViewModels.Windows
{
    [UsedImplicitly]
    public class ServiceStatusWindowViewModel : ViewModelBase
    {
        public ServiceStatusWindowViewModel(IMapper mapper, HealthRequester healthRequester)
        {
            _mapper = mapper;
            _healthRequester = healthRequester;

            if (IsInDesignMode)
            {
            }
            else
            {
                ServicesResponse servicesResponse = _healthRequester.GetServices();
                var svcvms = _mapper.Map<ServicesResponse.Service[], ServiceViewModel[]>(servicesResponse.services);
                AvailableEnvironments = svcvms;
            }
        }

        private readonly IMapper _mapper;
        private readonly HealthRequester _healthRequester;
        public ObservableCollection<ServiceEventViewModel> ServiceEvents { get; set; } = new ObservableCollection<ServiceEventViewModel>();

        public ServiceViewModel[] AvailableEnvironments { get; set; }

        private ServiceViewModel _selectedEnvironment;
        public ServiceViewModel SelectedEnvironment
        {
            get { return _selectedEnvironment; }
            set
            {
                if (_selectedEnvironment != value)
                {
                    _selectedEnvironment = value;
                    RaisePropertyChanged();

                    LoadEnvironenment(_selectedEnvironment.Id);
                }
            }
        }

        private void LoadEnvironenment(string environmentName)
        {
            ServiceEventsResponse restEvents = _healthRequester.GetServiceEvents(environmentName);
            ServiceEventViewModel[] restVms = _mapper.Map<ServiceEventsResponse.Event[], ServiceEventViewModel[]>(restEvents.events);

            ServiceEvents.Clear();
            foreach (var vm in restVms)
            {
                ServiceEvents.Add(vm);
            }
        }
    }
}