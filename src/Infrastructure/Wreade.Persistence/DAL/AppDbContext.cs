using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WEB.Models;

namespace Wreade.Persistence.DAL
{
    public class AppDbContext:IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.AppQueryFilters();

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Image> Images { get; set; }
        //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    //var entities = ChangeTracker.Entries<Category>();
        //    foreach (var item in entities)
        //    {
        //        switch (item.State)
        //        {
        //            case EntityState.Deleted:
        //                break;
        //            case EntityState.Modified:
        //                item.Entity.ModifiedAt = DateTime.Now;
        //                break;
        //            case EntityState.Added:
        //                item.Entity.CreatedAt = DateTime.Now;

        //                break;

        //        }
        //    }
        //    return base.SaveChangesAsync(cancellationToken);
        //}
    }
}
