using System.Text.Json.Serialization;

namespace Domain.Entittes.Bource;

public class Nomad : BourceBase
{  

    [JsonIgnore]
    public virtual ICollection<NomadDate> NomadDates { get; set; } = null;
}

