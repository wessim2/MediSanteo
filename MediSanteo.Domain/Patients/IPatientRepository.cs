using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSanteo.Domain.Patients
{
    public interface IPatientRepository
    {
        Task<Patient?> GetByIdAsync(Guid id,CancellationToken cancellationToken);
        void Add(Patient patient);
    }
}
