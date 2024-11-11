using MediSanteo.Domain.Abstractions;
using MediSanteo.Domain.Consultations.Events;


namespace MediSanteo.Domain.Consultations
{
    public sealed class Consultation : Entity
    {
        public Consultation(
            Guid Id,
            Guid patientId,
            Guid doctorId,
            ConsultationStatus status,
            DateTime appointmentTime,
            DateTime createdOnUtc,
            Money price
            ) : base(Id)
        {
            this.PatientId = patientId;
            this.DoctorId = doctorId;
            this.Status = status;
            this.AppointmentTime = appointmentTime;
            this.CreatedOnUtc = createdOnUtc;
            this.Price = price;
        }

        private Consultation() { }
        public Guid PatientId { get; private set; }
        public Guid DoctorId { get; private set; }
        public ConsultationStatus Status { get; private set; }
        public DateTime AppointmentTime { get; private set; }
        public Money Price { get; private set; }
        public DateTime CreatedOnUtc { get; private set; }
        public DateTime? ConfirmedOnUtc { get; private set; }
        public DateTime? RejectedOnUtc { get; private set; }
        public DateTime? RescheduledOnUtc { get; private set; }

        public static Consultation Book(Guid patientId,
            Guid doctorId,
            DateTime appointmentTime,
            Money price)
        {
            var consultation = new Consultation(new Guid(), patientId, doctorId, ConsultationStatus.Pending ,appointmentTime ,DateTime.UtcNow, price);

            consultation.RaiseDomainEvent(new ConsultationReservedDomainEvent(consultation.Id));
            
            return consultation;
        }


        public Result Confirm(DateTime utcNow)
        {
            if(Status != ConsultationStatus.Pending)
            {
                return Result.Failure(ConsultationErrors.NotPending);
            }
            Status = ConsultationStatus.Confirmed;
            ConfirmedOnUtc = utcNow;

           RaiseDomainEvent(new ConsultationConfirmedDomainEvent(Id));

            return Result.Success();
        }

        public Result Reject(DateTime utcNow)
        {
            if(Status != ConsultationStatus.Pending)
            {
                return Result.Failure(ConsultationErrors.NotPending);
            }
            Status = ConsultationStatus.Rejected;
            RejectedOnUtc = utcNow;

            RaiseDomainEvent(new ConsultationRejectedDomainEvent(Id));
            return Result.Success();
        }

        public Result Reschedule(DateTime utcNow,DateTime newDate)
        {
            if(Status != ConsultationStatus.Confirmed)
            {
                return Result.Failure(ConsultationErrors.NotConfirmed);
            }
            Status = ConsultationStatus.Rescheduled;
            AppointmentTime = newDate;
            RescheduledOnUtc = utcNow;

            RaiseDomainEvent(new ConsultationRescheduledDomainEvent(Id));
            return Result.Success();   
        }
    }
}
