using System.Reflection;

public class CompanyDBContext(DbContextOptions<CompanyDBContext> options) : DbContext(options)
{
    public DbSet<Department> Departments { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfiguration();
        //modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CompanyDBContext).Assembly);
    }
}
