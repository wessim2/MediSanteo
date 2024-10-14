using MediSanteo.Domain.Doctors;

namespace MediSanteo.Domain.Consultations
{
    public interface IConsultationRepository
    {
        Task<Consultation?> GetByIdAsync(Guid id,CancellationToken cancellationToken);
        void Add(Consultation consultation);

        Task<bool> IsOverlaping(Doctor doctor, DateTime appointmentTime, CancellationToken cancellationToken);
    }
}
