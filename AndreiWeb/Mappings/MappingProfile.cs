using AndreiWeb.Models;
using AndreiWeb.Models.ViewModels;
using AutoMapper;

namespace AndreiWeb.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ApplicationUser, OrderHeader>();
    }
}