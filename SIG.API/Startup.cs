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
        public Startup()
        {
        }

        IConfiguration Configuration { get; set; }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            // Get the azure function application directory. 'C:\whatever' for local and 'd:\home\whatever' for Azure
            var executionContextOptions = builder.Services.BuildServiceProvider()
                .GetService<IOptions<ExecutionContextOptions>>().Value;

            var currentDirectory = executionContextOptions.AppDirectory;

            // Get the original configuration provider from the Azure Function
            var configuration = builder.Services.BuildServiceProvider().GetService<IConfiguration>();

            // Create a new IConfigurationRoot and add our configuration along with Azure's original configuration 
            Configuration = new ConfigurationBuilder()
                .SetBasePath(currentDirectory)
                .AddEnvironmentVariables()
                .AddConfiguration(configuration) // Add the original function configuration 
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            // Replace the Azure Function configuration with our new one
            builder.Services.AddSingleton(Configuration);

            ConfigureServices(builder.Services);
        }

        private void ConfigureServices(IServiceCollection services)
        {

            services.AddOpenIDConnect(config =>
            {
                config.SetTokenValidation(TokenValidationParametersHelpers.Default("sig-function-api.myvtmi.im", "https://dev-regf5o5f.us.auth0.com/"));
                config.SetIssuerBaseUrlConfiguration("https://dev-regf5o5f.us.auth0.com/");
            });

        }
    }
}