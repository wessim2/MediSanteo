using MediSanteo.Domain.Abstractions;



namespace MediSanteo.Domain.Consultations.Events
{
    public sealed record ConsultationReservedDomainEvent(Guid id) : IDomainEvent;
}
