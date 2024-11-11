using MediSanteo.Application.Abstractions.Data;
using MediSanteo.Application.Abstractions.Messaging;
using MediSanteo.Domain.Abstractions;
using Dapper;
using MediSanteo.Application.Abstractions.Authentication;
namespace MediSanteo.Application.Users.GetLoggedInUser
{
    internal class GetLoggedInUserQueryHandler : IQueryHandler<GetLoggedInUserQuery, UserResponse>
    {
        private readonly ISqlConnectionFactory _sqlConnectionFactory;
        private readonly IUserContext _userContext;

        public GetLoggedInUserQueryHandler(
            ISqlConnectionFactory sqlConnectionFactory,
            IUserContext userContext)
        {
            _sqlConnectionFactory = sqlConnectionFactory;
            _userContext = userContext;
        }

        public async Task<Result<UserResponse>> Handle(
            GetLoggedInUserQuery request,
            CancellationToken cancellationToken)
        {
            using var connection = _sqlConnectionFactory.CreateConnection();

            const string sql = """
            SELECT
                id AS Id,
                first_name AS FirstName,
                last_name AS LastName,
                email AS Email
            FROM users
            WHERE identity_id = @IdentityId
            """;

            var user = await connection.QuerySingleAsync<UserResponse>(
                sql,
                new
                {
                    _userContext.IdentityId
                });

            return user;
        }
    }
}
