using AutoMapper;
using DemoAutoMapper.Dtos;

namespace DemoAutoMapper
{
    public class DtoProfile : Profile
    {
        public DtoProfile()
        {
            CreateMap<FirstDto, SecondDto>()
                .ForMember(
                    member => member.Id,
                    option => 
                    {
                        Console.WriteLine("ForMember(Id)...");
                        option.PreCondition((source, target) =>
                        {
                            Console.WriteLine("PreCondition(Id)...");
                            return true;
                        });
                        option.MapFrom(source => Convert.ToInt32(source.No));
                    })
                .ForMember(
                    member => member.Name,
                    option =>
                    {
                        Console.WriteLine("ForMember(Name)...");
                        option.PreCondition((source, target) =>
                        {
                            Console.WriteLine("PreCondition(Name)...");
                            return true;
                        });
                        option.MapFrom(source => source.Name);
                    })
                .BeforeMap((source, target) =>
                {
                    Console.WriteLine("BeforeMap...");
                })
                .AfterMap((source, target, context) =>
                {
                    Console.WriteLine("AfterMap...");
                })
                .ConvertUsing<DtoTypeConverter>();
                
            //CreateMap<FirstDto, SecondDto>().ReverseMap();
            //CreateMap<FirstDto, SecondDto>();
            //CreateMap<SecondDto, FirstDto>();
        }
    }
}
