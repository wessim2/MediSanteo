using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediSanteo.Application.Abstractions.Data;
using MediSanteo.Application.Abstractions.Email;
using MediSanteo.Domain.Abstractions;
using MediSanteo.Domain.Consultations;
using MediSanteo.Infrastructure.Data;
using MediSanteo.Infrastructure.Email;
using MediSanteo.Infrastructure.Repositories;
using MediSanteo.Application.Abstractions.Clock;
using MediSanteo.Infrastructure.Clock;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using MediSanteo.Infrastructure.Authentication;
using MediSanteo.Application.Abstractions.Authentication;
using Microsoft.Extensions.Options;
using MediSanteo.Domain.Users;
using MediSanteo.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authorization;
using MediSanteo.Domain.Prescription;
using MediSanteo.Domain.Medications;


namespace MediSanteo.Infrastructure
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            services.AddTransient<IEmailService,EmailService>();

            AddPersistence(services,configuration);
            AddAuthentication(services,configuration);
            AddAuthorization(services);
            return services;
        }
        public static void AddPersistence(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database") ??
             throw new ArgumentNullException(nameof(configuration));

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
            });

            services.AddScoped<IConsultationRepository, ConsultationRepository>();
            services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
            services.AddScoped<IMedicationRepository, MedicationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

            services.AddSingleton<ISqlConnectionFactory>(_ =>
            new SqlConnectionFactory(connectionString));

            SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        }
    
        public static void AddAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer();

            services.Configure<AuthenticationOptions>(configuration.GetSection("Authentication"));

            services.ConfigureOptions<JwtBearerOptionsSetup>();

            services.Configure<KeycloakOptions>(configuration.GetSection("Keycloak"));
            services.AddTransient<AdminAuthorizationDelegatingHandler>();

            services.AddHttpClient<IAuthenticationService, AuthenticationService>((serviceProvider, httpClient) =>
            {
                var keycloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;

                httpClient.BaseAddress = new Uri(keycloakOptions.AdminUrl);
            })
        .AddHttpMessageHandler<AdminAuthorizationDelegatingHandler>();

            services.AddHttpClient<IJwtService, JwtService>((serviceProvider, httpClient) =>
            {
                var keycloakOptions = serviceProvider.GetRequiredService<IOptions<KeycloakOptions>>().Value;

                httpClient.BaseAddress = new Uri(keycloakOptions.TokenUrl);
            });
            services.AddHttpContextAccessor();

            services.AddScoped<IUserContext, UserContext>();

        }

        public static void AddAuthorization(IServiceCollection services)
        {
            services.AddScoped<AuthorizationService>();
            services.AddTransient<Microsoft.AspNetCore.Authentication.IClaimsTransformation, CustomClaimTransformation>();
            services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
        }
    }
}
