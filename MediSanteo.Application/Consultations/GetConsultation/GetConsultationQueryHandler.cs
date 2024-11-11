using Dapper;
using MediSanteo.Application.Abstractions.Authentication;
using MediSanteo.Application.Abstractions.Data;
using MediSanteo.Application.Abstractions.Messaging;
using MediSanteo.Domain.Abstractions;
using MediSanteo.Domain.Consultations;


namespace MediSanteo.Application.Consultations.GetConsultation
{
    public sealed class GetConsultationQueryHandler : IQueryHandler<GetConsultationQuery, ConsultationResponse>
    {
        private static readonly int[] ConsultaionStatuses =
        {
        (int)ConsultationStatus.Completed,
        (int)ConsultationStatus.Rescheduled,
        (int)ConsultationStatus.Cancelled,
        (int)ConsultationStatus.Pending,
        (int)ConsultationStatus.Confirmed,

        };

        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IUserContext _userContext;

        public GetConsultationQueryHandler(ISqlConnectionFactory sqlConnectionFactory, IUserContext userContext)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _userContext = userContext;
        }

        public async Task<Result<ConsultationResponse>> Handle(GetConsultationQuery request, CancellationToken cancellationToken)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();

            var sql = """
                SELECT
                id as Id,
                patient_id as PatientId,
                doctor_id as DoctorId,
                status as Status,
                appointment_time as AppointmentTime,
                price_amount as PriceAmount,
                price_currency as PriceCurrency
                FROM public.consultations
                WHERE id = @ConsultationId
                """;

            var consultation = await connection.QueryFirstAsync<ConsultationResponse>(
                sql,
                new
                {
                    request.ConsultationId,
                }
                );

            if ( consultation == null || consultation.PatientId != _userContext.UserId)
            {
                return Result.Failure<ConsultationResponse>(ConsultationErrors.NotFound);
            }

            return consultation;
        }
    }
}
