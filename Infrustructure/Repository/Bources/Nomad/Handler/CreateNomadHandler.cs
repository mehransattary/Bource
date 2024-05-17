
using Application.DTO.Response;
using Application.Services.Bources.Command.Nomad;
using Domain.Entittes.Bource;
using Infrustructure.DataAccess;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Infrustructure.Repository.Bources.Nomad.Handler;

public class CreateNomadHandler(DataAccess.IDbContextFactory<AppDbContext> contextFactory) : 
	IRequestHandler<CreateNomadCommand, ServiceResponse>
{
    public async Task<ServiceResponse> Handle(CreateNomadCommand request, CancellationToken cancellationToken)
    {
		try
		{
			using var dbContext = contextFactory.CreateDbContext();

			var nomad = await dbContext.Nomads.FirstOrDefaultAsync(_=>_.Name.ToLower().Equals(request.addNomadRequestDTO.Name),cancellationToken);

			if(nomad != null)
			{
				return GeneralDbResponse.ItemAlreadyExist(request.addNomadRequestDTO.Name);
			}

			var data = request.addNomadRequestDTO.Adapt(new Domain.Entittes.Bource.Nomad());

			dbContext.Nomads.Add(data);

			await dbContext.SaveChangesAsync(cancellationToken);

			return GeneralDbResponse.ItemCreated(request.addNomadRequestDTO.Name);

        }
		catch (Exception ex)
		{

			return new ServiceResponse(true, ex.Message);
		}
    }
}
