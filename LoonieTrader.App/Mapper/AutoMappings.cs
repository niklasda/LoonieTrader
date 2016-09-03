using System.Globalization;
using AutoMapper;
using LoonieTrader.App.ViewModels;
using LoonieTrader.App.ViewModels.Windows;
using LoonieTrader.RestLibrary.HistoricalData;
using LoonieTrader.RestLibrary.RestApi.Responses;

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
                    //.ForMember(i => i.type, m => m.MapFrom(r => r.SelectedInstrument.Name))
                    .ForMember(i => i.price, m => m.MapFrom(r => r.MainPrice))
                    .ForMember(i => i.units, m => m.MapFrom(r => r.Amount));
//                    .ForMember(i => i.stopLossOnFill.price, m => m.MapFrom(r => r.StopLossPrice));
                    //.ForMember(i => i.timeInForce, m => m.MapFrom(r => r.SelectedInstrument.Name))
                    //.ForMember(i => i.positionFill, m => m.MapFrom(r => r.SelectedInstrument.Name));

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