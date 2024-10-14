using MediSanteo.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSanteo.Domain.Consultations
{
    public static class ConsultationErrors
    {
        public static Error NotFound = new
            ("Consultation.NotFound",
             "The Consultation with this speciic id is not found");
        public static Error Overlap = new
            ("Consultation.Overlap",
            "The consultation is overlapping with an existing one");
        public static Error NotPending = new
            ("Consultation.NotPending",
            "The consultation must be pending");
        public static Error NotConfirmed = new
            ("Consultation.NotConfirmed",
            "The consultation must be Confirmed");

    }
}
