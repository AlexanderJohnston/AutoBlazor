using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Totem.App.Web;

namespace BlazorServer
{
    public class Program
    {
        public static Task Main()
        {
            // Original method
            //CreateHostBuilder(args).Build().Run();

            // Totem method
            var configuration = new ConfigureWebApp();
            return WebApp.Run<ServerArea>(configuration
                .BlazorWebApplication()
                .ClientUIServices());
        }

    }
}
