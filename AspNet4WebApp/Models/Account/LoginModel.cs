using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNet4WebApp.Models.Account
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; }
        [PasswordPropertyText]
        public string Password { get; set; }
    }
}