
using Application.DTO.Response;
using Application.Services.Bources.Command.NomadAction;
using Infrustructure.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrustructure.Repository.Bources.NomadAction;

public class DeleteNomadActionHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) :
    IRequestHandler<DeleteNomadActionCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(DeleteNomadActionCommand request, CancellationToken cancellationToken)
    {
		try
		{
            using var dbContext = contextFactory.CreateDbContext();
            var nomadAction = await dbContext.NomadActions.FirstOrDefaultAsync(_ => _.Id == request.Id, cancellationToken);
            if (nomadAction == null)
            {
                return GeneralDbResponse.ItemNotFound(request.Id.ToString());
            }

            dbContext.NomadActions.Remove(nomadAction);
            await dbContext.SaveChangesAsync(cancellationToken);
            return GeneralDbResponse.ItemDelete(nomadAction.Name);
        }
        catch (Exception ex)
        {
            return new ServiceResponse(true, ex.Message);
        }
    }
}
