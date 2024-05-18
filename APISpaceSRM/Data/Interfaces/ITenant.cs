namespace APISpaceSRM.Data.Interfaces
{
    public interface ITenant
    {
        Guid? TenantId { get; set; }
        public Task<bool> SetTenant(Guid tenant);
    }
}
