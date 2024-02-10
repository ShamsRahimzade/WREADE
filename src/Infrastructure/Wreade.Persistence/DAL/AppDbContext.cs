using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Wreade.Domain.Entities;

namespace Wreade.Persistence.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.AppQueryFilters();

          //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~BookCategory
            modelBuilder.Entity<BookCategory>()
            .HasOne(bc => bc.Category)
            .WithMany(c => c.BookCategories)
            .HasForeignKey(bc => bc.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Follow
            modelBuilder.Entity<Follow>()
                  .HasOne(u => u.Followee)
                  .WithMany(u => u.Followers)
                  .HasForeignKey(u => u.FollowerId)
                  .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Follow>()
             .HasOne(u => u.Follower)
             .WithMany(u => u.Followees)
             .HasForeignKey(u => u.FolloweeId)
         .OnDelete(DeleteBehavior.Restrict);
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Comment
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasOne(a => a.Author)
                .WithMany(c => c.Comments)
                .HasForeignKey(aId => aId.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);


                entity.HasOne(p => p.CommentedBook)
                .WithMany(c => c.Comments)
                .HasForeignKey(pId => pId.CommentedBookId);

            });
            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~AppUser
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasMany<Book>(p => p.Books)
                .WithOne(a => a.User)
                .HasForeignKey(pId => pId.AppUserId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany<Comment>(c => c.Comments)
                .WithOne(a => a.Author)
                .HasForeignKey(cId => cId.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey, e.UserId });
            });
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
            });
            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });
            modelBuilder.Entity<ChapterImage>(entity =>
            {
                entity.HasKey(e => e.ChapterId); 
            });
        }

        public DbSet<Tag> Tags { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<BookCategory> BookCategory { get; set; }
        public DbSet<BookTag> BookTag { get; set; }
        public DbSet<Setting> Setting { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<ChapterImage> ChapterImages { get; set; }
        public DbSet<ChapterViewCount> ChapterViewCounts { get; set; }

    }
}
