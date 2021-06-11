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
        public DbSet<GroupUser> GroupUser { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<GroupCategory> GroupCategories { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<MeetingUser> MeetingUser{ get; set; }
        public GroupMeetingContext(DbContextOptions<GroupMeetingContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>()
                .HasKey(e => e.Id);

            builder.Entity<User>()
                .HasKey(e => e.Id);

            builder.Entity<GroupCategory>().HasKey(gc => new { gc.CategoryId, gc.GroupId });

            builder.Entity<Category>()
               .HasOne(c => c.User)
                .WithMany(g => g.Categories)
                .HasForeignKey(f => f.UserId);

            builder.Entity<MeetingUser>(e =>
            {
                e.HasKey(mu => new { mu.MeetingID, mu.UserId });
                e.Property(ent => ent.MeetingID).ValueGeneratedNever();
                e.Property(ent => ent.UserId).ValueGeneratedNever();
                e.HasOne(mu => mu.Meeting).WithMany().HasForeignKey(mu => mu.MeetingID);
                e.HasOne(mu => mu.User).WithMany().HasForeignKey(mu => mu.UserId);
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
