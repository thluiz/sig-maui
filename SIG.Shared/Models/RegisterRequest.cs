using Newtonsoft.Json;

namespace SIG.Shared.Models;

public class RegisterRequest
{
    [JsonRequired]
    public string UserName { get; set; }
    [JsonRequired]
    public string Password { get; set; }
    [JsonRequired]
    //[Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string PasswordConfirm { get; set; }
}