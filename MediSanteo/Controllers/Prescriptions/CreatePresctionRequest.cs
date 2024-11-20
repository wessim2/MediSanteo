namespace MediSanteo.Controllers.Prescriptions
{
    public sealed record CreatePresctionRequest(
         Guid PatientId,
         string Instructions,
         ICollection<Guid> Medications);
}
