using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSpaServices.Models;

namespace WebSpaServices.Utils
{
    // This is the approach starting with version 5
    public class AutomapperProfile : Profile
    {
        /// <summary>
        /// Настройка методов автомаппера
        /// </summary>
        public AutomapperProfile()
        {
            // Настройка AutoMapper
            CreateMap<Animal, AnimalLight>()
                    .ForMember(dest => dest.RegIds, opt => opt.MapFrom(src => src.Regions.Select(r => r.Id)));

            // Настройка AutoMapper
            CreateMap<AnimalLight, Animal>()
                .ForMember(dest => dest.Regions, opt => opt.Ignore());
        }
    }
}