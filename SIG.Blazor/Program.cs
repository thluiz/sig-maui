using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SIG.Blazor;
using SIG.Blazor.Data;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<SigApiAuthorizationMessageHandler>();

var baseAddress = builder.Configuration["API_Prefix"] ?? builder.HostEnvironment.BaseAddress;

builder.Services.AddHttpClient("SIG.API",
        client => client.BaseAddress = new Uri(baseAddress))
    .AddHttpMessageHandler((s) =>
    {
        var accessTokenProvider = s.GetRequiredService<IAccessTokenProvider>();
        var navigationManager = s.GetRequiredService<NavigationManager>();

        return new SigApiAuthorizationMessageHandler(accessTokenProvider, navigationManager, baseAddress);
    });


builder.Services.AddOidcAuthentication(options =>
{
    builder.Configuration.Bind("Auth0", options.ProviderOptions);
    options.ProviderOptions.ResponseType = "code";
    options.ProviderOptions.AdditionalProviderParameters.Add("audience", builder.Configuration["Auth0:Audience"]);
});


await builder.Build().RunAsync();
