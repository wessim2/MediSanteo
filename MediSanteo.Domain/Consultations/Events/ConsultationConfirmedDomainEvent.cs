using MediSanteo.Domain.Abstractions;


namespace MediSanteo.Domain.Consultations.Events
{
    public sealed record ConsultationConfirmedDomainEvent(Guid Id) : IDomainEvent;
}
