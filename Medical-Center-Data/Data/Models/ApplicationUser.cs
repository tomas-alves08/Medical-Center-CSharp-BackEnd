
using Microsoft.AspNetCore.Identity;

namespace Medical_Center_Data.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
