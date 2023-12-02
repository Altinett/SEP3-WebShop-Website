using BlazorWasm.Auth;
using BlazorWasm.Services;
using BlazorWasm.Services.Http;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using WebShop;
using Shared.Auth;
using WebShop.Pages;
using WebShop.Services.HTTP;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAuthService, JwtAuthService>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();

builder.Services.AddSingleton<IValidationService, ValidationService>();
builder.Services.AddSingleton<ICategoryService, CategoryService>();
builder.Services.AddSingleton<IHandicapService, HandicapService>();
builder.Services.AddSingleton<IProductService, ProductService>();
builder.Services.AddSingleton<ISearchService, SearchService>();
builder.Services.AddSingleton<IBasketService, BasketService>();
builder.Services.AddSingleton<IFormService, FormService>();


AuthorizationPolicies.AddPolicies(builder.Services);


await builder.Build().RunAsync();