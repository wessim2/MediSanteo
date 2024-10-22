using MediatR;
using MediSanteo.Application.Abstractions.Email;
using MediSanteo.Domain.Consultations;
using MediSanteo.Domain.Consultations.Events;
using MediSanteo.Domain.Doctors;
using MediSanteo.Domain.Patients;

namespace MediSanteo.Application.Consultations.ReserveConsultation
{
    public sealed class ReserveConsultationDomainEventHandler : INotificationHandler<ConsultationReservedDomainEvent>
    {
        private readonly IConsultationRepository _consultationRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IEmailService _emailService;
        private readonly IDoctorRepository _doctorRepository;

        public ReserveConsultationDomainEventHandler(
            IConsultationRepository consultationRepository,
            IDoctorRepository doctorRepository,
            IPatientRepository patientRepository,
            IEmailService emailService)
        {
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _consultationRepository = consultationRepository;
            _emailService = emailService;
        }

        public async Task Handle(ConsultationReservedDomainEvent notification, CancellationToken cancellationToken)
        {
            var consultation = await _consultationRepository.GetByIdAsync(notification.id, cancellationToken);
            
            if (consultation == null)
            {
                return;
            }

            var patient = await _patientRepository.GetByIdAsync(consultation.PatientId, cancellationToken);

            if( patient is null)
            {
                return;
            }

            var doctor = await _doctorRepository.GetByIdAsync(consultation.DoctorId, cancellationToken);

            if( doctor is null)
            {
                return;
            }
            await _emailService.SendAsync(patient.Email, "Consultation Reserved", "You need to confirm your reservation");
            await _emailService.SendAsync(doctor.Email, "Consultation Has been reserved", "There is a reserved consultation waiting to be confirmed from the patient");


        }
    }
}
