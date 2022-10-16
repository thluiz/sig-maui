namespace SIG.Shared.Models;
using Newtonsoft.Json;

public class LoginRequest
{
    [JsonRequired]
    public string UserName { get; set; }
    [JsonRequired]
    public string Password { get; set; }
    public bool RememberMe { get; set;  }
}