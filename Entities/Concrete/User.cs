using Core.Entities.Concrete.EntityFramework;

namespace Entities.Concrete
{
    public class User : AppUser
    {
        public string PhotoUrl { get; set; }
    }
}
