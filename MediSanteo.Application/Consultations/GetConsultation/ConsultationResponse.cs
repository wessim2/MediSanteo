using MediSanteo.Domain.Consultations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSanteo.Application.Consultations.GetConsultation
{
    public class ConsultationResponse
    {
        public Guid PatientId { get; init; }
        public Guid DoctorId { get; init; }
        public int Status { get; init; }
        public DateTime AppointmentTime { get; init; }
        public decimal PriceAmount { get; init; }
        public string PriceCurrency { get; init; }
        public DateTime CreatedOnUtc { get; init; }
        public DateTime? ConfirmedOnUtc { get; init; }
        public DateTime? RejectedOnUtc { get; init; }
        public DateTime? RescheduledOnUtc { get; init; }
    }
}
