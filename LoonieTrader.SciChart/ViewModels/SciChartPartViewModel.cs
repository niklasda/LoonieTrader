using System;
using System.Globalization;
using System.Threading;
using System.Windows.Input;
using AutoMapper;
using JetBrains.Annotations;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.Models;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;
using LoonieTrader.Library.ViewModels;

namespace LoonieTrader.SciChart.ViewModels
{
    [UsedImplicitly]
    public class SciChartPartViewModel : ChartBaseViewModel
    {
        public SciChartPartViewModel(IMapper mapper, ISettingsService settings, IPricingStreamingRequester priceStreamer)
        {
            _mapper = mapper;
            if (IsInDesignMode)
            {
            }
            else
            {
                var cfg = settings.CachedSettings.SelectedEnvironment;
                var strm = priceStreamer.GetPriceStream(cfg.DefaultAccountId, "EUR_USD");
                strm.NewValue += Strm_NewPrice;
            }

        }

        private readonly IMapper _mapper;
        //private readonly DateTime _dateOffset;
        private readonly SynchronizationContext _uiContext = SynchronizationContext.Current;

        //private InstrumentViewModel _instrument;
        //public InstrumentViewModel Instrument {
        //    get { return _instrument;}
        //    set
        //    {
        //        if (_instrument != value)
        //        {
        //            _instrument = value;
        //            RaisePropertyChanged();
        //        }
        //    }
        //}

        public ICommand UpdateCommand { get; set; }

        private void Strm_NewPrice(object sender, StreamEventArgs<PricesResponse.Price> e)
        {
            Console.WriteLine(@"addPoint: {0}", e.Obj);
            AddPoint(e.Obj);
        }

        private void AddPoint(PricesResponse.Price price)
        {
            // needed to parse price string since they use us separators
            var c = CultureInfo.GetCultureInfo("en-US");

            _uiContext.Post(o =>
            {
                if (price.asks?.Length > 0)
                {
                    double ask = double.Parse(price.asks[0].price, c);
                    var p = new CandleDataViewModel
                    {
                        Open = (decimal)ask,
                        High = (decimal)(ask + 0.02),
                        Low = (decimal)(ask - 0.01),
                        Close = (decimal)(ask + 0.01),
                        Date = DateTime.Now.ToString("yyyyMMdd"),
                        Time = DateTime.Now.ToString("HHmmss")
                    };

                    //SeriesCollection[0].Values.Add(p);
                    //SeriesCollection[1].Values.Add(p);
                    //  Labels.Add(DateTime.Now.AddDays(Labels.Count).Ticks);
                }
            }, null);

        }
    }
}