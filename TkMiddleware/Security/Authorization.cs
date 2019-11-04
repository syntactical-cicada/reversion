using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Filters;

namespace TkMiddleware.Security
{
    public class ApiKeyAuthenticationAttribute : IAuthenticationFilter
    {
        private const string ApiKey = "8a2894ad-7cb6-4e65-b406-67fc5c7b6de6";
        public bool AllowMultiple { get; set; }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            HttpRequestMessage request = context.Request;

            // Get Auth header
            AuthenticationHeaderValue authorization = request.Headers.Authorization;

            // Validate the static token
            if (authorization?.Scheme == ApiKey)
            {
                var principal = new GenericPrincipal(new GenericIdentity(authorization.Scheme), null);
                Thread.CurrentPrincipal = principal;
                if (HttpContext.Current != null)
                {
                    HttpContext.Current.User = principal;
                }

                context.Principal = principal;
            }
            else
            {
                context.ErrorResult = new AuthenticationFailureResult(request);
            }
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            var challenge = new AuthenticationHeaderValue("Basic");
            context.Result = new AddChallengeOnUnauthorizedResult(challenge, context.Result);

            return Task.FromResult(0);
        }
    }
    
    class AuthenticationFailureResult : IHttpActionResult
    {
        private HttpRequestMessage _request;

        public AuthenticationFailureResult(HttpRequestMessage request)
        {
            _request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            response.RequestMessage = _request;

            return Task.FromResult(response);
        }
    }

    class AddChallengeOnUnauthorizedResult : IHttpActionResult
    {
        public AddChallengeOnUnauthorizedResult(AuthenticationHeaderValue challenge, IHttpActionResult innerResult)
        {
            Challenge = challenge;
            InnerResult = innerResult;
        }

        public AuthenticationHeaderValue Challenge { get; private set; }

        public IHttpActionResult InnerResult { get; private set; }

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await InnerResult.ExecuteAsync(cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                // Only add one challenge per authentication scheme.
                if (!response.Headers.WwwAuthenticate.Any((h) => h.Scheme == Challenge.Scheme))
                {
                    response.Headers.WwwAuthenticate.Add(Challenge);
                }
            }

            return response;
        }
    }
}