namespace GameOfLifeServer.Helpers;

using GameOfLifeServer.Entities;
using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
        
    }
    /*protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // usamos sqlite.
        options.UseSqlite(Configuration.GetConnectionString("GameOfLifeDb"));
    }*/

    public DbSet<MessageEntity> Messages { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Session> MatchGroup { get; set; }
}