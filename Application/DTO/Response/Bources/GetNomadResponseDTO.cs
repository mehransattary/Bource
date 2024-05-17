namespace Application.DTO.Response.Bources;

public class GetNomadResponseDTO
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; }
}
