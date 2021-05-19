using System;
using GroupMeeting.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GroupMeeting.Areas.Identity.Data;

[assembly: HostingStartup(typeof(GroupMeeting.Areas.Identity.IdentityHostingStartup))]
namespace GroupMeeting.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<GroupMeetingContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("GroupMeetingContextConnection")));

                services.AddDefaultIdentity<User>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.User.RequireUniqueEmail = true;
                })
                    .AddEntityFrameworkStores<GroupMeetingContext>();
            });
        }
    }
}