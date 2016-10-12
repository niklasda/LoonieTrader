﻿using System.Collections.ObjectModel;
using AutoMapper;
using GalaSoft.MvvmLight;
using JetBrains.Annotations;
using LoonieTrader.Library.RestApi.Requesters;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.App.ViewModels.Windows
{
    [UsedImplicitly]
    public class ServiceStatusPracticeWindowViewModel : ViewModelBase
    {
        public ServiceStatusPracticeWindowViewModel(IMapper mapper, HealthRequester healthRequester)
        {
            ServiceEventsResponse restEvents = healthRequester.GetServiceEvents("fxtrade-rest-api");
            ServiceEventViewModel[] restVms = mapper.Map<ServiceEventsResponse.Event[], ServiceEventViewModel[]>(restEvents.events);
            RestEvents = new ObservableCollection<ServiceEventViewModel>(restVms);

            ServiceEventsResponse streamEvents = healthRequester.GetServiceEvents("fxtrade-streaming-api");
            ServiceEventViewModel[] streamVms = mapper.Map<ServiceEventsResponse.Event[], ServiceEventViewModel[]>(streamEvents.events);
            StreamEvents = new ObservableCollection<ServiceEventViewModel>(streamVms);
        }

        public ObservableCollection<ServiceEventViewModel> RestEvents { get; set; }
        public ObservableCollection<ServiceEventViewModel> StreamEvents { get; set; }
    }
}