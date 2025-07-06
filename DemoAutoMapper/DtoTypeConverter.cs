using AutoMapper;
using DemoAutoMapper.Dtos;

namespace DemoAutoMapper
{
    public class DtoTypeConverter : ITypeConverter<FirstDto, SecondDto>
    {
        public SecondDto Convert(FirstDto source, SecondDto target, ResolutionContext context)
        {
            Console.WriteLine("ITypeConverter...");
            return target;
        }
    }
}
