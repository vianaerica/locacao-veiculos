using AutoMapper;
using LocacaoVeiculosApi.Domain.Entities;

namespace LocacaoVeiculosApi.Presentation.ViewModel
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Categoria, CategoriaDto>();
            CreateMap<Marca, MarcaDto>();
            CreateMap<Modelo, ModeloDto>();
            
            CreateMap<CreateCategoriaDto, Categoria>();
            CreateMap<CreateMarcaDto, Marca>();
            CreateMap<CreateModeloDto, Modelo>();
        }
        
    }
}