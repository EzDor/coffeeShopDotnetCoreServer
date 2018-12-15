namespace WebApplication.Controllers.Forms.Users
{
    public class LoginResponseParams
    {
        public string username { get; set; }
        public object token { get; set; }
        public bool? isAdmin { get; set; }
     
    }
}