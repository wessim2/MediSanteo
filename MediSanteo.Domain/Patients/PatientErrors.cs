using MediSanteo.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSanteo.Domain.Patients
{
    public static class PatientErrors
    {
        public static Error NotFound = new
            ("Patient.NotFound",
            "The Patient with this specific Id was not found");
    }
}
