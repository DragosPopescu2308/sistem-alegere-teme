using MyApp.Models;
using MyApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace MyApp.Repositories
{
    public interface IAccountRepository
    {
        Task<SignInResult> LoginAsync(LoginViewModel model);
        Task<IdentityResult> RegisterAsync(RegisterViewModel model);
        Task LogoutAsync();
        Task<Users> FindByEmailAsync(string email);
        Task<IdentityResult> ChangePasswordAsync(ChangePasswordViewModel model);
        Task AddUserToRoleAsync(Users user, string role);
    }
}
