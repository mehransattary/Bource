using Application.DTO.Response;
using MediatR;

namespace Application.Services.Bources.Command.Nomad;

public record DeleteNomadCommand(Guid Id) : IRequest<ServiceResponse>;

