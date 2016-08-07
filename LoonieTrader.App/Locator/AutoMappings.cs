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
            }
        }
    }
}