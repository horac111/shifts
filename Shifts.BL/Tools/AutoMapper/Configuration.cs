using AutoMapper;
using Shifts.BL.Data.DTO;
using Shifts.DAL.Models;

namespace Shifts.BL.Tools.AutoMapper;
public class Configuration : Profile
{
    public Configuration()
    {
        CreateMap<UserDto, ApplicationUser>();
    }
}
