using MediSanteo.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSanteo.Domain.Doctors
{
    public static class DoctorErrors
    {
        public static Error NotFound = new
            ("Doctor.NotFound",
            "The Doctor with this speciic id is not found");
    }
}
