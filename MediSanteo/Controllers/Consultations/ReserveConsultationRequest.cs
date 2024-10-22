using MediSanteo.Domain.Consultations;

namespace MediSanteo.Controllers.Consultations
{
    public sealed record ReserveConsultationRequest(
        Guid patiendId,
        Guid doctorId,
        DateTime appointmentTime,
        Money price);
}