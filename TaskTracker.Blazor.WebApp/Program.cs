using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using Blazored.LocalStorage;
using TaskTracker.Blazor.WebApp;
using TaskTracker.Blazor.WebApp.Authentication;
using TaskTracker.Blazor.Services; 
using TaskTracker.Blazor.Services.Abstraction;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"]
                 ?? "https://localhost:7163/api";


builder.Services.AddBlazoredLocalStorage();
builder.Services.AddMudServices();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<IAppStorageService, LocalStorageService>();

builder.Services.AddApplicationServices(apiBaseUrl);

await builder.Build().RunAsync();