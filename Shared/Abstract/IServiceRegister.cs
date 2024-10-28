using Microsoft.AspNetCore.Builder;

namespace Shared.Abstract;
public interface IServiceRegister
{
    void RegisterServicesToBuilder(WebApplicationBuilder builder);

    void RegisterIntoApp(WebApplication app);
}
