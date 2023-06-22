using AutoMapper;
using DemoAutoMapper.Dtos;
using System.Text.Json;

namespace DemoAutoMapper
{
    internal class Runner
    {
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        public Runner() 
        {
            _jsonSerializerOptions = new JsonSerializerOptions()
            {
                WriteIndented = true,
            };
        }
        public Task Run()
        {
            var mapperConfiguration = new MapperConfiguration(configure => {
                //configure.AllowNullDestinationValues = false;
                //configure.SourceMemberNamingConvention = new LowerUnderscoreNamingConvention();
                //configure.DestinationMemberNamingConvention = new PascalCaseNamingConvention();
                //configure.CreateMap<FirstDto, SecondDto>()
                //    .ForMember(member => member.Id, option => option.MapFrom(source => Convert.ToInt32(source.No)));
                configure.AddProfile<DtoProfile>();
            });
            //mapperConfiguration.AssertConfigurationIsValid();
            var mapper = mapperConfiguration.CreateMapper();
            //mapperConfiguration.CompileMappings();
            var firstDtos = new List<FirstDto>() 
            {
                new FirstDto() {
                    Id = Guid.NewGuid().ToString(),
                    No = "1",
                    Name = "Name"
                }
            };
            Console.WriteLine(JsonSerializer.Serialize(firstDtos, _jsonSerializerOptions));
            var secondDtos = mapper.Map<IEnumerable<FirstDto>, IEnumerable<SecondDto>>(firstDtos);
            //var secondDtos = mapper.Map<FirstDto, SecondDto>(firstDtos.First(), opt => 
            //{
            //    opt.BeforeMap((src, dest) =>
            //    {
            //        src.Name += "@";
            //    });
            //    opt.AfterMap((src, dest) =>
            //    {
            //        dest.Name += "#";
            //    });
            //});
            Console.WriteLine(JsonSerializer.Serialize(secondDtos, _jsonSerializerOptions));
            return Task.CompletedTask;
        }
    }
}
