
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Application.Extensions.Identity;
using Domain.Entittes.ActivityTracker;
using Domain.Entittes.Bource;

namespace Infrustructure.DataAccess;

public class AppDbContext (DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Tracker> ActivityTracker { get; set; }
    public DbSet<Nomad> Nomads { get; set; }
    public DbSet<NomadDate> NomadDates { get; set; }
    public DbSet<NomadAction> NomadActions { get; set; }
}
