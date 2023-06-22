using AutoMapper;
using System;

namespace DemoAutoMapper.Dtos
{
    //[AutoMap(typeof(SecondDto))]
    internal class FirstDto
    {
        public string? Id { get; set; }
        public string? No { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
    }
}
