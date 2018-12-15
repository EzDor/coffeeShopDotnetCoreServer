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

        public Status(string message, bool success)
        {
            this.message = message;
            this.success = success;
        }
    }
}