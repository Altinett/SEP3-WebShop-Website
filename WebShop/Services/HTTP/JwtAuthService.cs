using System.Reflection.Metadata;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json;
using Shared.DTOs;
using Shared.Models;
using WebShop.Shared.DTOs;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BlazorWasm.Services.Http;

public class JwtAuthService : IAuthService
{
    private readonly HttpClient client = new ();

    // this private variable for simple caching
    private static string? Jwt { get;  set; } = "";

    public Action<ClaimsPrincipal> OnAuthStateChanged { get; set; } = null!;
    
    private UserDto UserDto { get; set; }
    
    public async Task LoginAsync(string username, string password)
    {
        /*

        string userAsJson = JsonSerializer.Serialize(userLoginDto);
        Console.WriteLine(userAsJson);
        StringContent content = new(userAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync("http://localhost:5096/auth/login", content);
        string responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseContent);
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(responseContent);
        }

        string token = responseContent;
        Jwt = token;
       */


        UserDto =  await GetFromLogic(username, password);
        
        if (UserDto == null)
        {
            throw new Exception("Forkert brugernavn og/eller adgangskode");
        }
        Jwt = "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJKV1RTZXJ2aWNlQWNjZXNzVG9rZW4iLCJqdGkiOiJlNTU3NDA5My1mOTljLTQ1ODAtYmM1Yy1mZWQzMGY2NzM3YjciLCJpYXQiOiIyMyBOb3YgMjMgMDkuMDAuNDgiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiTUxHIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiTWVtYmVyIiwiVXNlciBJRCI6IjYiLCJEaXNwbGF5TmFtZSI6Ik1hcmMiLCJFbWFpbCI6ImhhaGFAeGQuY29tIiwiQWdlIjoiMjciLCJEb21haW4iOiJ4ZC5jb20iLCJTZWN1cml0eUxldmVsIjoiMSIsImV4cCI6MTcwMDczMzY0OCwiaXNzIjoiSldUQXV0aGVudGljYXRpb25TZXJ2ZXIiLCJhdWQiOiJKV1RTZXJ2aWNlQmxhem9yV2FzbUNsaWVudCJ9.GXASqoSQfmbjVaNvvgsGmXhO10jk_vjj3pb_ZZG9Qzmfo1jKACh9G6ZcfGbVsfUAJpC5dPn9FLH3MIQGzjn8Ug";
        
        ClaimsPrincipal principal = CreateClaimsPrincipal();

        OnAuthStateChanged.Invoke(principal);
    }

    public UserDto GetUser()
    {
        return UserDto;
    }

    private static ClaimsPrincipal CreateClaimsPrincipal()
    {
        if (string.IsNullOrEmpty(Jwt))
        {
            return new ClaimsPrincipal();
        }

        IEnumerable<Claim> claims = ParseClaimsFromJwt(Jwt);
        
        ClaimsIdentity identity = new(claims, "jwt");

        ClaimsPrincipal principal = new(identity);
        return principal;
    }

    public Task LogoutAsync()
    {
        Jwt = null;
        ClaimsPrincipal principal = new();
        OnAuthStateChanged.Invoke(principal);
        return Task.CompletedTask;
    }
    
    public async Task<UserDto> GetFromLogic(string username, string password)
    {
        UserDto user = new()
        {
            username = username,
            password = password
        };
        string postAsJson = JsonConvert.SerializeObject(user);
        StringContent content = new (postAsJson, Encoding.UTF8, "application/json");
        HttpResponseMessage response = await client.PostAsync($"http://localhost:8080/users",content);
        string responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine(responseContent);
        UserDto userDto = JsonConvert.DeserializeObject<UserDto>(responseContent);
        return userDto;
    }

    public Task<ClaimsPrincipal> GetAuthAsync()
    {
        ClaimsPrincipal principal = CreateClaimsPrincipal();
        return Task.FromResult(principal);
    }


    // Below methods stolen from https://github.com/SteveSandersonMS/presentation-2019-06-NDCOslo/blob/master/demos/MissionControl/MissionControl.Client/Util/ServiceExtensions.cs
    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        string payload = jwt.Split('.')[1];
        byte[] jsonBytes = ParseBase64WithoutPadding(payload);
        Dictionary<string, object>? keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
        return keyValuePairs!.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }

        return Convert.FromBase64String(base64);
    }
}