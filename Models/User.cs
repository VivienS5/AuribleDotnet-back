namespace Aurible.Models;
    public class User
    {
        public required int IdUser { get; set; }
        public required string RefreshToken { get; set; }
        public required DateTime ExpirationDate { get; set; }
        public int Role { get; set; } = 0;  // 0 = User, 1 = Admin
    }
