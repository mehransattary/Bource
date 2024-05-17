
using Application.DTO.Response;
using Application.Services.Bources.Command.NomadDate;
using Infrustructure.DataAccess;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrustructure.Repository.Bources.NomadDate;

public class DeleteNomadDateHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) :
    IRequestHandler<DeleteNomadDateCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(DeleteNomadDateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            using var dbContext = contextFactory.CreateDbContext();
            var nomadDates = await dbContext.NomadDates.FirstOrDefaultAsync(_ => _.Id == request.Id, cancellationToken);
            if (nomadDates == null)
            {
                return GeneralDbResponse.ItemNotFound(request.Id.ToString());
            }

            dbContext.NomadDates.Remove(nomadDates);
            await dbContext.SaveChangesAsync(cancellationToken);
            return GeneralDbResponse.ItemDelete(nomadDates.Name);
        }
        catch (Exception ex)
        {
            return new ServiceResponse(true, ex.Message);
        }
    }
}
