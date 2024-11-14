using MediSanteo.Domain.Medications;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace MediSanteo.Infrastructure.Configurations
{
    public class MedicationConfiguration : IEntityTypeConfiguration<Medication>
    {
        public void Configure(EntityTypeBuilder<Medication> builder)
        {
            builder.ToTable("medications");
            builder.Property(m => m.Name)
                .HasConversion(m => m.Value, value => new Name(value))
                .IsRequired();
            builder.Property(m => m.Description)
                .HasConversion(m => m.Value, value => new Description(value))
                .IsRequired();
            builder.Property(m => m.Dosage)
                .HasConversion(m => m.Value, value => new Dosage(value))
                .IsRequired();
        }
    }
}
