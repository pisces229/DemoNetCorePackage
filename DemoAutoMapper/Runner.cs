using AutoMapper;
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
                configure.CreateMap<FirstDto, SecondDto>()
                    .ForMember(member => member.Id, option => option.MapFrom(source => Convert.ToInt32(source.No)));
            });
            mapperConfiguration.AssertConfigurationIsValid();
            var mapper = mapperConfiguration.CreateMapper();
            var firstDtos = new List<FirstDto>() 
            {
                new FirstDto() {
                    Id = Guid.NewGuid().ToString(),
                    No = "1",
                    Name = "Name"
                }
            };
            Console.WriteLine(JsonSerializer.Serialize(firstDtos, _jsonSerializerOptions));
            var secondDtos = mapper.Map<IEnumerable<SecondDto>>(firstDtos);
            Console.WriteLine(JsonSerializer.Serialize(secondDtos, _jsonSerializerOptions));
            return Task.CompletedTask;
        }
        private class FirstDto
        {
            public string? Id { get; set; }
            public string? No { get; set; }
            public string? Name { get; set; }
            public string? Address { get; set; }
        }
        private class SecondDto
        {
            public string? Id { get; set; }
            public int? No { get; set; }
            public string? Name { get; set; }
            public string? Birthday { get; set; }
        }
    }
}
