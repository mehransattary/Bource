using Application.DTO.Response;
using Application.Services.Bources.Command.NomadDate;
using Infrustructure.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrustructure.Repository.Bources.NomadDate;

public class CreateNomadDateHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) :
    IRequestHandler<CreateNomadDateCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(CreateNomadDateCommand request, CancellationToken cancellationToken)
    {
        try
        {
            using var dbContext = contextFactory.CreateDbContext();

            var nomadAction = await dbContext.NomadDates.FirstOrDefaultAsync(_ => _.Name.ToLower().Equals(request.addNomadDateRequestDTO.Name), cancellationToken);

            if (nomadAction != null)
            {
                return GeneralDbResponse.ItemAlreadyExist(request.addNomadDateRequestDTO.Name);
            }

            var data = request.addNomadDateRequestDTO.Adapt(new Domain.Entittes.Bource.NomadDate());

            dbContext.NomadDates.Add(data);

            await dbContext.SaveChangesAsync(cancellationToken);

            return GeneralDbResponse.ItemCreated(request.addNomadDateRequestDTO.Name);
        }
        catch (Exception ex)
        {
            return new ServiceResponse(true, ex.Message);
        }
    }
}
