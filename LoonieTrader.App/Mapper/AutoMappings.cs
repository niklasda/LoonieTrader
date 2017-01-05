using System.Globalization;
using AutoMapper;
using LiveCharts.Defaults;
using LoonieTrader.App.ViewModels;
using LoonieTrader.App.ViewModels.Windows;
using LoonieTrader.Library.Constants;
using LoonieTrader.Library.HistoricalData;
using LoonieTrader.Library.RestApi.Enums;
using LoonieTrader.Library.RestApi.Responses;
using LoonieTrader.Library.ViewModels;
using LoonieTrader.Shared.Models;

namespace LoonieTrader.App.Mapper
{
    public class AutoMappings
    {
        public IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            return config.CreateMapper();
        }

        private class MappingProfile : Profile
        {
            public MappingProfile()
            {
                var serverCulture = AppProperties.ServerCulture;

                CreateMap<AccountInstrumentsResponse.Instrument, InstrumentViewModel>();

                CreateMap<AccountSummaryResponse.AccountSummary, AccountSummaryViewModel>();

                CreateMap<ComplexOrderWindowViewModel, OrderCreateResponse.OrderDefinition.Order>()
                    .ForMember(i => i.instrument, m => m.MapFrom(r => r.SelectedInstrument.Name))
                    .ForMember(i => i.price, m => m.MapFrom(r => r.MainPrice.ToString(serverCulture)))
                    .ForMember(i => i.timeInForce, m => m.MapFrom(r => r.IsGtcExpiry ? TimeInForce.GTC.ToString() : TimeInForce.GTD.ToString()))
                    .ForMember(i => i.type, m => m.MapFrom(r => r.IsMarketOrder ? OrderTypes.MARKET.ToString() : OrderTypes.LIMIT.ToString()))
                    .ForMember(i => i.positionFill, m => m.MapFrom(r => OrderPositionFill.DEFAULT.ToString()))
                    .ForMember(i => i.units, m => m.MapFrom(r => r.Amount.ToString(serverCulture)));

                CreateMap<PricesResponse.Bid, PriceDepthViewModel>()
                    .ForMember(i => i.Bid, m => m.MapFrom(r => r.liquidity))
                    .ForMember(i => i.Price, m => m.MapFrom(r => r.price));

                CreateMap<PricesResponse.Ask, PriceDepthViewModel>()
                    .ForMember(i => i.Ask, m => m.MapFrom(r => r.liquidity))
                    .ForMember(i => i.Price, m => m.MapFrom(r => r.price));

                CreateMap<PositionsResponse.Position, PositionViewModel>()
                    .ForMember(i => i.ProfitLoss, m => m.MapFrom(r => decimal.Parse(r.pl, serverCulture)))
                    .ForMember(i => i.UnrealizedPL, m => m.MapFrom(r => decimal.Parse(r.unrealizedPL, serverCulture)))
                    .ForMember(i => i.ResettablePL, m => m.MapFrom(r => decimal.Parse(r.resettablePL, serverCulture)));

                CreateMap<OrdersResponse.Order, OrderViewModel>();
                CreateMap<ServiceEventsResponse.Event, ServiceEventViewModel>();
                CreateMap<ServicesResponse.Service, ServiceViewModel>();

                CreateMap<TradesResponse.Trade, TradeViewModel>();

                CreateMap<TransactionsResponse.Transaction, TransactionViewModel>()
                    .ForMember(i => i.AccountBalance, m => m.MapFrom(r => decimal.Parse(r.accountBalance ?? "0", serverCulture)))
                    .ForMember(i => i.Id, m => m.MapFrom(r => int.Parse(r.id ?? "0", serverCulture)));


                CreateMap<CandleDataRecord, CandleDataViewModel>();
                CreateMap<CandleDataRecord, OhlcPoint>();
                CreateMap<CandleDataViewModel, OhlciPoint>();
            }
        }
    }
}