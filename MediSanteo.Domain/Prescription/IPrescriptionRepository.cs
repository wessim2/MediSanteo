
namespace MediSanteo.Domain.Prescription
{
    public interface IPrescriptionRepository
    {
        Task<Prescription?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        void Add(Prescription prescription);
    }
}
