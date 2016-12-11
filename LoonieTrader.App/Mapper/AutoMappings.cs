using System.Globalization;
using AutoMapper;
using LiveCharts.Defaults;
using LoonieTrader.App.ViewModels;
using LoonieTrader.App.ViewModels.Windows;
using LoonieTrader.Library.HistoricalData;
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
                var sourceCulture = new CultureInfo("en-US");

                CreateMap<AccountInstrumentsResponse.Instrument, InstrumentViewModel>();

                CreateMap<AccountSummaryResponse.AccountSummary, AccountSummaryViewModel>();

                CreateMap<ComplexOrderWindowViewModel, OrderCreateResponse.OrderDefinition.Order>()
                    .ForMember(i => i.instrument, m => m.MapFrom(r => r.SelectedInstrument.Name))
                    .ForMember(i => i.price, m => m.MapFrom(r => r.MainPrice))
                    .ForMember(i => i.units, m => m.MapFrom(r => r.Amount));

                CreateMap<PricesResponse.Bid, PriceDepthViewModel>()
                    .ForMember(i => i.Bid, m => m.MapFrom(r => r.liquidity))
                    .ForMember(i => i.Price, m => m.MapFrom(r => r.price));

                CreateMap<PricesResponse.Ask, PriceDepthViewModel>()
                    .ForMember(i => i.Ask, m => m.MapFrom(r => r.liquidity))
                    .ForMember(i => i.Price, m => m.MapFrom(r => r.price));

                CreateMap<PositionsResponse.Position, PositionViewModel>()
                    .ForMember(i => i.ProfitLoss, m => m.MapFrom(r => decimal.Parse(r.pl, sourceCulture)));

                CreateMap<OrdersResponse.Order, OrderViewModel>();
                CreateMap<ServiceEventsResponse.Event, ServiceEventViewModel>();
                CreateMap<ServicesResponse.Service, ServiceViewModel>();

                CreateMap<TradesResponse.Trade, TradeViewModel>();

                CreateMap<TransactionsResponse.Transaction, TransactionViewModel>()
                    .ForMember(i => i.AccountBalance, m => m.MapFrom(r => decimal.Parse(r.accountBalance ?? "0", sourceCulture)));


                CreateMap<CandleDataRecord, CandleDataViewModel>();
                CreateMap<CandleDataRecord, OhlcPoint>();
                CreateMap<CandleDataViewModel, OhlciPoint>();
            }
        }
    }
}