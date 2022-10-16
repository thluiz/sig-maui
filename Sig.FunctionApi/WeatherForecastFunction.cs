using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Sig.FunctionApi;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Xml.Linq;
using SIG.Shared;

namespace Sig.FunctionApi
{
    public class WeatherForecastFunction
    {
        public readonly WeatherForecastService Service;

        public WeatherForecastFunction()
        {
            Service = new WeatherForecastService();
        }


        [Authorize]
        [FunctionName("WeatherForecast")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {

            var result = await Service.GetForecastAsync(DateTime.Now);

            var userId = req.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            return new OkObjectResult(result.Select(r =>
            {
                r.Summary = userId;
                return r;
            }));
        }
    }
}
