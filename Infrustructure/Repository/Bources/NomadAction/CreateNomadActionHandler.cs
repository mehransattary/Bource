
using Application.DTO.Response;
using Application.Services.Bources.Command.NomadAction;
using Infrustructure.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrustructure.Repository.Bources.NomadAction;

public class CreateNomadActionHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) : 
    IRequestHandler<CreateNomadActionCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(CreateNomadActionCommand request, CancellationToken cancellationToken)
    {
		try
		{
            using var dbContext = contextFactory.CreateDbContext();

            var nomadAction = await dbContext.NomadActions.FirstOrDefaultAsync(_ => _.Name.ToLower().Equals(request.addNomadActionRequestDTO.Name), cancellationToken);

            if (nomadAction != null)
            {
                return GeneralDbResponse.ItemAlreadyExist(request.addNomadActionRequestDTO.Name);
            }

            var data = request.addNomadActionRequestDTO.Adapt(new Domain.Entittes.Bource.NomadAction());

            dbContext.NomadActions.Add(data);

            await dbContext.SaveChangesAsync(cancellationToken);

            return GeneralDbResponse.ItemCreated(request.addNomadActionRequestDTO.Name);
        }
		catch (Exception ex)
		{
            return new ServiceResponse(true, ex.Message);
        }
    }
}
