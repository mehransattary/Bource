using Application.DTO.Response.Bources;
using Application.Services.Bources.Queries.NomadDate;
using Infrustructure.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrustructure.Repository.Bources.NomadDate;

public class GetAllNomadDateHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) :
    IRequestHandler<GetAllNomadDateQuery, IEnumerable<GetNomadDateResponseDTO>>
{
    public async Task<IEnumerable<GetNomadDateResponseDTO>> Handle(GetAllNomadDateQuery request, CancellationToken cancellationToken)
    {
        using var dbContext = contextFactory.CreateDbContext();
        var data = await dbContext.NomadDates.AsNoTracking().ToListAsync(cancellationToken);
        return data.Adapt<List<GetNomadDateResponseDTO>>();
    }
}
