namespace APISpaceSRM.Data.Interfaces
{
    public interface ITenantGlobal
    {
        public Task<bool> GetTenantAsync(Guid tenantId);
        public Task<bool> CreateTenant(string name,string password);
    }
}
