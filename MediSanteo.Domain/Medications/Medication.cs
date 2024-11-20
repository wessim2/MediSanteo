using MediSanteo.Domain.Abstractions;
using MediSanteo.Domain.Prescription;
using MediSanteo.Domain.Users;


namespace MediSanteo.Domain.Medications
{
    public sealed class Medication : Entity
    {
        public Medication(Guid Id ,Name Name,Description Description,Dosage Dosage) : base(Id)
        {
            this.Name = Name;
            this.Description = Description; 
            this.Dosage = Dosage; 
        }
        private Medication() { }
        public Name Name { get; set; }
        public Description Description { get; set; }
        public Dosage Dosage { get; set; }

    }
}
