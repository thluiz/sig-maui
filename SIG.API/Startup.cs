using AzureFunctions.Extensions.OpenIDConnect.Configuration;
using AzureFunctions.Extensions.OpenIDConnect.InProcess.Configuration;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using SIG.API;
using ConfigurationBuilder = Microsoft.Extensions.Configuration.ConfigurationBuilder;

[assembly: FunctionsStartup(typeof(Startup))]

namespace SIG.API
{
    public class Startup : FunctionsStartup
    {
        IConfiguration Configuration { get; set; }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddOpenIDConnect(config =>
            {
                config.SetTokenValidation(TokenValidationParametersHelpers.Default("sig-function-api.myvtmi.im", "https://dev-regf5o5f.us.auth0.com/"));
                config.SetIssuerBaseUrlConfiguration("https://dev-regf5o5f.us.auth0.com/");
            });
        }
    }
}