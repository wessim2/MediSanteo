using Dapper;
using MediSanteo.Application.Abstractions.Data;
using MediSanteo.Application.Abstractions.Messaging;
using MediSanteo.Domain.Abstractions;


namespace MediSanteo.Application.Consultations.GetConsultation
{
    public sealed class GetConsultationQueryHandler : IQueryHandler<GetConsultationQuery, ConsultationResponse>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;

        public GetConsultationQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
        }

        public async Task<Result<ConsultationResponse>> Handle(GetConsultationQuery request, CancellationToken cancellationToken)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();

            var sql = """
                SELECT 
                    id as Id,
                    status as Status
                FROM consultations
                WHERE id = @ConsultationId
                """;

            var consultation = await connection.QueryFirstAsync<ConsultationResponse>(
                sql,
                new
                {
                    request.ConsultationId,
                }
                );

            return consultation;
        }
    }
}
