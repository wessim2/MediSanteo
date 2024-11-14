using MediatR;
using MediSanteo.Application.Abstractions.Email;
using MediSanteo.Domain.Consultations;
using MediSanteo.Domain.Consultations.Events;
using MediSanteo.Domain.Users;


namespace MediSanteo.Application.Consultations.ReserveConsultation
{
    public sealed class ReserveConsultationDomainEventHandler : INotificationHandler<ConsultationReservedDomainEvent>
    {
        private readonly IConsultationRepository _consultationRepository;
        private readonly IEmailService _emailService;
        private readonly IUserRepository _userRepository;

        public ReserveConsultationDomainEventHandler(
            IConsultationRepository consultationRepository,
            IEmailService emailService,
            IUserRepository userRepository)
        {

            _consultationRepository = consultationRepository;
            _emailService = emailService;
            _userRepository = userRepository;
        }

        public async Task Handle(ConsultationReservedDomainEvent notification, CancellationToken cancellationToken)
        {
            var consultation = await _consultationRepository.GetByIdAsync(notification.id, cancellationToken);
            
            if (consultation == null)
            {
                return;
            }

            var patient = await _userRepository.GetByIdAsync(consultation.PatientId, cancellationToken);

            if( patient is null)
            {
                return;
            }

            var doctor = await _userRepository.GetByIdAsync(consultation.DoctorId, cancellationToken);

            if( doctor is null)
            {
                return;
            }
            await _emailService.SendAsync(patient.Email, "Consultation Reserved", "You need to confirm your reservation");
            await _emailService.SendAsync(doctor.Email, "Consultation Has been reserved", "There is a reserved consultation waiting to be confirmed from the patient");


        }
    }
}
