using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace SportsStore.Models
{
    public class StoreIdentityContext:IdentityDbContext<IdentityUser>
    {
        public StoreIdentityContext(DbContextOptions<StoreIdentityContext> options) : base(options) { }
    }
}
