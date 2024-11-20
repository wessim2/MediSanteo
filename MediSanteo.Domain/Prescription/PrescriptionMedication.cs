using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSanteo.Domain.Prescription
{
    public class PrescriptionMedication
    {
        public Guid PrescriptionId { get; set; }
        public Guid MedicationId { get; set; }
    }
}
