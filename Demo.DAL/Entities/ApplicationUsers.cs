using Microsoft.AspNetCore.Identity;

namespace Demo.DAL.Entities;
public class ApplicationUsers : IdentityUser
{
    public string FristName { get; set; } = default!;
    public string LastName { get; set; } = default!;
}
