using MediSanteo.Domain.Abstractions;


namespace MediSanteo.Domain.Consultations.Events
{
    public sealed record ConsultationRejectedDomainEvent(Guid Id) : IDomainEvent;
}
