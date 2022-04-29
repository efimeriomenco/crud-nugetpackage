using CRUD.Repository.Model;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Repository.Repositories
{
    public class CrudDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }

        public CrudDbContext(DbContextOptions<CrudDbContext> options)
        : base(options)
        {
        }

        protected void OnConfiguring(DbContextOptionsBuilder optionsBuidler)
        {
            if (!optionsBuidler.IsConfigured)
            {
                optionsBuidler.UseSqlServer("Data Source=DESKTOP-J32FIV4\\SQLEXPRESS;Database=CrudDb;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonMapper());
        }
    }


}
