using Bogus;
using Dapper;
using MediSanteo.Application.Abstractions.Data;
using MediSanteo.Domain.Medications;


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

            List<object> medications = new();
            for(var i = 0; i < 10; i++)
            {
                medications.Add(new
                {
                    Id = Guid.NewGuid(), // Use Guid.NewGuid() to ensure uniqueness
                    Name = faker.Commerce.ProductName(),
                    Dosage = faker.PickRandom(new[] { 50, 100, 150, 200 }), // Replace with actual dosage values
                    Description = faker.Lorem.Sentence(), // Generate a random sentence for the description
                });
            }
            
            const string sql = """
            INSERT INTO public.medications
            (id, "name", description, dosage)
            VALUES(@Id, @Name, @Description, @Dosage);
            """;

            connection.Execute(sql, medications);
        }
    }
}
