using System.Security.Claims;
using Shared.Models;
using WebShop.Shared.DTOs;

namespace BlazorWasm.Services;

public interface IAuthService
{
    public UserDto GetUser();
    public Task LoginAsync(string username, string password);
    public Task LogoutAsync();
    public Task<ClaimsPrincipal> GetAuthAsync();

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; }
}