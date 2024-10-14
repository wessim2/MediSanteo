using Microsoft.EntityFrameworkCore;
using MediSanteo.Domain.Consultations;
using MediSanteo.Domain.Doctors;
using MediSanteo.Infrastructure.Repositories;

namespace MediSanteo.Infrastructure.Repositories
{
    internal sealed class ConsultationRepository : Repository<Consultation>, IConsultationRepository
    {
        private static readonly ConsultationStatus[] ActiveConsultationStatuses = { 
            ConsultationStatus.Confirmed,
            ConsultationStatus.Completed,
            ConsultationStatus.Pending
        };
        public ConsultationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> IsOverlaping(Doctor doctor, DateTime appointmentTime, CancellationToken cancellationToken)
        {
            return await DbContext.Set<Consultation>()
                .AnyAsync(consultation =>
                consultation.DoctorId == doctor.Id &&
                consultation.AppointmentTime == appointmentTime &&
                ActiveConsultationStatuses.Contains(consultation.Status),
                cancellationToken);
        }
    }
}
