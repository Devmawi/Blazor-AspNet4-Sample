using System.ComponentModel;

namespace BlazorAppLoginApi.Server.Models.Account
{
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        [DisplayName("__RequestVerificationToken")]
        public string __RequestVerificationToken { get; set; }
    }
}
