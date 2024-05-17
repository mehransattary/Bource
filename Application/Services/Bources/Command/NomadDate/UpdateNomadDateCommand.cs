using Application.DTO.Request.Bources;
using Application.DTO.Response;
using MediatR;

namespace Application.Services.Bources.Command.NomadDate;

public record UpdateNomadDateCommand(UpdateNomadDateRequestDTO updateNomadDateRequestDTO) : IRequest<ServiceResponse>;



