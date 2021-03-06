using EmployeeApp.Model;
using Microsoft.EntityFrameworkCore;
namespace EmployeeApp.Data
{
    public class ApplicationContext :DbContext
    {
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-41RHDMT\\SQLEXPRESS01;Initial Catalog=EmployeeDB;Integrated Security=True");   
        }
    }
}
