using MediSanteo.Domain.Medications;
using MediSanteo.Domain.Prescription;
using MediSanteo.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSanteo.Infrastructure.Configurations
{
    internal class PrescriptionConfiguration : IEntityTypeConfiguration<Prescription>
    {
        public void Configure(EntityTypeBuilder<Prescription> builder)
        {
            builder.ToTable("prescriptions");
            builder.HasKey(prescription => prescription.Id);
            builder.HasOne<User>().WithMany()
                .HasForeignKey(prescription => prescription.PatientId);

            builder.Property(prescription => prescription.CreationDate);
            builder.Property(prescription => prescription.Instructions)
                .HasConversion(instructions => instructions.Value, value => new Instructions(value))
                .IsRequired();

            builder.HasMany(p => p.Medications)
                .WithMany()
                .UsingEntity<PrescriptionMedication>();   

        }
    }
}
