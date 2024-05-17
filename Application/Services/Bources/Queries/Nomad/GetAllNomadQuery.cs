using Application.DTO.Response.Bources;
using MediatR;


namespace Application.Services.Bources.Queries.Nomad;

public class GetAllNomadQuery : IRequest<IEnumerable<GetNomadResponseDTO>>
{
}
