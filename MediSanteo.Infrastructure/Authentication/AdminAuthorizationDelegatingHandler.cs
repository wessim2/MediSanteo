using MediSanteo.Infrastructure.Authentication.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;


namespace MediSanteo.Infrastructure.Authentication
{
    public sealed class AdminAuthorizationDelegatingHandler : DelegatingHandler
    {
        private readonly KeycloakOptions _keycloakOptions;
        private readonly ILogger<AdminAuthorizationDelegatingHandler> logger;

        public AdminAuthorizationDelegatingHandler(IOptions<KeycloakOptions> keycloakOptions, ILogger<AdminAuthorizationDelegatingHandler> logger)
        {
            _keycloakOptions = keycloakOptions.Value;
            this.logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var authorizationToken = await GetAuthorizationToken(cancellationToken);

            this.logger.LogInformation(authorizationToken.AccessToken);
            this.logger.LogInformation("******************************");

            request.Headers.Authorization = new AuthenticationHeaderValue(
                JwtBearerDefaults.AuthenticationScheme,
                authorizationToken.AccessToken);

            var httpResponseMessage = await base.SendAsync(request, cancellationToken);

            httpResponseMessage.EnsureSuccessStatusCode();

            return httpResponseMessage;
        }

        private async Task<AuthorizationToken> GetAuthorizationToken(CancellationToken cancellationToken)
        {
            var authorizationRequestParameters = new KeyValuePair<string, string>[]
            {
            new("client_id", _keycloakOptions.AdminClientId),
            new("client_secret", _keycloakOptions.AdminClientSecret),
            new("scope", "openid email"),
            new("grant_type", "client_credentials")
            };
            this.logger.LogInformation(_keycloakOptions.AdminClientId);
            this.logger.LogInformation(_keycloakOptions.AdminClientSecret);
            var authorizationRequestContent = new FormUrlEncodedContent(authorizationRequestParameters);

            var authorizationRequest = new HttpRequestMessage(
                HttpMethod.Post,
                new Uri(_keycloakOptions.TokenUrl))
            {
                Content = authorizationRequestContent
            };

            var authorizationResponse = await base.SendAsync(authorizationRequest, cancellationToken);

            authorizationResponse.EnsureSuccessStatusCode();

            return await authorizationResponse.Content.ReadFromJsonAsync<AuthorizationToken>() ??
                   throw new ApplicationException();
        }
    }
}
