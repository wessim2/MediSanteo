using MediSanteo.Domain.Users;

namespace MediSanteo.Domain.Consultations
{
    public interface IConsultationRepository
    {
        Task<Consultation?> GetByIdAsync(Guid id,CancellationToken cancellationToken);
        void Add(Consultation consultation);

        Task<bool> IsOverlaping(User doctor, DateTime appointmentTime, CancellationToken cancellationToken);
    }
}
