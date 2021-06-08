using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GroupMeeting.Areas.Identity.Data;
using GroupMeeting.Areas.GroupCategories.Models;
using GroupMeeting.Models;
namespace GroupMeeting.Data
{
    public class GroupMeetingContext : IdentityDbContext<User>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<GroupOwner> GroupOwner { get; set; }
        public DbSet<GroupCity> GroupCity { get; set; }
        public DbSet<GroupUser> GroupUser { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<GroupCategory> GroupCategories { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public GroupMeetingContext(DbContextOptions<GroupMeetingContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>()
                .HasKey(e => e.Id);

            builder.Entity<Group>()
                .HasOne(g => g.City).WithOne().OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                .HasKey(e => e.Id);

            builder.Entity<GroupCategory>().HasKey(gc => new { gc.CategoryId, gc.GroupId });

            builder.Entity<Category>()
               .HasOne(c => c.User)
                .WithMany(g => g.Categories)
                .HasForeignKey(f => f.UserId);

            builder.Entity<GroupCity>(e =>
            {
                e.HasKey(go => new { go.GroupID, go.CityID });
                e.HasOne(go => go.Group).WithOne().HasForeignKey<GroupCity>(go => go.GroupID);
                e.HasOne(go => go.City).WithMany().HasForeignKey(go => go.CityID);
            });

            builder.Entity<GroupUser>()
                .HasKey(gc => new { gc.GroupID, gc.UserID });
            builder.Entity<GroupUser>()
                .HasOne(x => x.Group)
                .WithMany(g => g.GroupUsers)
                .HasForeignKey(y => y.GroupID);
            builder.Entity<GroupUser>()
                .HasOne(x => x.User)
                .WithMany(y => y.GroupUsers)
                .HasForeignKey(y => y.UserID);

            builder.Entity<GroupOwner>(e =>
            {
                e.HasKey(go => new { go.GroupID, go.OwnerID });
                e.HasOne(go => go.Group).WithOne().HasForeignKey<GroupOwner>(go => go.GroupID);
                e.HasOne(go => go.Owner).WithMany().HasForeignKey(go => go.OwnerID);
            }
            );
            builder.Entity<Meeting>()
                .HasKey(x => x.ID);
            builder.Entity<Meeting>()
                .HasOne(x => x.Group)
                .WithMany(y => y.Meetings)
                .HasForeignKey(z => z.Group_id);

            //wat
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
