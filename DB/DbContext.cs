using Aurible.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Book> Book { get; set; }
    public DbSet<Chapter> Chapter { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Définir la clé primaire pour Book
        modelBuilder.Entity<Book>()
            .HasKey(b => b.idBook);

        // Définir la clé primaire pour Chapter
        modelBuilder.Entity<Chapter>()
            .HasKey(c => c.idChapter);

        // Configurer la relation entre Chapter et Book sans explicitement définir une clé étrangère
        modelBuilder.Entity<Chapter>()
            .HasOne(c => c.Book) // Un chapitre est lié à un livre
            .WithMany(b => b.Chapters) // Un livre peut avoir plusieurs chapitres
            .OnDelete(DeleteBehavior.Cascade); // Si un livre est supprimé, supprimer aussi les chapitres associés
    }
}
