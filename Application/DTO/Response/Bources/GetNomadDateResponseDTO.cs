using Domain.Entittes.Bource;
using System.Text.Json.Serialization;

namespace Application.DTO.Response.Bources;

public class GetNomadDateResponseDTO
{
    public string Code { get; set; }
    public DateTime Date { get; set; }
    public int NomadId { get; set; }
    public Nomad? Nomad { get; set; }

    [JsonIgnore]
    public virtual ICollection<NomadAction> NomadActions { get; set; } = null;
}
