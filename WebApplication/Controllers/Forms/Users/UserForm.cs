using WebApplication.Models.Statuses;

namespace WebApplication.Controllers.Forms.Users
{
    public class UserForm
    {
        public string firstName { get; set; }
        
        public string lastName { get; set; }
        
        public string username { get; set; }
        
        public string password { get; set; }
        
        public UserStatus status { get; set; }
        
        public bool admin { get; set; }
    }
}