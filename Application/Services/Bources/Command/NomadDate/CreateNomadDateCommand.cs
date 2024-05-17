using Application.DTO.Request.Bources;
using Application.DTO.Response;
using MediatR;

namespace Application.Services.Bources.Command.NomadDate;

public record CreateNomadDateCommand(AddNomadDateRequestDTO addNomadDateRequestDTO):IRequest<ServiceResponse>;

