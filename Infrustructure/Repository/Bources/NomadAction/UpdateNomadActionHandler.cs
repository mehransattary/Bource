
using Application.DTO.Response;
using Application.Services.Bources.Command.NomadAction;
using Infrustructure.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrustructure.Repository.Bources.NomadAction;

public class UpdateNomadActionHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) :
    IRequestHandler<UpdateNomadActionCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(UpdateNomadActionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            using var dbContext = contextFactory.CreateDbContext();
            var nomad = dbContext.NomadActions.FirstOrDefaultAsync(_ => _.Id.Equals(request.updateNomadActionRequestDTO.Id), cancellationToken);
            if (nomad == null)
            {
                return GeneralDbResponse.ItemNotFound(request.updateNomadActionRequestDTO.Name);
            }

            dbContext.Entry(nomad).State = EntityState.Detached;
            var adaptData = request.updateNomadActionRequestDTO.Adapt(new Domain.Entittes.Bource.NomadAction());
            dbContext.NomadActions.Update(adaptData);
            await dbContext.SaveChangesAsync(cancellationToken);
            return GeneralDbResponse.ItemUpdate(request.updateNomadActionRequestDTO.Name);

        }
        catch (Exception ex)
        {
            return new ServiceResponse(true, ex.Message);
        }
    }
}
