using MediSanteo.Domain.Prescription;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace MediSanteo.Infrastructure.Configurations
{
    public class PrescriptionMedicationsConfiguration : IEntityTypeConfiguration<PrescriptionMedicament>
    {
        public void Configure(EntityTypeBuilder<PrescriptionMedicament> builder)
        {
            builder.ToTable("prescription_medications");
            builder.HasKey(x => new { x.PrescriptionId , x.MedicationId});
        }
    }
}
