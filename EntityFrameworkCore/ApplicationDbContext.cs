using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Core.Autentication;
using Core.Entities.Catalog;
using Core.Entities.Notifications;
using Core.Entities.Tweet;
using System.Runtime.CompilerServices;

namespace EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<User, Role, int>
{
    public virtual DbSet<UserType> UserTypes { get; set; }

    public virtual DbSet<Status> Status { get; set; }
    public virtual DbSet<Tweet> Tweets { get; set; }
    public virtual DbSet<Comment> Comments { get; set; }
    public virtual DbSet<Notification> Notifications { get; set; }
    public virtual DbSet<NotificationType> NotificationTypes { get; set; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }
}