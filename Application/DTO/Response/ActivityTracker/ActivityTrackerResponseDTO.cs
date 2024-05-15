using System.ComponentModel.DataAnnotations;

namespace Application.DTO.Response.ActivityTracker;

public class ActivityTrackerResponseDTO : BaseActivityTracker
{
    [Required]
    public string Username { get; set; }
}
