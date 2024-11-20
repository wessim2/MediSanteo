using MediSanteo.Domain.Medications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSanteo.Application.Medications.GetMedicationsById
{
    public class MedicationsResponse
    {
        ICollection<Medication> Medications { get; set; }
    }
}
