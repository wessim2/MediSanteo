using MediSanteo.Application.Abstractions.Messaging;
using MediSanteo.Domain.Abstractions;
using MediSanteo.Domain.Prescription;
using MediSanteo.Domain.Users;

namespace MediSanteo.Application.Prescriptions.CreatePrescription
{
    internal class CreatePrescriptionCommandHandler : ICommandHandler<CreatePrescriptionCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPrescriptionRepository _prescriptionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePrescriptionCommandHandler(IUserRepository userRepository, IPrescriptionRepository prescriptionRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _prescriptionRepository = prescriptionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Guid>> Handle(CreatePrescriptionCommand request, CancellationToken cancellationToken)
        {
            var patient = await _userRepository.GetByIdAsync(request.PatientId, cancellationToken);
            if (patient == null)
            {
                return Result.Failure<Guid>(UserErrors.NotFound);
            }

            if (patient.Roles.Contains(Role.Patient))
            {
                return Result.Failure<Guid>(UserErrors.NotPatient);
            }

            var prescription = Prescription.Create(
                request.PatientId,
                new Instructions(request.Instructions),
                request.Medications);

            _prescriptionRepository.Add(prescription);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return prescription.Id;

        }
    }
}
