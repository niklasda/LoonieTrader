using System.Collections.ObjectModel;
using AutoMapper;
using GalaSoft.MvvmLight;
using JetBrains.Annotations;
using LoonieTrader.Library.RestApi.Requesters;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.App.ViewModels.Windows
{
    [UsedImplicitly]
    public class ServiceStatusLiveWindowViewModel : ViewModelBase
    {
        public ServiceStatusLiveWindowViewModel(IMapper mapper, HealthRequester healthRequester)
        {
            _mapper = mapper;
            _healthRequester = healthRequester;

            ServicesResponse servicesResponse = _healthRequester.GetServices();
            var svcvms = _mapper.Map<ServicesResponse.Service[], ServiceViewModel[]>(servicesResponse.services);
            AvailableEnvironments = svcvms;
        }

        private readonly IMapper _mapper;
        private readonly HealthRequester _healthRequester;
        public ObservableCollection<ServiceEventViewModel> RestEvents { get; set; }
        public ObservableCollection<ServiceEventViewModel> StreamEvents { get; set; }

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

                    LoadEnvironenment(_selectedEnvironment.Name);
                }
            }
        }

        private void LoadEnvironenment(string environmentName)
        {
            ServiceEventsResponse restEvents = _healthRequester.GetServiceEvents("fxtrade-rest-api");
            ServiceEventViewModel[] restVms = _mapper.Map<ServiceEventsResponse.Event[], ServiceEventViewModel[]>(restEvents.events);
            RestEvents = new ObservableCollection<ServiceEventViewModel>(restVms);

            ServiceEventsResponse streamEvents = _healthRequester.GetServiceEvents("fxtrade-streaming-api");
            ServiceEventViewModel[] streamVms = _mapper.Map<ServiceEventsResponse.Event[], ServiceEventViewModel[]>(streamEvents.events);
            StreamEvents = new ObservableCollection<ServiceEventViewModel>(streamVms); 
        }
    }
}