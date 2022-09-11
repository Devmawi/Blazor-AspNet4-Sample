using AspNet4WebApp.Models.Account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Cors;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace AspNet4WebApp.Controllers
{
    [Authorize]
    [EnableCors("*")]
    public class AccountController : Controller
    {
        public IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl="/Home/Index")
        {
            var user = HttpContext.User;
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (!ModelState.IsValid || !ValidateLogin(model))
            {
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, "Administrator"),
                new Claim(ClaimTypes.Name, model.Username)
            };
            var claimsIdentity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(claimsIdentity);

            return RedirectToLocal(returnUrl);
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        private bool ValidateLogin(LoginModel model)
        {
            return !String.IsNullOrEmpty(model.Username) && !String.IsNullOrEmpty(model.Password);
        }
    }
}