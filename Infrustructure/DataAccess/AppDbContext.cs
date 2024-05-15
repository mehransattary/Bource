
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Application.Extensions.Identity;

namespace Infrustructure.DataAccess;

public class AppDbContext (DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{

}
