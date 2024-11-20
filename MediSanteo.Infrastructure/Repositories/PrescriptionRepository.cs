using MediSanteo.Domain.Prescription;
using MediSanteo.Domain.Users;


namespace MediSanteo.Infrastructure.Repositories
{
    internal sealed class PrescriptionRepository : Repository<Prescription>, IPrescriptionRepository
    {
        public PrescriptionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override void Add(Prescription prescription)
        {
            foreach (var medication in prescription.Medications)
            {
                DbContext.Attach(medication);
            }

            DbContext.Add(prescription);
        }
    }
}
