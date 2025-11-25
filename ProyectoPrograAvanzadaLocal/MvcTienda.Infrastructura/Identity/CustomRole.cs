using Microsoft.AspNet.Identity.EntityFramework;
using MvcTienda.Infrastructura.Identity;

public class CustomRole : IdentityRole<int, CustomUserRole>
{
    public CustomRole() { }
    public CustomRole(string name)
    {
        Name = name;
    }
}
