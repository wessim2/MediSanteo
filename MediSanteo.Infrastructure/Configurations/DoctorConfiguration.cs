using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MediSanteo.Domain.Doctors;

namespace MediSanteo.Infrastructure.Configurations
{
    internal sealed class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.HasKey(doctor => doctor.Id);
            
            builder.OwnsOne(doctor => doctor.Address);

            builder.Property(doctor => doctor.Email)
                .HasConversion(email => email.Value, value => new Domain.Shared.Email(value));

            builder.HasIndex(doctor => doctor.Email).IsUnique();

            builder.OwnsOne(doctor => doctor.FullName);

            builder.Property(doctor => doctor.Speciality)
                .HasConversion(speciality => speciality.Value, value => new Speciality(value));

            builder.Property(doctor => doctor.PhoneNumber)
                .HasConversion(phoneNumber => phoneNumber.Value , value => new PhoneNumber(value));
                
        }
    }
}
