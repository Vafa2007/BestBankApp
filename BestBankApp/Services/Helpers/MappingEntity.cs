using AutoMapper;
using BestBankApp.Models;

namespace BestBankApp.Services.Helpers
{
    public class MappingEntity : Profile
    {
        public MappingEntity()
        {
            CreateMap<ClientPayload, Clients>().ReverseMap();
            CreateMap<EditClientPayload, Clients>().ReverseMap();
            CreateMap<Clients, CreditApplyPayload>()
                .ForMember(dest => dest.ClientId, opts => opts.MapFrom(src => src.Id)).ReverseMap(); ;
            CreateMap<Credits, CreditApplyPayload>().ReverseMap();
        }
    }
}