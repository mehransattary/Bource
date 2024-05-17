
using System.ComponentModel.DataAnnotations;

namespace Application.DTO.Request.Bources;

public class UpdateNomadDateRequestDTO : BaseBorceRequestDTO
{
    [Required]
    public DateTime Date { get; set; }

    [Required]  
    public int NomadId { get; set; }
}
