using System;
using System.Net;
using System.Net.Sockets;
using Microsoft.AspNetCore.Mvc;
 
namespace ConfigAPI.Controllers 
{ 
    [Route("config")]
    
    public class ConfigController : Controller 
    {   
        [HttpGet("getip")]
        
        public ContentResult GetMachineIP()
        {
            // Gather values for result
            var hostName = Dns.GetHostName();
            var ips = Dns.GetHostAddressesAsync(hostName).Result;
            var addresses = "";

            foreach (var ip in ips)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    addresses = addresses + " / " + ip;
                }
            }
            addresses = addresses.Remove(0,3);
            return Content(addresses);
        }

        [HttpGet("getname")]
        
        public ContentResult GetMachineName()
        {
            // Gather values for result
            string machineName = Environment.MachineName;
            return Content(machineName);
        }

    }
    
} 