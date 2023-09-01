using AutoMapper;
using MagicVilla_WebAPI.Models;
using MagicVilla_WebAPI.Models.DTOs;

namespace MagicVilla_WebAPI;

public class MapConfig : Profile
{
    public MapConfig()
    {
        CreateMap<VillaDTO, Villa>();
        CreateMap<Villa, VillaDTO>();

        // creating map and reverse map in one line
        CreateMap<Villa, VillaCreateDTO>().ReverseMap();
        CreateMap<Villa, VillaUpdateDTO>().ReverseMap();

        
        CreateMap<VillaNumberDTO, VillaNumber>().ReverseMap();
        CreateMap<VillaNumber, VillaNumberCreateDTO>().ReverseMap();
        CreateMap<VillaNumber, VillaNumberUpdateDTO>().ReverseMap();
    }
    
}