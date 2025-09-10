
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class DepartmentConfigurations : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.Property(d=> d.ID)
            .UseIdentityColumn(10,10);
        builder.Property(d => d.Name)
            .IsRequired(true)
            .HasMaxLength(50)
            .HasColumnType("VarChar");
        builder.Property(d => d.Description)
            .IsRequired(true)
            .HasMaxLength(50)
            .HasColumnType("VarChar");
        builder.Property(d => d.Code)
            .IsRequired(true)
            .HasMaxLength(50)
            .HasColumnType("VarChar");
        builder.Property(x => x.CreatedOn)
            .HasDefaultValueSql("GETDATE()");
    }
}
