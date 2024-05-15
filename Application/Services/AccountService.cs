using Application.DTO.Request.ActivityTracker;
using Application.DTO.Request.Identity;
using Application.DTO.Response;
using Application.DTO.Response.ActivityTracker;
using Application.DTO.Response.Identity;
using Application.Interface.Identity;
 
namespace Application.Services;

public class AccountService(IAccount account) : IAccountService
{
    public async Task<ServiceResponse> CreateUserAsync(CreateUserRequestDTO model)
    => await account.CreateUserAsync(model);

    public async Task<IEnumerable<GetUserWithClaimResponseDTO>> GetUserWithClaimsAsync()
    => await account.GetUserWithClaimsAsync();

    public async Task<ServiceResponse> LoginAsync(LoginUserRequestDTO model)
    => await account.LoginAsync(model);

    public async Task SetUpAsync()
    => await account.SetUpAsync();

    public async Task<ServiceResponse> UpdateUserAsync(ChangeUserClaimRequestDTO model)
    => await account.UpdateUserAsync(model);

    public async Task SaveActvityAsync(ActivityTrackerRequestDTO model)
    => await account.ServiceActivityAsync(model);

    public async Task<IEnumerable<IGrouping<DateTime, ActivityTrackerResponseDTO>>> GroupActivitiesAsync()
    {
        var data = (await GetActivitiesAsync()).GroupBy(e => e.Date).AsEnumerable();
        return data;
    }

    private async Task<IEnumerable<ActivityTrackerResponseDTO>> GetActivitiesAsync()
    => await account.GetActivitiesAsync();


}
