using MediSanteo.Domain.Medications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSanteo.Infrastructure.Repositories
{
    internal sealed class MedicationRepository : Repository<Medication>, IMedicationRepository
    {
        public MedicationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
