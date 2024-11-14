using MediSanteo.Domain.Abstractions;


namespace MediSanteo.Domain.Medications
{
    public sealed class Medication : Entity
    {
        public Medication(Name Name,Description Description,Dosage Dosage)
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
