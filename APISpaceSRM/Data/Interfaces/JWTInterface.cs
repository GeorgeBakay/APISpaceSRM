using Microsoft.Net.Http.Headers;

namespace APISpaceSRM.Data.Interfaces

{
    public interface JWTInterface
    {
        public Task<string> GetTenantTocken(Guid Id);
        public Task<Guid> GetTenantIdFromToken(string token);
    }
}
