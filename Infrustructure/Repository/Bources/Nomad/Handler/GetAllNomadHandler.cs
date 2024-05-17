using Application.DTO.Response.Bources;
using Application.Services.Bources.Queries.Nomad;
using Infrustructure.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrustructure.Repository.Bources.Nomad.Handler;

public class GetAllNomadHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) : 
    IRequestHandler<GetAllNomadQuery, IEnumerable<GetNomadResponseDTO>>
{
    public async Task<IEnumerable<GetNomadResponseDTO>> Handle(GetAllNomadQuery request,CancellationToken cancellationToken)
    {
        using var dbContext = contextFactory.CreateDbContext();
        var data = await dbContext.Nomads.AsNoTracking().ToListAsync(cancellationToken);
        return data.Adapt<List<GetNomadResponseDTO>>();
    }
}
