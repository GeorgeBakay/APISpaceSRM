using APISpaceSRM.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APISpaceSRM.Data.Repository
{
    public class TenantRepository : ITenant
    {
        public readonly DataContext _context;
        public TenantRepository(DataContext context) {  _context = context; }
        public Guid? TenantId { get; set; }
        public async Task<bool> SetTenant(Guid tenant)
        {
            var tenantInfo = await _context.tenants.Where(x => x.Id == tenant).FirstOrDefaultAsync();
            if (tenantInfo != null)
            {
                TenantId = tenantInfo.Id;
                return true;
            }
            else
            {
                throw new Exception("Tenant Invalid");
            }

        }
    }
}
