using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;

public class CompanyDBContext(DbContextOptions<CompanyDBContext> options) : 
    IdentityDbContext <ApplicationUsers>(options)
{
    public DbSet<Department> Departments { get; set; }
    public DbSet<Employee> employees { get; set; }
    //public DbSet<ApplicationUsers> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //modelBuilder.ApplyConfiguration();
        //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.Entity<ApplicationUsers>(builder => {
            builder.Property(u => u.FristName)
                .HasColumnType("VarChar")
                .HasMaxLength(265);
            builder.Property(u => u.LastName)
               .HasColumnType("VarChar")
               .HasMaxLength(265);
        });
        //modelBuilder.Ignore<IdentityUserClaim<string>>();
        //AspNetUsers
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CompanyDBContext).Assembly);
    }
}
