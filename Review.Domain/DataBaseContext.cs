using Microsoft.EntityFrameworkCore;
using Review.Domain.Helper;
using Review.Domain.Models;

namespace Review.Domain;

public class DataBaseContext : DbContext
{
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Models.Review> Reviews { get; set; }
    public DbSet<Login> Logins { get; set; }

    public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.Review>()
            .HasOne(p => p.Rating)
            .WithMany(t => t.Feedbacks)
            .HasForeignKey(p => p.RatingId)
            .OnDelete(DeleteBehavior.Cascade);

        var feedbacks = Initialization.GetFeedbacks();
        var ratings = Initialization.GetRatings();

        modelBuilder.Entity<Models.Review>().HasData(feedbacks);
        modelBuilder.Entity<Rating>().HasData(ratings);

        var logins = Initialization.GetLogins();
        modelBuilder.Entity<Login>().HasData(logins);
    }
}