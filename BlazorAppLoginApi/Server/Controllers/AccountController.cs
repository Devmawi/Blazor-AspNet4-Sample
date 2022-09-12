using BlazorAppLoginApi.Server.Models.Account;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Server.HttpSys;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace BlazorAppLoginApi.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AccountController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        [Route("ExternalLogin")]
        [HttpGet]
        public string ExternalLogin()
        {
            return _antiforgery.GetTokens(HttpContext).RequestToken;
        }

        [Route("Login")]
        [HttpPost]
        //[ValidateAntiForgeryToken()]
        public IActionResult Login([FromForm]LoginModel model, [FromQuery]string redirectUrl= "/FetchData")
        {
            if (!ModelState.IsValid)
            {
                return LocalRedirect("/Login");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                model.Username == "Admin" ? new Claim(ClaimTypes.Role, "Admin") : new Claim(ClaimTypes.Role, "User"),
                new Claim(ClaimTypes.Name, model.Username)
            };
            
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return NoContent();
        }

        [Route("SignOut")]
        [HttpGet]
        public ActionResult GetSignOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }

        [HttpGet]
        [Route("UserInfo")]
        [Authorize]    
        public UserInfo GetUserInfo()
        {
            var user = HttpContext.User.Identity as ClaimsIdentity;
            var username = user?.Name;
            var role = user.FindFirst(user.RoleClaimType).Value;
            return new UserInfo()
            {
                Username = username,
                Role = role
            };
        }
    }
}
