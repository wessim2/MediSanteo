
namespace MediSanteo.Domain.Medications
{
    public interface IMedicationRepository
    {
        Task<Medication?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        void Add(Medication medication);
    }
}
