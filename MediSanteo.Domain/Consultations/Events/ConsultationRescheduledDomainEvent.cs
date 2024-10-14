using MediSanteo.Domain.Abstractions;


namespace MediSanteo.Domain.Consultations.Events
{
    public sealed record ConsultationRescheduledDomainEvent(Guid Id) : IDomainEvent;
}
