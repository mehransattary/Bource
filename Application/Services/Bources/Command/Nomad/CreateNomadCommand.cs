using Application.DTO.Request.Bources;
using Application.DTO.Response;
using MediatR;

namespace Application.Services.Bources.Command.Nomad;

public record CreateNomadCommand(AddNomadRequestDTO addNomadRequestDTO) : IRequest<ServiceResponse>;
    
    

