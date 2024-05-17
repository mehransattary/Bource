
using Application.DTO.Response;
using MediatR;

namespace Application.Services.Bources.Command.NomadAction;

public record DeleteNomadActionCommand(Guid Id):IRequest<ServiceResponse>;

