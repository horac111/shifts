using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Abstract;
using Shifts.Components.Account;
using Shifts.DAL.Abstracts.Repository;
using Shifts.DAL.Models;
using Shifts.DAL.Repository;

namespace Shifts.DAL.Data;
public class RegisterDalService : IServiceRegister
{
    public void RegisterIntoApp(WebApplication app)
    {
        // Add additional endpoints required by the Identity /Account Razor components.
        //app.MapAdditionalIdentityEndpoints();

        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }

        app.UseAuthentication();

        app.UseAuthorization();
    }

    public void RegisterServicesToBuilder(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IdentityUserAccessor>();
        /*builder.Services.AddScoped<IdentityRedirectManager>();*/
        builder.Services.AddScoped<AuthenticationStateProvider, IdentityRevalidatingAuthenticationStateProvider>();

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityConstants.ApplicationScheme;
            options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        })
        .AddIdentityCookies();

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.Password.RequiredUniqueChars = 5;
                options.Password.RequiredLength = 8;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        builder.Services.AddScoped<IChangeRequestRepository, ChangeRequestRepository>();
        builder.Services.AddScoped<IOccurenceRepository, OccurenceRepository>();
        builder.Services.AddScoped<IShiftAssignmentRepository, ShiftAssignmentRepository>();
        builder.Services.AddScoped<IShiftDayRepository, ShiftDayRepository>();
        builder.Services.AddScoped<IShiftRepository, ShiftRepository>();
        builder.Services.AddScoped<IWorkingAvailabilityRepository, WorkingAvailabilityRepository>();

    }
}
