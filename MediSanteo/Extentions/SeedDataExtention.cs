using Bogus;
using Dapper;
using MediSanteo.Application.Abstractions.Data;
using MediSanteo.Domain.Doctors;
using MediSanteo.Domain.Patients;

namespace MediSanteo.Extentions
{
    public static class SeedDataExtention
    {
        public static void SeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var sqlConnectionFactory = scope.ServiceProvider.GetRequiredService<ISqlConnectionFactory>();

            using var connection = sqlConnectionFactory.CreateConnection();

            var faker = new Faker();

            List<object> doctors = new();
            for(var i = 0; i < 10; i++)
            {
                doctors.Add(new
                {
                    Id = new Guid(),
                    FirstName = faker.Person.FirstName,
                    LastName = faker.Person.LastName,
                    Email = faker.Person.Email,
                    BirthDate = faker.Person.DateOfBirth,
                });
            }
            const string sql = """
            INSERT INTO public.patient
            (id, full_name_first_name, full_name_last_name, email, birth_date)
            VALUES(@Id, @FirstName, @LastName, @Email, @BirthDate);
            """;

            connection.Execute(sql, doctors);
        }
    }
}
