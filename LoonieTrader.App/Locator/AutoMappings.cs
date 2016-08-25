using System.Globalization;
using AutoMapper;
using LoonieTrader.App.ViewModels;
using LoonieTrader.RestLibrary.HistoricalData;
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
                var sourceCulture = new CultureInfo("en-US");

                CreateMap<AccountInstrumentsResponse.Instrument, InstrumentViewModel>();

                CreateMap<AccountSummaryResponse.AccountSummary, AccountSummaryViewModel>();

                CreateMap<PositionsResponse.Position, PositionViewModel>()
                    .ForMember(i => i.ProfitLoss, m => m.MapFrom(r => decimal.Parse(r.pl, sourceCulture)));

                CreateMap<OrdersResponse.Order, OrderViewModel>();

                CreateMap<TradesResponse.Trade, TradeModel>();

                CreateMap<TransactionsResponse.Transaction, TransactionViewModel>()
                    .ForMember(i => i.AccountBalance, m => m.MapFrom(r => decimal.Parse(r.accountBalance ?? "0", sourceCulture)));


                CreateMap<CandleDataRecord, CandleDataViewModel>();//.ForMember(i => i.Instrument, m => m.MapFrom(r => r.instrument));
            }
        }
    }
}