using AutoMapper;
using GalaSoft.MvvmLight;
using JetBrains.Annotations;
using LoonieTrader.App.ViewModels.Parts;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;

namespace LoonieTrader.App.ViewModels.Windows
{
    [UsedImplicitly]
    public class ChartWindowViewModel : ViewModelBase
    {
        public ChartWindowViewModel(IMapper mapper, ISettingsService settingsService, IPricingStreamingRequester priceStreamer, ChartPartViewModel chartPart)
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
        public ChartPartViewModel ChartPart { get; private set; }

    }
}