using System.Globalization;
using AutoMapper;
using AutoMapper.Configuration.Conventions;
using LoonieTrader.App.ViewModels;
using LoonieTrader.RestLibrary.HistoricalData;
using LoonieTrader.RestLibrary.Models.Responses;

namespace LoonieTrader.App.Locator
{
    public class AutoMappings
    {
        public IMapper CreateMapper()
        {
           // Mapper.Initialize(cfg => cfg.DefaultMemberConfig.AddName<CaseInsensitiveName>());
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            return config.CreateMapper();
        }

        private class MappingProfile : Profile
        {
            public MappingProfile()
            {
                var sourceCulture = new CultureInfo("en-US");

                CreateMap<AccountInstrumentsResponse.Instrument, InstrumentViewModel>();
              //      .ForMember(i => i.Instrument, m => m.MapFrom(r => r.displayName));

                CreateMap<AccountSummaryResponse.AccountSummary, AccountSummaryViewModel>();
//                    .ForMember(i => i.Id, m => m.MapFrom(r => r.id));

                CreateMap<PositionsResponse.Position, PositionViewModel>()
                    .ForMember(i => i.ProfitLoss, m => m.MapFrom(r => decimal.Parse(r.pl, sourceCulture)));

                CreateMap<OrdersResponse.Order, OrderViewModel>();
//                    .ForMember(i => i.Instrument, m => m.MapFrom(r => r.instrument));

                CreateMap<TradesResponse.Trade, TradeModel>();
//                    .ForMember(i => i.Instrument, m => m.MapFrom(r => r.instrument));

                CreateMap<TransactionsResponse.Transaction, TransactionViewModel>()
                    .ForMember(i => i.AccountBalance, m => m.MapFrom(r => decimal.Parse(r.accountBalance ?? "0", sourceCulture)));


                CreateMap<CandleDataRecord, CandleDataViewModel>();//.ForMember(i => i.Instrument, m => m.MapFrom(r => r.instrument));
            }
        }
    }
}