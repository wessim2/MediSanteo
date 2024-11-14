using MediSanteo.Application.Abstractions.Clock;
using MediSanteo.Application.Abstractions.Messaging;
using MediSanteo.Application.Exceptions;
using MediSanteo.Domain.Abstractions;
using MediSanteo.Domain.Consultations;

using MediSanteo.Domain.Users;

namespace MediSanteo.Application.Consultations.ReserveConsultation
{
    internal sealed class ReserveConsultationCommandHandler : ICommandHandler<ReserveConsultationCommand, Guid>
    {

        private readonly IConsultationRepository _consultationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IUserRepository _userRepository;

        public ReserveConsultationCommandHandler(
            IConsultationRepository consultationRepository,
            IUnitOfWork unitOfWork,
            IDateTimeProvider dateTimeProvider,
            IUserRepository userRepository)
        {
            _consultationRepository = consultationRepository;
            _unitOfWork = unitOfWork;
            _dateTimeProvider = dateTimeProvider;
            _userRepository = userRepository;
        }

        public async Task<Result<Guid>> Handle(ReserveConsultationCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _userRepository.GetByIdAsync(request.doctorId, cancellationToken);

            if (doctor is null)
            {
                return Result.Failure<Guid>(UserErrors.NotFound);
            }

            if ( doctor.Roles.Contains(Role.Doctor) )
            {
                return Result.Failure<Guid>(UserErrors.NotDoctor);
            }

            var patient = await _userRepository.GetByIdAsync(request.patientId, cancellationToken);

            if (patient is null)
            {
                return Result.Failure<Guid>(UserErrors.NotFound);
            }
            if (patient.Roles.Contains(Role.Patient))
            {
                return Result.Failure<Guid>(UserErrors.NotPatient);
            }

            if (await _consultationRepository.IsOverlaping(doctor, request.appointmentTime, cancellationToken))
            {
                return Result.Failure<Guid>(ConsultationErrors.Overlap);
            }

            try
            {

            var consultation = Consultation.Book(
                request.patientId,
                request.doctorId,
                request.appointmentTime,
                request.price
                );


            _consultationRepository.Add(consultation);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return consultation.Id;

            } catch(ConcurrencyException)
            {
                return Result.Failure<Guid>(ConsultationErrors.Overlap);
            }
        }
    }
}
