using Aurible.Models;

namespace AuribleDotnet_back.Service.AuthServices
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public User? GetUserInfo(string id){
            return _context.Users.First(u => u.IdMicrosoft == id);
        }
        public bool CreateUser(User user){
            try{
                _context.Users.Add(user);
                _context.SaveChanges();
                return true;
            }catch(Exception e){
                Console.WriteLine(e);
                return false;
            }
        }
        public bool HasExist(string id){
            try{
                int user = _context.Users.Where(u => u.IdMicrosoft == id).Select(u => u.IdUser).FirstOrDefault();
                if(user > 0){
                    return true;
                }
                return false;
            }catch(Exception e){
                Console.WriteLine(e);
                return false;
            }
        }
        public bool IsAdmin(string id){
            try{
                int role = _context.Users.Where(u => u.IdMicrosoft == id).Select(u => u.Role).FirstOrDefault();
                if(role == 1){
                    return true;
                }
            return false;
            }catch(Exception e){
                Console.WriteLine(e);
                return false;
            }
        }
    }
}