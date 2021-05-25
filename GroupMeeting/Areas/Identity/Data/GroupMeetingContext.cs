using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using GroupMeeting.Areas.Identity.Data;
using GroupMeeting.Models;

namespace GroupMeeting.Data
{
    public class GroupMeetingContext : IdentityDbContext<User>
    {
        public GroupMeetingContext(DbContextOptions<GroupMeetingContext> options)
            : base(options)
        {
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<GroupCity>()
                .HasKey(gc => new { gc.GroupID, gc.CityID });
            builder.Entity<GroupCity>()
                .HasOne(x => x.Group)
                .WithMany(g => g.GroupCities)
                .HasForeignKey(y => y.GroupID);
            builder.Entity<GroupCity>()
                .HasOne(x => x.City)
                .WithMany(y => y.GroupCities)
                .HasForeignKey(y => y.CityID);

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
                e.HasOne(go => go.Group).WithMany().HasForeignKey(go => go.GroupID);
                e.HasOne(go => go.Owner).WithOne().HasForeignKey<GroupOwner>(go => go.OwnerID);
            }
            );
            //wat
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
