

using Application.DTO.Request.Identity;
using Application.DTO.Response.Identity;
using Application.DTO.Response;
using Application.DTO.Request.ActivityTracker;
using Application.DTO.Response.ActivityTracker;

namespace Application.Services;

public interface IAccountService
{
    Task<ServiceResponse> LoginAsync(LoginUserRequestDTO model);

    Task<ServiceResponse> CreateUserAsync(CreateUserRequestDTO model);

    Task<IEnumerable<GetUserWithClaimResponseDTO>> GetUserWithClaimsAsync();

    Task SetUpAsync();

    Task<ServiceResponse> UpdateUserAsync(ChangeUserClaimRequestDTO model);

    Task SaveActvityAsync(ActivityTrackerRequestDTO model);

    Task<IEnumerable<IGrouping<DateTime, ActivityTrackerResponseDTO>>> GroupActivitiesAsync();
}
