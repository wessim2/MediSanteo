using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSanteo.Domain.Doctors
{
    public interface IDoctorRepository
    {
        Task<Doctor?> GetByIdAsync(Guid id,CancellationToken cancellationToken);
        void Add(Doctor doctor);
    }
}
