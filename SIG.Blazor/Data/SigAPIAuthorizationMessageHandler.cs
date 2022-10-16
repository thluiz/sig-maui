using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace SIG.Blazor.Data;

public class SigApiAuthorizationMessageHandler : AuthorizationMessageHandler
{
    public SigApiAuthorizationMessageHandler(IAccessTokenProvider provider,
        NavigationManager navigationManager, string baseAddress)
        : base(provider, navigationManager)
    {
        ConfigureHandler(
            authorizedUrls: new[] { baseAddress },
            scopes: new[] { "User.Read" });
    }
}