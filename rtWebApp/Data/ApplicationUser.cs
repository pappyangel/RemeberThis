using Microsoft.AspNetCore.Identity;

namespace rtWebApp.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public bool IsPremium { get; set; }
    public bool IsAdmin { get; set; }
}

