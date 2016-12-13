using System;
using System.Net;
using System.Net.Sockets;
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
                // Gather values for frontend service 
                string appVersion = "1.1.0";
                string frontendName = Environment.MachineName;
                var ips = Dns.GetHostAddressesAsync(frontendName).Result;
                var addresses = "";

                foreach (var ip in ips)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        addresses = addresses + " / " + ip;
                    }
                }
                string frontendIP = addresses.Remove(0,3);
                
                // Call backend API container to gather values
                string backend_svc_ip = Environment.GetEnvironmentVariable("BACKEND_IP");
                string backend_svc_port = Environment.GetEnvironmentVariable("BACKEND_PORT");
                
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://" + backend_svc_ip + ":" + backend_svc_port);
                string backendIP = await client.GetStringAsync("/config/getip");
                string backendName = await client.GetStringAsync("/config/getname");

                await context.Response.WriteAsync("<h1>Simple Web App (DC/OS)</h1>");
                await context.Response.WriteAsync("<p>App version: " + appVersion);
                await context.Response.WriteAsync("<br>");
                await context.Response.WriteAsync("<p>Frontend: " + frontendName + " / " + frontendIP);
                await context.Response.WriteAsync("<br>");
                await context.Response.WriteAsync("<p>Backend: " + backendName + " / " + backendIP);

            });
        }
    }
}
