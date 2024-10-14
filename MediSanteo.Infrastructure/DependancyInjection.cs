using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediSanteo.Application.Abstractions.Data;
using MediSanteo.Application.Abstractions.Email;
using MediSanteo.Domain.Abstractions;
using MediSanteo.Domain.Consultations;
using MediSanteo.Domain.Doctors;
using MediSanteo.Domain.Patients;
using MediSanteo.Infrastructure.Data;
using MediSanteo.Infrastructure.Email;
using MediSanteo.Infrastructure.Repositories;


namespace MediSanteo.Infrastructure
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<IEmailService,EmailService>();

            var connectionString = configuration.GetConnectionString("Database") ??
                throw new ArgumentNullException(nameof(configuration));

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
            });


            services.AddScoped<IConsultationRepository, ConsultationRepository>();
            services.AddScoped<IDoctorRepository, DoctorRepository>();
            services.AddScoped<IPatientRepository, PatientRepository>();

            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

            services.AddSingleton<ISqlConnectionFactory>(_ => 
            new SqlConnectionFactory(connectionString));

            SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());

            return services;
        }
    }
}
