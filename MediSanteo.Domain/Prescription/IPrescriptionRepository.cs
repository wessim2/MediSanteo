using MediSanteo.Domain.Doctors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSanteo.Domain.Prescription
{
    public interface IPrescriptionRepository
    {
        Task<Prescription?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        void Add(Prescription prescription);
    }
}
