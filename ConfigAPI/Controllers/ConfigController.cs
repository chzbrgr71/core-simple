using System;
using Microsoft.AspNetCore.Mvc;
 
namespace ConfigAPI.Controllers 
{ 
    [Route("config")]
    
    public class ButtheadController : Controller 
    {   
        [HttpGet("getip")]
        
        public ContentResult GetMachineIP()
        {
            // Gather values for result
            string machineIP = "192.168.1.1";
            return Content(machineIP);
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