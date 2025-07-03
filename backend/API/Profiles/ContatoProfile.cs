using API.DTOs;
using API.Models;
using AutoMapper;

namespace API.Profiles
{
    public class ContatoProfile : Profile
    {
        public ContatoProfile()
        {
            CreateMap<Contato, ContatoReadDTO>();
            CreateMap<ContatoCreateDTO, Contato>();
        }
    }
}
