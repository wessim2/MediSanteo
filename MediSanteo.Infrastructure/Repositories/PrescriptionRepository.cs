using MediSanteo.Domain.Prescription;


namespace MediSanteo.Infrastructure.Repositories
{
    internal sealed class PrescriptionRepository : Repository<Prescription>, IPrescriptionRepository
    {
        public PrescriptionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
