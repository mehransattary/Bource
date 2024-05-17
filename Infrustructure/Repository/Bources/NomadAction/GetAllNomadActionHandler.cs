
using Application.DTO.Response.Bources;
using Application.Services.Bources.Queries.Nomad;
using Application.Services.Bources.Queries.NomadAction;
using Infrustructure.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrustructure.Repository.Bources.NomadAction;

public class GetAllNomadActionHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) :
    IRequestHandler<GetAllNomadActionQuery, IEnumerable<GetNomadActionResponseDTO>>
{
    public async Task<IEnumerable<GetNomadActionResponseDTO>> Handle(GetAllNomadActionQuery request, CancellationToken cancellationToken)
    {
        using var dbContext = contextFactory.CreateDbContext();
        var data = await dbContext.NomadActions.AsNoTracking().ToListAsync(cancellationToken);
        return data.Adapt<List<GetNomadActionResponseDTO>>();
    }
}
