using Application.DTO.Response;
using Application.Services.Bources.Command.Nomad;
using Infrustructure.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrustructure.Repository.Bources.Nomad.Handler;

public class UpdateNomadHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) : 
	IRequestHandler<UpdateNomadCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(UpdateNomadCommand request, CancellationToken cancellationToken)
    {
		try
		{
			using var dbContext = contextFactory.CreateDbContext();
			var nomad = dbContext.Nomads.FirstOrDefaultAsync(_ => _.Id.Equals(request.updateNomadRequestDTO.Id), cancellationToken);
			if(nomad == null)
			{
				return GeneralDbResponse.ItemNotFound(request.updateNomadRequestDTO.Name); 
			}

			dbContext.Entry(nomad).State = EntityState.Detached;
			var adaptData = request.updateNomadRequestDTO.Adapt(new Domain.Entittes.Bource.Nomad());
			dbContext.Nomads.Update(adaptData);
			await dbContext.SaveChangesAsync(cancellationToken);
			return GeneralDbResponse.ItemUpdate(request.updateNomadRequestDTO.Name);

		}
		catch (Exception ex)
		{
			return new ServiceResponse(true, ex.Message);
		}
    }
}
