using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GroupMeeting.Areas.Identity.Data;
using GroupMeeting.Areas.GroupCategories.Models;

namespace GroupMeeting.Data
{
    public class GroupMeetingContext : IdentityDbContext<ApplicationUser>
    {
        public GroupMeetingContext(DbContextOptions<GroupMeetingContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>()
          .HasKey(e => e.Id);

            builder.Entity<Category>()
               .HasOne(c => c.User)
                .WithMany(g => g.Categories)
                .HasForeignKey(f => f.UserId);

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
