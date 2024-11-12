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
        public static Error NotDoctor = new(
            "User.NotDoctor",
            "This User is not a doctor."
            );
        public static Error NotPatient = new(
            "User.NotPatient",
            "This User is not a patient."
            );
        public static Error InvalidRole = new(
        "User.InvalidRole",
        "Role is invalid."
        );
    }
}
