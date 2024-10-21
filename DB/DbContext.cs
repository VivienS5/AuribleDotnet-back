using Aurible.Models;using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    // Assurez-vous que ces noms correspondent à votre modèle
    public DbSet<Book> Books { get; set; }
    public DbSet<Chapter> Chapters { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .HasKey(b => b.idBook);

        modelBuilder.Entity<Chapter>()
            .HasKey(c => c.idChapter);
        modelBuilder.Entity<User>()
            .HasKey(u => u.IdUser);
        modelBuilder.Entity<User>()
            .Property(u => u.IdUser)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Chapter>()
            .HasOne(c => c.Book)
            .WithMany(b => b.Chapters)
            .HasForeignKey(c => c.idBook_FK);
    }
}
