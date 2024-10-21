namespace Aurible.Models;
    public class User
    {
        public required int IdUser { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string IdMicrosoft { get; set; }
        public  string? RefreshToken { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public required int Role { get; set; } = 0;  // 0 = User, 1 = Admin
    }
    public class UserLogin{
        public required string Name { get; set; }
        public required string Email { get; set; }
    }
