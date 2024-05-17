
using System.ComponentModel.DataAnnotations;

namespace Application.DTO.Request.Bources;

public class AddNomadDateRequestDTO : BaseBorceRequestDTO
{
    [Required]
    public DateTime Date { get; set; }

    [Required]  
    public int NomadId { get; set; }
}
