using Microsoft.EntityFrameworkCore;

namespace TSR2025Backend.Data;

public class ApplicationContext : DbContext
{
    public static ApplicationContext Instance
    {
        get
        {
            if (_instance != null) return _instance;
            _instance = new ApplicationContext();
            _instance.Database.EnsureCreated();
            _instance.Users.Load();
            _instance.AuthenticationCodes.Load();
            return _instance;
        }
    }

    private static ApplicationContext _instance = null;
    
    public DbSet<User> Users { get; set; } = null;
    public DbSet<AuthenticationCode> AuthenticationCodes { get; set; } = null;
    public DbSet<Lobby> Lobbies { get; set; } = null;
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlite("Data Source=tsrdb.db");
    }
}