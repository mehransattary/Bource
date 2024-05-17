using Application.DTO.Response.Bources;
using MediatR;

namespace Application.Services.Bources.Queries.NomadDate;

public class GetAllNomadDateQuery : IRequest<IEnumerable<GetNomadDateResponseDTO>>
{
}
