

namespace WEB.Models
{
    public class AppUser: Microsoft.AspNetCore.Identity.IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDay { get; set; }
    }
}
