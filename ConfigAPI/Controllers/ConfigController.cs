using System;
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
            string machineIP = "TBD";
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