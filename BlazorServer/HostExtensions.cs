using Blazor.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Totem.App.Web;
using Totem.Timeline.SignalR;
using Totem.Timeline.SignalR.Hosting;

namespace BlazorServer
{
    public static class HostExtensions
    {

        public static ConfigureWebApp ClientUIServices(this ConfigureWebApp configure)
        {
            return configure.BeforeServices((context, services) =>
            {
                services.AddServerSideBlazor();
                // Informs Totem that we will subscribe to events later on from the client via SignalR.
                services.AddSignalR().AddQueryNotifications();
                services.AddTransient<HubConnectionBuilder>();
            });
        }

        public static ConfigureWebApp BlazorWebApplication(this ConfigureWebApp configure)
        {
            return configure.BeforeApp(app =>
            {
                var developerMode = app.ApplicationServices
                    .GetRequiredService<IHostEnvironment>().IsDevelopment();
                if (developerMode)
                {
                    app.UseDeveloperExceptionPage();
                    app.UseBlazorDebugging();
                }
                app.UseClientSideBlazorFiles<AutoBlazor.Startup>();
                app.UseAuthentication();
                app.UseAuthorization();
                app.UseRouting();
                app.UseCors();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                    endpoints.MapRazorPages();
                    endpoints.MapBlazorHub();
                    endpoints.MapHub<QueryHub>("/hubs/query");
                    endpoints.MapDefaultControllerRoute();
                    endpoints.MapControllerRoute("Imports", "{controller=Imports}/{action=StartImport}");
                    endpoints.MapControllerRoute("ImportTest", "{controller=Imports}/{action=Test}");
                    endpoints.MapFallbackToClientSideBlazor<AutoBlazor.Startup>("index.html");
                });
            });
        }
    }
}
