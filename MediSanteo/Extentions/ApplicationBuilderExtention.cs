using MediSanteo.Infrastructure;
using MediSanteo.Middleware;
using Microsoft.EntityFrameworkCore;

namespace MediSanteo.Extentions
{
    public static class ApplicationBuilderExtention
    {
        public static void ApplyMigrations(this IApplicationBuilder applicationBuilder)
        {
            using var scope = applicationBuilder.ApplicationServices.CreateScope();

            using var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            dbContext.Database.Migrate();
        }
        public static void UseCustomExceptionHandler(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
