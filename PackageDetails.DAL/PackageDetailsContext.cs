using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PackageDetails.CORE.Base;
using PackageDetails.CORE.Models.Identity;
using PackageDetails.CORE.Models.MasterData;
using PackageDetails.CORE.Models.Packages;
using PackageDetails.CORE.Models.Products;
using PackageDetails.CORE.Models.Switchboards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PackageDetails.DAL
{
    public partial class PackageDetailsContext : DbContext
    {

        public PackageDetailsContext(DbContextOptions<PackageDetailsContext> options)
             : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Package> Package { get; set; }
        public DbSet<PackageProduct> PackageProduct { get; set; }
        public DbSet<DeliveryStage> DeliveryStage { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            modelBuilder.Entity<User>();
            modelBuilder.Entity<Product>();
            modelBuilder.Entity<Package>();
            modelBuilder.Entity<PackageProduct>();
            modelBuilder.Entity<DeliveryStage>();
        }

        private void AddTimestamps()
        {
            IEnumerable<EntityEntry> entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added || x.State == EntityState.Modified));
            DateTime dateNow = DateTime.Now;

            foreach (EntityEntry entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).CreateDate = dateNow;
                    ((BaseEntity)entity.Entity).CreatedUser = CurrentUser;
                }

                ((BaseEntity)entity.Entity).ModifyDate = dateNow;
                ((BaseEntity)entity.Entity).ModifiedUser = CurrentUser;
            }
        }

        public static string CurrentUser
        {
            get
            {
                return !string.IsNullOrEmpty(Thread.CurrentPrincipal?.Identity?.Name)
                   ? Thread.CurrentPrincipal?.Identity?.Name
                   : "Anonymous";
            }
        }

        public static string CurrentSamAccountName
        {
            get
            {
                string username = CurrentUser;
                if (username.Contains(@"\"))
                {
                    int lastIndex = username.LastIndexOf(@"\");
                    username = username.Substring(lastIndex + 1);
                }

                return username;
            }
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }
    }
}
