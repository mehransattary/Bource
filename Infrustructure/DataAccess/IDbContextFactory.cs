using Microsoft.EntityFrameworkCore;

namespace Infrustructure.DataAccess;

public interface IDbContextFactory<T> where T : DbContext
{
    T CreateDbContext();
}
