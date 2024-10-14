using MediSanteo.Application.Abstractions.Messaging;
using MediSanteo.Application.Exceptions;
using MediSanteo.Domain.Abstractions;
using MediSanteo.Domain.Consultations;
using MediSanteo.Domain.Doctors;
using MediSanteo.Domain.Patients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSanteo.Application.Consultations.ReserveConsultation
{
    internal sealed class ReserveConsultationCommandHandler : ICommandHandler<ReserveConsultationCommand, Guid>
    {
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IConsultationRepository _consultationRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ReserveConsultationCommandHandler(
            IConsultationRepository consultationRepository,
            IDoctorRepository doctorRepository,
            IPatientRepository patientRepository,
            IUnitOfWork unitOfWork)
        {
            _consultationRepository = consultationRepository;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(ReserveConsultationCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _doctorRepository.GetByIdAsync(request.doctorId, cancellationToken);

            if (doctor is null)
            {
                return Result.Failure<Guid>(DoctorErrors.NotFound);
            }

            var patient = await _patientRepository.GetByIdAsync(request.patientId, cancellationToken);

            if (patient is null)
            {
                return Result.Failure<Guid>(PatientErrors.NotFound);
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
