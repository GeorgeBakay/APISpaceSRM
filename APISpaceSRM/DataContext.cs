using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions;
using APISpaceSRM.Data.Models;

namespace APISpaceSRM
{
    public class DataContext : DbContext
    {
        //Загрузка БД
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        
        //Налаштування зв'язок
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Client>().HasMany(u => u.Records).WithOne(e => e.Client).HasForeignKey(o => o.ClientId).OnDelete(DeleteBehavior.Cascade);

            modelbuilder.Entity<Record>().HasOne(u => u.Client).WithMany(e => e.Records).OnDelete(DeleteBehavior.ClientSetNull);
            modelbuilder.Entity<Record>().HasMany(u => u.Photos).WithOne(e => e.Record).HasForeignKey(o => o.RecordId).OnDelete(DeleteBehavior.Cascade);
           
            modelbuilder.Entity<Employer>().HasMany(u => u.Works).WithMany(e => e.Employers);
            modelbuilder.Entity<Employer>().HasMany(u => u.Salaries).WithOne(e => e.Employer).HasForeignKey(o => o.EmployerId).OnDelete(DeleteBehavior.Cascade);

            modelbuilder.Entity<Work>().HasMany(u => u.Employers).WithMany(e => e.Works);
            modelbuilder.Entity<Work>().HasOne(u => u.Service).WithMany(e => e.Works);

            modelbuilder.Entity<Service>().HasMany(u => u.Works).WithOne(e => e.Service).HasForeignKey(o => o.ServiceId).OnDelete(DeleteBehavior.Cascade);
            modelbuilder.Entity<Service>().HasMany(u => u.SetServices).WithMany(e => e.Services);

            modelbuilder.Entity<Photo>().HasOne(u => u.Record).WithMany(e => e.Photos);
           
        }

        //Загрузка таблиць
        public DbSet<Tenant> tenants { get; set; }
        public DbSet<Client> clients { get; set; }
        public DbSet<Employer> employers { get; set; }
        public DbSet<Record> records { get; set; }
        public DbSet<Work> works { get; set; }
        public DbSet<Service> services { get; set; }
        //public DbSet<SetService> setServices { get; set; }
        public DbSet<Photo> photos { get; set; }
        public DbSet<Cost> costs { get; set; }
        public DbSet<Salary> salaries { get; set; }
        //DbSet<PolishingService> PolishingServices { get; set; }
        //DbSet<PolishHeadLightsService> PolishHeadLightsServices { get; set; }
        //DbSet<PolishSalonService> PolishSalonServices { get; set; }
        //DbSet<BodyWashService> BodyWashServices { get; set; }
        //DbSet<MotorWashService> MotorWashServices { get; set; }
        //DbSet<DistCeramService> DistCeramServices { get; set; }
        //DbSet<CarCeramService> CarCeramServices { get; set; }
        //DbSet<WaxService> WaxServices { get; set; }
        //DbSet<AntiRainService> AntiRainServices { get; set; }
    }
}
