using AutoMapper;
using LoonieTrader.App.ViewModels;
using LoonieTrader.RestLibrary.Models.Responses;

namespace LoonieTrader.App.Locator
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
                CreateMap<AccountInstrumentsResponse.Instrument, InstrumentModel>()
                    .ForMember(i => i.Instrument, m => m.MapFrom(r => r.displayName));

                CreateMap<AccountSummaryResponse.AccountSummary, AccountSummaryModel>()
                    .ForMember(i => i.Id, m => m.MapFrom(r => r.id));

                CreateMap<PositionsResponse.Position, PositionModel>()
                    .ForMember(i => i.Instrument, m => m.MapFrom(r => r.instrument));

                CreateMap<OrdersResponse.Order, OrderModel>()
                    .ForMember(i => i.Instrument, m => m.MapFrom(r => r.instrument));

                CreateMap<TradesResponse.Trade, TradeModel>()
                    .ForMember(i => i.Instrument, m => m.MapFrom(r => r.instrument));

                CreateMap<TransactionsResponse.Transaction, TransactionModel>()
                    .ForMember(i => i.Instrument, m => m.MapFrom(r => r.instrument));
            }
        }
    }
}