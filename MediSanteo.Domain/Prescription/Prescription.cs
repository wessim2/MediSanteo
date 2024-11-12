using MediSanteo.Domain.Abstractions;
using MediSanteo.Domain.Medications;


namespace MediSanteo.Domain.Prescription
{
    public class Prescription : Entity
    {
        public Prescription(
            Guid Id,
            DateOnly CreationDate,
            Guid PatientId,
            Instructions Instructions,
            ICollection<Medication> Medications
            ):base(Id) 
            {
            this.CreationDate = CreationDate;
            this.PatientId = PatientId;
            this.Instructions = Instructions;
            this.Medications = Medications;
            }
        private Prescription() { }
        public DateOnly CreationDate { get; private set; }
        public Guid PatientId { get; private set; }
        public Instructions Instructions { get; private set; }
        public ICollection<Medication> Medications { get; private set; } = new List<Medication>();
        
        public static Prescription Create(
            Guid patientId,
            Instructions instructions, 
            ICollection<Medication> medications) 
        {
            var prescription = new Prescription(new Guid(),DateOnly.FromDateTime(DateTime.Now),patientId, instructions, medications);

            return prescription;
        }
    }
}
