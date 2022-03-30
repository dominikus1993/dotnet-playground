using Microsoft.EntityFrameworkCore;

namespace Cancellations
{
    public class PersonsDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; } = null!;

        public PersonsDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }


    public static class PersonsDbContextExtensions
    {
        public static async Task EnsureSeedData(this PersonsDbContext context)
        {
            context.Database.EnsureCreated();
            if (await context.Persons.AnyAsync())
            {
                return;
            }

            context.Persons.AddRange(
                new Person { Id = Guid.NewGuid(), Name = "John Doe" },
                new Person { Id = Guid.NewGuid(), Name = "Jane Doe2" }
            );
            await context.SaveChangesAsync();
        }
    }
}
