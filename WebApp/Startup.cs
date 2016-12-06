using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace WebApp
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                // Call API backend container to get values
                //backend_svc_ip = Environment.GetEnvironmentVariable("BACKEND_IP");
                string backend_svc_ip = "http://localhost:5001";

                var client = new HttpClient();
                client.BaseAddress = new Uri(backend_svc_ip);
                
                string appVersion = "1.0.5";
                
                string frontendName = Environment.MachineName;
                //string frontendIP = Dns.GetHostAddressesAsync(frontendName);
                string frontendIP = "10.10.10.1";
                
                string backendIP = await client.GetStringAsync("/config/getip");
                string backendName = await client.GetStringAsync("/config/getname");

                await context.Response.WriteAsync("<h1>Simple Web App</h1>");
                await context.Response.WriteAsync("<p>App version: " + appVersion);
                await context.Response.WriteAsync("<br>");
                await context.Response.WriteAsync("<p>Frontend: " + frontendName + " / " + frontendIP);
                await context.Response.WriteAsync("<br>");
                await context.Response.WriteAsync("<p>Backend: " + backendName + " / " + backendIP);

            });
        }
    }
}
