using System;

namespace WebApplication.Controllers.Forms
{
    public class LoginResponseParams
    {
        public string username { get; set; }
        public object token { get; set; }
        public bool? isAdmin { get; set; }
     
    }
}