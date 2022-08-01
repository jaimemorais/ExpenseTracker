namespace ExpenseTrackerApp.Services
{
    public class User
    {
        public string Email { get; set; }
        public string Token { get; internal set; }
    }
}