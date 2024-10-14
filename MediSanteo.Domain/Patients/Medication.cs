namespace MediSanteo.Domain.Patients
{
    public record Medication(
        string Name,
        decimal Dosage,
        string Notes,
        DateTime StartDate,
        DateTime EndDate
        );
}