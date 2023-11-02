using Microsoft.EntityFrameworkCore;
using UserWalletApplication.Models;

namespace UserWalletApplication.Database;

public class MyDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public DbSet<Wallet> Wallets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("WalletDb");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasMany(u => u.UserWallets)
            .WithOne(w => w.User)
            .HasForeignKey(wallet => wallet.Owner);


        // modelBuilder.Entity<Wallet>()
        //     .HasOne<User>(wallet => wallet.User)
        //     .WithMany(user => user.UserWallets)
        //     .HasForeignKey(wallet => wallet.Owner);
    }
}
