namespace MediSanteo.Controllers.Medications
{
    internal sealed record MedicationsRequest(ICollection<Guid> Ids);

}