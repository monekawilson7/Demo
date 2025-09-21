using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Context.Configurations;
internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
{
    public void Configure(EntityTypeBuilder<Employee> builder)
    {
        builder.Property(e => e.Name)
            .HasColumnType("varchar")
               .IsRequired()
               .HasMaxLength(30);
        builder.Property(e => e.Email)
            .HasColumnType("varchar")
               .IsRequired(false)
               .HasMaxLength(30);

        builder.Property(e => e.PhoneNumber)
            .HasColumnType("char")
               .IsRequired(false)
               .HasMaxLength(11);

        builder.Property(e => e.Salary)
            .HasColumnType("decimal(10,2)")
               .IsRequired();
        builder.Property(e => e.Gender)
            .HasConversion(x => x.ToString(),
            s => Enum.Parse<Gender>(s));
        builder.Property(e => e.EmployeeType)
           .HasConversion(x => x.ToString(),
           s => Enum.Parse<EmployeeType>(s));
    }
}