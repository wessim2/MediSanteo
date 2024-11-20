using Dapper;
using MediSanteo.Application.Abstractions.Authentication;
using MediSanteo.Application.Abstractions.Data;
using MediSanteo.Application.Abstractions.Messaging;
using MediSanteo.Application.Consultations.GetConsultation;
using MediSanteo.Domain.Abstractions;
using MediSanteo.Domain.Consultations;
using MediSanteo.Domain.Medications;


namespace MediSanteo.Application.Medications.GetMedicationsById
{
    public sealed class GetMedicationsByIdQueryHandler : IQueryHandler<GetMedicationsByIdQuery, IReadOnlyCollection<Medication>>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetMedicationsByIdQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<IReadOnlyCollection<Medication>>> Handle(GetMedicationsByIdQuery request, CancellationToken cancellationToken)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();

            var sql = """
            SELECT
                id AS Id,
                name AS Name,
                description AS Description,
                dosage AS Dosage
            FROM public.medications
            WHERE id = ANY(@Ids)
        """;

            var medications = (await connection.QueryAsync<Medication>(
                sql,
                new { request.Ids } 
            )).AsList();

            if (medications == null)
            {
                return Result.Failure<IReadOnlyCollection<Medication>>(MedicationErrors.NotFound);
            }

            return medications;
        }
    }
}
