using Microsoft.FluentUI.AspNetCore.Components;
using Shared.Abstract;
using Shared.Senders;
using Shifts.WebServices.Components;
using Shifts.WebServices.Components.Shared;
using Shifts.WebServices.Data;
using System.Net;
using System.Net.Mail;
using System.Reflection;

namespace Shifts
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddCascadingAuthenticationState();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSingleton<NavbarItemProvider>();

            var smtpSettings = GetSmtpSettings(builder.Configuration);
            builder.Services.AddSingleton(new MailAddress(smtpSettings.Email, smtpSettings.Name));

            builder.Services.AddScoped(_ => GetSmtpClient(smtpSettings));
            builder.Services.AddScoped<EmailSender>();
            builder.Services.AddFluentUIComponents();
            builder.Services.AddHttpClient();

            builder.Services.AddFluentUIComponents();
            builder.Services.AddHttpClient();

            var serviceRegisters = GetShiftsAssemblies()
                .Distinct()
                .SelectMany(x => x.GetTypes())
                .Where(x => !x.IsAbstract && x.GetInterface(nameof(IServiceRegister)) is not null && x.GetConstructor(BindingFlags.Public | BindingFlags.Instance, Array.Empty<Type>()) is not null)
                .Select(x => (IServiceRegister?)Activator.CreateInstance(x))
                .Where(x => x is not null)
                .ToArray();

            foreach (var serviceRegister in serviceRegisters)
                serviceRegister!.RegisterServicesToBuilder(builder);


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            foreach (var serviceRegister in serviceRegisters)
                serviceRegister!.RegisterIntoApp(app);

            app.Run();
        }

        private static IEnumerable<Assembly> GetShiftsAssemblies()
        {
            var assembly = Assembly.GetEntryAssembly();
            if (assembly is null)
                yield break;

            Queue<Assembly> queue = new();
            Stack<Assembly> stack = new();
            queue.Enqueue(assembly);
            stack.Push(assembly);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                var newAssemblies = current.GetReferencedAssemblies()
                    .Where(x => x.FullName.ToLower().StartsWith("shifts"))
                    .Select(x => Assembly.Load(x));

                foreach (var newAssebly in newAssemblies)
                {
                    stack.Push(newAssebly);
                    queue.Enqueue(newAssebly);
                }

            }

            while (stack.Count > 0)
                yield return stack.Pop();
        }

        private static SmtpSettings GetSmtpSettings(IConfiguration config)
        {
            var settings = config.GetSection("SmtpSettings")?.Get<SmtpSettings>();

            if (settings is null)
                throw new ArgumentException(nameof(SmtpSettings));

            return settings;
        }

        private static SmtpClient GetSmtpClient(SmtpSettings settings)
        {
            SmtpClient client = new(settings.Url, settings.Port);
            client.Credentials = new NetworkCredential(settings.Login, settings.Password);
            client.EnableSsl = settings.UseSsl;

            return client;
        }
    }
}
