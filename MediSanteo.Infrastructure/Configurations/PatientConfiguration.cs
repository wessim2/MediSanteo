using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MediSanteo.Domain.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSanteo.Infrastructure.Configurations
{
    internal sealed class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.HasKey(patient => patient.Id);

            builder.Property(patient => patient.Email)
                .IsRequired()
                .HasConversion(email => email.Value, value => new Domain.Shared.Email(value));

            builder.HasIndex(patient => patient.Email).IsUnique();

            builder.OwnsOne(patient => patient.FullName);

            builder.Property(patient => patient.BirthDate)
                .HasConversion(birthDate => birthDate.Value, value => new BirthDate(value));

            builder.OwnsMany(patient => patient.Medications);

            builder.OwnsMany(patient => patient.VitalSignHistory);
        }
    }
}
