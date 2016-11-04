//using AutoMapper;
//using LoonieTrader.Library.HistoricalData;

//namespace LoonieTrader.Library.Tests.Locator
//{
//    public class TestAutoMappings
//    {
//        public IMapper CreateMapper()
//        {
//            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
//            return config.CreateMapper();
//        }

//        private class MappingProfile : Profile
//        {
//            public MappingProfile()
//            {
//                CreateMap<CandleDataRecord, CandleDataViewModel>();
//            }
//        }
//    }
//}