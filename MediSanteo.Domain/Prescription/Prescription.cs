using MediSanteo.Domain.Abstractions;
using MediSanteo.Domain.Medications;
using MediSanteo.Domain.Users;


namespace MediSanteo.Domain.Prescription
{
 
    public class Prescription : Entity
    {
        public Prescription(
            Guid Id,
            DateOnly CreationDate,
            Guid PatientId,
            Instructions Instructions
            ):base(Id) 
            {
            this.CreationDate = CreationDate;
            this.PatientId = PatientId;
            this.Instructions = Instructions;
            }
        private Prescription() { }
        public DateOnly CreationDate { get; private set; }
        public Guid PatientId { get; private set; }
        public Instructions Instructions { get; private set; }
        public ICollection<Medication> Medications = [];
        
        public static Prescription Create(
            Guid patientId,
            Instructions instructions
            ) 
        {
            var prescription = new Prescription(new Guid(),DateOnly.FromDateTime(DateTime.Now),patientId, instructions);

            return prescription;
        }
    }
}
