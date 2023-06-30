using Microsoft.AspNetCore.Identity;
namespace Entrytask_Urban.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool authorized;

        
    }
}
