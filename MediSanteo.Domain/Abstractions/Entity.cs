namespace MediSanteo.Domain.Abstractions
{
    public class Entity
    {
        private readonly List<IDomainEvent> _domainEvents = new();  
        protected Entity ( Guid Id )
        {
            this.Id = Id;
        }

        protected Entity() { }
        public Guid Id { get; init; }
        public IReadOnlyList<IDomainEvent> GetDomainEvents()
        {
            return _domainEvents.ToList();
        }
        public void ClearDomainEvents ()
        {
            _domainEvents.Clear();
        }
        public void RaiseDomainEvent( IDomainEvent domainEvent )
        {
            _domainEvents.Add( domainEvent );
        }
    }
}
