namespace Aurible.Models;
    public class User
    {
        public required int IdUser { get; set; }
        public required string Title {get; set;}
        public required string Author {get;set;}
        public required string AccessToken {get; set;}
        public required string RefreshToken {get; set;}
        public required DateTime ExpirationDate {get; set;}
        
    }
