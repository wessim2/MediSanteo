using MediSanteo.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSanteo.Domain.Medications
{
    public static class MedicationErrors
    {
        public static Error NotFound = new
            ("Medication.NotFound",
             "Medications Not found");
    }
}
