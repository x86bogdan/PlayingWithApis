﻿using System.Globalization;
using System.Net;
using System.Threading.RateLimiting;

namespace ClassLibrary1
{
    internal class ClientSideRateLimitedHandler : DelegatingHandler, IAsyncDisposable
    {
        private readonly RateLimiter limiter;

        public ClientSideRateLimitedHandler(RateLimiter rateLimiter) : base(new HttpClientHandler())
        {
            limiter = rateLimiter;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
       HttpRequestMessage request, CancellationToken cancellationToken)
        {
            using RateLimitLease lease = await limiter.AcquireAsync(permitCount: 1, cancellationToken);

            if (lease.IsAcquired)
            {
                return await base.SendAsync(request, cancellationToken);
            }

            var response = new HttpResponseMessage(HttpStatusCode.TooManyRequests);
            if (lease.TryGetMetadata(
                    MetadataName.RetryAfter, out TimeSpan retryAfter))
            {
                response.Headers.Add("Retry-After",
                    ((int)retryAfter.TotalSeconds).ToString(NumberFormatInfo.InvariantInfo));
            }

            return response;
        }

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            await limiter.DisposeAsync().ConfigureAwait(false);

            Dispose(disposing: false);
            GC.SuppressFinalize(this);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                limiter.Dispose();
            }
        }
    }
}
