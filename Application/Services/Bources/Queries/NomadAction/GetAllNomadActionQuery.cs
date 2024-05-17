
using Application.DTO.Response.Bources;
using MediatR;

namespace Application.Services.Bources.Queries.NomadAction;

public class GetAllNomadActionQuery : IRequest<IEnumerable<GetNomadActionResponseDTO>>
{
}
