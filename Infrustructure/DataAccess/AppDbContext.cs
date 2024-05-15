
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Application.Extensions.Identity;
using Domain.Entittes.ActivityTracker;

namespace Infrustructure.DataAccess;

public class AppDbContext (DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Tracker> ActivityTracker { get; set; }
}
