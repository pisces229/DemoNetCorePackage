using AutoMapper;
using DemoAutoMapper.Dtos;

namespace DemoAutoMapper
{
    internal class DtoProfile : Profile
    {
        public DtoProfile()
        {
            CreateMap<FirstDto, SecondDto>()
                .BeforeMap((src, dest) =>
                {
                    //...
                })
                .AfterMap((src, dest) =>
                {
                    //...
                })
                .ForMember(member => member.Id, option => option.MapFrom(source => Convert.ToInt32(source.No)));
            //CreateMap<FirstDto, SecondDto>().ReverseMap();
            //CreateMap<FirstDto, SecondDto>();
            //CreateMap<SecondDto, FirstDto>();
        }
    }
}
