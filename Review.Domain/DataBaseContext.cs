using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Review.Domain.Helper;
using Review.Domain.Models;

namespace Review.Domain;

public class DataBaseContext : DbContext
{
    protected readonly IConfiguration Configuration;
    
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Feedback> Feedbacks { get; set; }
    public DbSet<Login> Logins { get; set; }

    public DataBaseContext(DbContextOptions<DataBaseContext> options, IConfiguration configuration) : base(options)
    {
        Configuration = configuration;
        Database.EnsureCreated();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to mysql with connection string from app settings
        var connectionString = Configuration.GetConnectionString("WebApiDatabase");
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Feedback>()
            .HasOne(p => p.Rating)
            .WithMany(t => t.Feedbacks)
            .HasForeignKey(p => p.RatingId)
            .OnDelete(DeleteBehavior.Cascade);

        var feedbacks = Initialization.SetFeedbacks();
        var rating = Initialization.SetRatings();

        modelBuilder.Entity<Feedback>().HasData(feedbacks);
        modelBuilder.Entity<Rating>().HasData(rating);

        Login[] login = Initialization.SetLogins();
        modelBuilder.Entity<Login>().HasData(login);
    }
}