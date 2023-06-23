using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace AuthReadyAPI.Middleware
{
    public class CustomHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken CT = default)
        {
            bool isAPIHealthy = true;

            /* Custom Logic and Checks here */

            if (isAPIHealthy) return Task.FromResult(HealthCheckResult.Healthy("All systems go"));

            return Task.FromResult(new HealthCheckResult(context.Registration.FailureStatus, "API unhealthy"));
        }
    }
}
