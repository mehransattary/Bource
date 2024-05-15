using System.ComponentModel.DataAnnotations;

namespace Application.DTO.Response.ActivityTracker;

public class BaseActivityTracker
{
    [Required]
    [DataType(DataType.DateTime)]
    public DateTime Date { get; set; } = DateTime.Now.Date;

    [Required]
    public string Titile { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public bool OperationState { get; set; }

    [Required]
    public string UserId { get; set; }
}
