using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MediSanteo.Domain.Consultations;
using MediSanteo.Domain.Users;


namespace MediSanteo.Infrastructure.Configurations
{
    internal sealed class ConsulationConfiguration : IEntityTypeConfiguration<Consultation>
    {
        public void Configure(EntityTypeBuilder<Consultation> builder)
        {
            builder.ToTable("consultations");

            builder.HasKey(consultation => consultation.Id);

            builder.Property(consultation => consultation.AppointmentTime)
                .IsRequired();

            builder.OwnsOne(consultation => consultation.Price, priceBuilder =>
            {
                priceBuilder.Property(money => money.Currency)
                .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
            });
               

            builder.HasOne<User>().WithMany()
                .HasForeignKey(consulation => consulation.PatientId);

            builder.HasOne<User>().WithMany()
                .HasForeignKey(consultation => consultation.DoctorId);

            builder.Property<uint>("Version").IsRowVersion();

        }
    }
}
