using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows.Input;
using AutoMapper;
using GalaSoft.MvvmLight.CommandWpf;
using JetBrains.Annotations;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.Models;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;
using LoonieTrader.Library.ViewModels;
using OxyPlot;

namespace LoonieTrader.OxyPlot.ViewModels
{
    [UsedImplicitly]
    public class OxyPlotPartViewModel : ChartBaseViewModel
    {
        public OxyPlotPartViewModel(IMapper mapper, ISettingsService settings, IPricingStreamingRequester priceStreamer)
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

            this.Title = "Example 2";
            this.Points = new List<DataPoint>
                            {
                                new DataPoint(0, 4),
                                new DataPoint(10, 13),
                                new DataPoint(20, 15),
                                new DataPoint(30, 16),
                                new DataPoint(40, 12),
                                new DataPoint(50, 12)
                            };

            UpdateCommand = new RelayCommand(UpdateAllOnClick);

        }

        private readonly IMapper _mapper;
        //private readonly DateTime _dateOffset;
        private readonly SynchronizationContext _uiContext = SynchronizationContext.Current;

        public string Title { get; private set; }

        public IList<DataPoint> Points { get; private set; }

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
                        Open = ask,
                        High = (ask + 0.02),
                        Low = (ask - 0.01),
                        Close = (ask + 0.01),
                        Date = DateTime.Now.ToString("yyyyMMdd"),
                        Time = DateTime.Now.ToString("HHmmss")
                    };

                    //SeriesCollection[0].Values.Add(p);
                    //SeriesCollection[1].Values.Add(p);
                    //  Labels.Add(DateTime.Now.AddDays(Labels.Count).Ticks);
                }
            }, null);

        }

        private void UpdateAllOnClick()
        {
        }
    }
}