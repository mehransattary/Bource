using Application.DTO.Response;
using Application.Services.Bources.Command.Nomad;
using Infrustructure.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrustructure.Repository.Bources.Nomad.Handler;

public class DeleteNomadHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) : 
    IRequestHandler<DeleteNomadCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(DeleteNomadCommand request, CancellationToken cancellationToken)
    {
        try
        {
            using var dbContext = contextFactory.CreateDbContext();
            var nomad = await dbContext.Nomads.FirstOrDefaultAsync(_ => _.Id == request.Id, cancellationToken);
            if (nomad == null)
            {
                return GeneralDbResponse.ItemNotFound(request.Id.ToString());
            }

            dbContext.Nomads.Remove(nomad);
            await dbContext.SaveChangesAsync(cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);
            return GeneralDbResponse.ItemDelete(nomad.Name);
        }
        catch (Exception ex)
        {
            return new ServiceResponse(true, ex.Message);
        }
    }
}

