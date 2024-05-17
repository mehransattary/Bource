using Application.DTO.Request.Bources;
using Application.DTO.Response;
using MediatR;

namespace Application.Services.Bources.Command.NomadAction;
public record UpdateNomadActionCommand(UpdateNomadActionRequestDTO updateNomadActionRequestDTO) : IRequest<ServiceResponse>;
