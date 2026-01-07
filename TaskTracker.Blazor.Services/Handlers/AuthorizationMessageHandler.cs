using System.Net.Http.Headers;
using TaskTracker.Blazor.Services.Abstraction;
using TaskTracker.Blazor.Domain.Constants; 

namespace TaskTracker.Blazor.Services.Handlers;

public class AuthorizationMessageHandler : DelegatingHandler
{
    private readonly IAppStorageService _storageService;

    public AuthorizationMessageHandler(IAppStorageService storageService)
    {
        _storageService = storageService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
       
        var token = await _storageService.GetItemAsync<string>(AppConstants.AuthTokenKey);

        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}