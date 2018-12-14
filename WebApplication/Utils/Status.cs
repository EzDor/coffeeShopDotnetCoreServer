namespace WebApplication.Utils
{
    public class Status
    {
        public string message;
        public bool success;

        public Status(string message)
        {
            this.message = message;
            this.success = true;
        }
    }
}