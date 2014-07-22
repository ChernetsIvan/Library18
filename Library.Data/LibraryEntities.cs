using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Microsoft.AspNet.Identity.EntityFramework;
using Library.Data.Configuration;
using Library.Model.Models;


namespace Library.Data
{
    public class LibraryEntities : IdentityDbContext<IdentityUser>
    {
        public LibraryEntities()
            : base("LibraryEntities")
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAmount> BookAmounts { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<BookQrCode> BookQrCodes { get; set; }
        public DbSet<UserBook> UserBooks { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) //учитываем конфигурацию сущностей
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<IncludeMetadataConvention>();

            modelBuilder.Configurations.Add(new AuthorConfiguration());
            modelBuilder.Configurations.Add(new BookConfiguration());
            modelBuilder.Configurations.Add(new BookAmountConfiguration());
            modelBuilder.Configurations.Add(new BookAuthorConfiguration());
            modelBuilder.Configurations.Add(new BookQrCodeConfiguration());
            modelBuilder.Configurations.Add(new UserBookConfiguration());
            modelBuilder.Configurations.Add(new UserProfileConfiguration());
        }
    }
}
