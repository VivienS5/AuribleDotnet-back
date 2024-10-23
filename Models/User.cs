namespace Aurible.Models;
    public class User
    {
        public required int IdUser { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string IdMicrosoft { get; set; }
        public required int Role { get; set; } = 0; 
    }
    public class UserLogin{
        public required string Name { get; set; }
        public required string Email { get; set; }
    }
