using MediSanteo.Application.Abstractions.Messaging;
using MediSanteo.Domain.Medications;


namespace MediSanteo.Application.Prescriptions.CreatePrescription
{
    public sealed record CreatePrescriptionCommand
    ( Guid PatientId,
      string Instructions,
      ICollection<Medication> Medications
     ) : ICommand<Guid>; 
}
