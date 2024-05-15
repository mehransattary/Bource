using Application.DTO.Request.Identity;
using Application.DTO.Response;
using Application.DTO.Response.Identity;
using Application.Extensions.Identity;
using Application.Interface.Identity;
using Infrustructure.DataAccess;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Security.Claims;

namespace Infrustructure.Repository;

public class Account(UserManager<ApplicationUser> userManager,
                     SignInManager<ApplicationUser> signInManager,
                     AppDbContext context) : IAccount
{
    public async Task<ServiceResponse> CreateUserAsync(CreateUserRequestDTO model)
    {

        var user = await FindUserByEmail(model.Email);

        if (user != null) 
        {
            return new ServiceResponse(false, "User already exist");
        }

        var newUser = new ApplicationUser()
        {
            UserName = model.Email,
            PasswordHash = model.Password,
            Email = model.Email,
            Name = model.Name
        };

        var result = CheckResult(await userManager.CreateAsync(newUser, model.Password));

        if(!result.Flag)
        {
            return result;
        }
        else
        {
            return await CreateUserClaims(model);
        }

    }    

    public async Task<IEnumerable<GetUserWithClaimResponseDTO>> GetUserWithClaimsAsync()
    {
        var userList = new List<GetUserWithClaimResponseDTO>();

        var allUser = await userManager.Users.ToListAsync();

        if(allUser.Count == 0)
        {
            return userList;        
        }

        foreach (var user in allUser) 
        {
            var currentUser = await userManager.FindByIdAsync(user.Id);
            var getCurrentUserClaims = await userManager.GetClaimsAsync(currentUser);

            if(getCurrentUserClaims.Any())
            {
                userList.Add(new GetUserWithClaimResponseDTO() 
                { 
                    UserId = user.Id,   
                    Email = getCurrentUserClaims.FirstOrDefault(_=>_.Type == ClaimTypes.Email).Value,
                    RoleName = getCurrentUserClaims.FirstOrDefault(_ => _.Type == ClaimTypes.Role).Value,
                    Name = getCurrentUserClaims.FirstOrDefault(_=>_.Type == "Name").Value,
                    ManagerUser = Convert.ToBoolean(getCurrentUserClaims.FirstOrDefault(_ => _.Type == "ManagerUser")),
                    Create = Convert.ToBoolean(getCurrentUserClaims.FirstOrDefault(_ => _.Type == "Create").Value),
                    Update = Convert.ToBoolean(getCurrentUserClaims.FirstOrDefault(_ => _.Type == "Update").Value),
                    Delete = Convert.ToBoolean(getCurrentUserClaims.FirstOrDefault(_ => _.Type == "Delete").Value),
                    Read = Convert.ToBoolean(getCurrentUserClaims.FirstOrDefault(_ => _.Type == "Read").Value),
                });
            }

        
        }
        return userList;
    }

    public async Task<ServiceResponse> LoginAsync(LoginUserRequestDTO model)
    {

        var user = await FindUserByEmail(model.Email);
        if (user is null)
        {
            return new ServiceResponse(false,"User not found");
        }

        var verifyPassword = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
        if(!verifyPassword.Succeeded)
        {
            return new ServiceResponse(false, "Unknow error occured while logging");
        }

        var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false );
        if(!result.Succeeded)
        {
            return new ServiceResponse(false, "Unknow error occured while logging");
        }

        return new ServiceResponse(true, null);
    }

    public async Task SetUpAsync()
    => await CreateUserAsync(new CreateUserRequestDTO()
        {
            Name="Administrator",
            Email ="admin@admin.com",
            Password ="Admin@123",
            Policy = Policy.AdminPolicy
        });

    public async Task<ServiceResponse> UpdateUserAsync(ChangeUserClaimRequestDTO model)
    {
        var user = await userManager.FindByIdAsync(model.UserId);

        if (user == null)
            return new ServiceResponse(false, "User not found");

        var oldUserClaims = await userManager.GetClaimsAsync(user);

        Claim[] newUserClaims =
        [
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, model.RoleName),
            new Claim("Name", model.Name),
            new Claim("Create", model.Create.ToString()),
            new Claim("Update", model.Update.ToString()),
            new Claim("ManagerUser", model.ManagerUser.ToString()),
            new Claim("Delete", model.Delete.ToString()),
        ];

        var result = await userManager.RemoveClaimsAsync(user,oldUserClaims);

        var response = CheckResult(result);

        if(!response.Flag)
        {
            return new ServiceResponse(false, response.Message);
        }

        var addNewClaims = await userManager.AddClaimsAsync(user, newUserClaims);

        var outcome = CheckResult(addNewClaims);

        if (outcome.Flag)
        {
            return new ServiceResponse(true, "User updated");
        }
        else
            return outcome;

    }

    private async Task<ServiceResponse> CreateUserClaims(CreateUserRequestDTO model)
    {
        if (string.IsNullOrEmpty(model.Policy))
        {
            return new ServiceResponse(false, "No policy specified");
        }

        Claim[] userCliams = [];

        if (model.Policy.Equals(Policy.AdminPolicy, StringComparison.OrdinalIgnoreCase))
        {
            userCliams =
            [
                 new Claim(ClaimTypes.Email , model.Email),
                 new Claim(ClaimTypes.Role , "Admin"),
                 new Claim("Name",model.Name),
                 new Claim("Create","true"),
                 new Claim("Update","true"),
                 new Claim("Delete","true"),
                 new Claim("Read","true"),
                 new Claim("ManagerUser","true"),
            ];
        }
        else if (model.Policy.Equals(Policy.ManagerPolicy, StringComparison.OrdinalIgnoreCase))
        {
            userCliams =
            [
                 new Claim(ClaimTypes.Email , model.Email),
                 new Claim(ClaimTypes.Role , "Manager"),
                 new Claim("Name",model.Name),
                 new Claim("Create","true"),
                 new Claim("Update","true"),
                 new Claim("Delete","true"),
                 new Claim("Read","true"),
                 new Claim("ManagerUser","false"),
                 new Claim("Delete","false"),
            ];
        }
        else if (model.Policy.Equals(Policy.UserPolicy, StringComparison.OrdinalIgnoreCase))
        {
            userCliams =
            [
                 new Claim(ClaimTypes.Email , model.Email),
                 new Claim(ClaimTypes.Role , "User"),
                 new Claim("Name",model.Name),
                 new Claim("Create","false"),
                 new Claim("Update","false"),
                 new Claim("Delete","false"),
                 new Claim("Read","false"),
                 new Claim("ManagerUser","false"),
                 new Claim("Delete","false"),
            ];
        }

        var user = await FindUserByEmail(model.Email);

        var identityResult = await userManager.AddClaimsAsync(user, userCliams);

        var result = CheckResult(identityResult);

        if(result.Flag)
        {
            return new ServiceResponse(true, "User created");
        }

        return result;
    }

    private static ServiceResponse CheckResult(IdentityResult identityResult)
    {
        if (identityResult.Succeeded)
        {
            return new ServiceResponse(true, null);
        }

        var errors = identityResult.Errors.Select(_ => _.Description);

        return new ServiceResponse(false, string.Join(Environment.NewLine, errors));
    }

    private async Task<ApplicationUser> FindUserByEmail(string email) => await userManager.FindByEmailAsync(email);

    private async Task<ApplicationUser> FindUserById(string userId) => await userManager.FindByIdAsync(userId);

}
