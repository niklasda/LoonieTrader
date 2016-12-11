using AutoMapper;
using GalaSoft.MvvmLight;
using JetBrains.Annotations;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.ViewModels;

namespace LoonieTrader.App.ViewModels.Windows
{
    [UsedImplicitly]
    public class ChartWindowViewModel : ViewModelBase
    {
        public ChartWindowViewModel(IMapper mapper, ISettingsService settingsService, IPricingStreamingRequester priceStreamer, ChartBaseViewModel chartPart)
        {
            if (IsInDesignMode)
            {
            }
            else
            {
             //   IObservable<PricesResponse.Price> strm = priceStreamer.GetPriceStream(settings.DefaultAccountId, "EUR_USD");
             //   var sub = strm.Subscribe(x => AddPoint(x));
            }

            ChartPart = chartPart;

        }

        public InstrumentViewModel Instrument {
            get { return ChartPart.Instrument; }
            set { ChartPart.Instrument = value; }
        }
        public ChartBaseViewModel ChartPart { get; private set; }

    }
}