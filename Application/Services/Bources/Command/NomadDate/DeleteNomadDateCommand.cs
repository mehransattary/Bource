using Application.DTO.Response;
using MediatR;

namespace Application.Services.Bources.Command.NomadDate;

public record DeleteNomadDateCommand(Guid Id) : IRequest<ServiceResponse>;

