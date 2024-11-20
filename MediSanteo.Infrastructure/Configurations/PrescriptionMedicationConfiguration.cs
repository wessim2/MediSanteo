using MediSanteo.Domain.Prescription;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSanteo.Infrastructure.Configurations
{
    internal class PrescriptionMedicationConfiguration
    {
      /*  public void Configure(EntityTypeBuilder<PrescriptionMedication> builder)
        {
            builder.ToTable("prescription_medication");

            builder.HasKey(x => new {
                x.PrescriptionId,
                x.MedicationId
            });

        }
*/
      
    }
}
