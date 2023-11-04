using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Concrete.EntityFramework
{
    public class AppUser : IdentityUser 
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
