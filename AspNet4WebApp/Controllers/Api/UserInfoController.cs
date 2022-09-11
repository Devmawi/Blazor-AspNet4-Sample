using AspNet4WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace AspNet4WebApp.Controllers
{
    [Authorize(Users = "Admin, Otto")]
    public class UserInfoController : ApiController
    {
        [HttpGet]
        public UserInfo Get()
        {
            var user = new ClaimsPrincipal(HttpContext.Current.User);
            var role = user.FindFirst(ClaimTypes.Role);
            return new UserInfo()
            {
                Username = user.Identity.Name,
                Role = role.Value
            };
        }
    }
}
