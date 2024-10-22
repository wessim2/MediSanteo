using MediSanteo.Domain.Abstractions;

namespace MediSanteo.Domain.Users
{
    public static class UserErrors
    {
        public static Error NotFound = new
            ("User.NotFound",
            "The User with this speciic id is not found");

        public static Error InvalidCredentials = new(
            "User.InvalidCredentials",
            "Credentials are invalid."
            );
    }
}
