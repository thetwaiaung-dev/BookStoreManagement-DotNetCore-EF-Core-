using BookManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManagement
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) 
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                        .HasMany(x => x.Pages)
                        .WithOne(x => x.Book)
                        .HasForeignKey(x => x.Book_Id)
                        .IsRequired();

            modelBuilder.Entity<Book>()
                        .HasOne(x => x.Author)
                        .WithMany(x => x.Books)
                        .HasForeignKey(x=>x.Author_Id)
                        .IsRequired();

            modelBuilder.Entity<Book>()
                        .HasOne(x=>x.Category)
                        .WithMany(x => x.Books)
                        .HasForeignKey(x=>x.Category_Id)
                        .IsRequired();
        }

        public DbSet<Book> Book { get; set; }
        public DbSet<BookCategory> Category { get; set; }
    }
}
