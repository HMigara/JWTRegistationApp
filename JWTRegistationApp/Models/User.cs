namespace JWTRegistationApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string DocumentPath { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
