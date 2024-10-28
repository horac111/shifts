using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Shared.Abstract;
using Shifts.BL.Abstract.Services;
using Shifts.BL.Services;
using Shifts.BL.Tools.AutoMapper;

namespace Shifts.BL.Tools;
public class RegisteBLServices : IServiceRegister
{
    public void RegisterIntoApp(WebApplication app)
    {
    }

    public void RegisterServicesToBuilder(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IManageUsersService, ManageUsersService>();
        builder.Services.AddScoped<IShiftDaysService, ShiftDaysService>();
        builder.Services.AddAutoMapper(conf => conf.AddProfile<Configuration>());
    }
}
