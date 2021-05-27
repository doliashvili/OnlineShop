using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.UI.Extensions
{
    public static class HttpClientExt
    {
        public static async Task SendAsync(this HttpClient httpClient, HttpMethod httpMethod, string relativePath, string body, CancellationToken cancellationToken)
        {
            // _logger.LogDebug("Sending... HttpMethod: '{HttpMethod}', RelativePath: '{RelativePath}', Body: '{Body}'", httpMethod, relativePath, body);

            var request = new HttpRequestMessage(httpMethod, relativePath)
            {
                Version = HttpVersion.Version11,
                VersionPolicy = HttpVersionPolicy.RequestVersionOrLower
            };

            if (body is not null)
            {
                request.Content = new StringContent(body, Encoding.UTF8, "application/json");
            }

            var response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);

            await response.EnsureSuccessAsync().ConfigureAwait(false);
        }

        public static async Task<TResult> SendAsync<TResult>(this HttpClient httpClient, HttpMethod httpMethod, string relativePath, string body, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(httpMethod, relativePath)
            {
                //  Version = HttpVersion.Version11,
                // VersionPolicy = HttpVersionPolicy.RequestVersionOrLower
            };

            if (body is not null)
            {
                request.Content = new StringContent(body, Encoding.UTF8, "application/json");
            }

            var response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);

            await response.EnsureSuccessAsync().ConfigureAwait(false);

            var json = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

            if (json.Length == 0)
                return default;

            if (typeof(TResult) == typeof(string))
                return (TResult)(object)json;

            if (typeof(TResult) == typeof(Guid))
                return (TResult)(object)Guid.Parse(json);

            var result = json.FromJson<TResult>(true);

            return result;
        }

        public static async Task EnsureSuccessAsync(this HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            ProblemDetails problemDetails;
            try
            {
                problemDetails = content.FromJson<ProblemDetails>(true);
            }
            catch
            {
                throw new Exception($"Http error: Status: {response.StatusCode}, Content: {content}");
            }

            throw new Exception($"Exception Title: '{problemDetails.Title}', Exception type: '{problemDetails.Type}', status: '{problemDetails.Status}' Details: '{problemDetails.Detail}'");
        }
    }
}