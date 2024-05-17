using Application.DTO.Response;
using Application.Services.Bources.Command.NomadDate;
using Infrustructure.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrustructure.Repository.Bources.NomadDate;

public class UpdateNomadDateHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) :
    IRequestHandler<UpdateNomadDateCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(UpdateNomadDateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            using var dbContext = contextFactory.CreateDbContext();
            var nomad = dbContext.NomadDates.FirstOrDefaultAsync(_ => _.Id.Equals(request.updateNomadDateRequestDTO.Id), cancellationToken);
            if (nomad == null)
            {
                return GeneralDbResponse.ItemNotFound(request.updateNomadDateRequestDTO.Name);
            }

            dbContext.Entry(nomad).State = EntityState.Detached;
            var adaptData = request.updateNomadDateRequestDTO.Adapt(new Domain.Entittes.Bource.NomadDate());
            dbContext.NomadDates.Update(adaptData);
            await dbContext.SaveChangesAsync(cancellationToken);
            return GeneralDbResponse.ItemUpdate(request.updateNomadDateRequestDTO.Name);

        }
        catch (Exception ex)
        {
            return new ServiceResponse(true, ex.Message);
        }
    }
}
