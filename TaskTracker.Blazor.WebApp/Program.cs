using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using Blazored.LocalStorage;
using Refit;
using TaskTracker.Blazor.WebApp;
using TaskTracker.Blazor.WebApp.Authentication;
using TaskTracker.Blazor.WebApp.Handlers;
using TaskTracker.Blazor.Services;
using TaskTracker.Blazor.Services.Abstraction;
using TaskTracker.Blazor.Services.Abstraction.ExternalApi;

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
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBoardService, BoardService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITaskListService, TaskListService>();


builder.Services.AddTransient<AuthorizationMessageHandler>();


builder.Services.AddRefitClient<IAuthApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiBaseUrl));
builder.Services.AddRefitClient<IBoardApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiBaseUrl))
    .AddHttpMessageHandler<AuthorizationMessageHandler>();
builder.Services.AddRefitClient<ITaskApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiBaseUrl))
    .AddHttpMessageHandler<AuthorizationMessageHandler>();
builder.Services.AddRefitClient<ICommentApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiBaseUrl))
    .AddHttpMessageHandler<AuthorizationMessageHandler>();
builder.Services.AddRefitClient<IUserApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiBaseUrl))
    .AddHttpMessageHandler<AuthorizationMessageHandler>();
builder.Services.AddRefitClient<ITaskListApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(apiBaseUrl))
    .AddHttpMessageHandler<AuthorizationMessageHandler>();


await builder.Build().RunAsync();