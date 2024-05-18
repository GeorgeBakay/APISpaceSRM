using APISpaceSRM.Data.Interfaces;
using APISpaceSRM.Data.Repository;

namespace APISpaceSRM.Middleware
{
    public class TenantResolver
    {
        private readonly RequestDelegate _next;
        public TenantResolver(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context,ITenant currentTenant,JWTInterface jwt)
        {
            context.Request.Headers.TryGetValue("Bearer", out var tokenFromHeader);
            if (!string.IsNullOrEmpty(tokenFromHeader))
            {

                //TODO: get tenantId from JWT token
                var tenant = await jwt.GetTenantIdFromToken(tokenFromHeader);
                await currentTenant.SetTenant(tenant);

            }
            await _next(context);
        }
    }
}
