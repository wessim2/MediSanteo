using MediSanteo.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSanteo.Infrastructure.Configurations
{
    internal sealed class RolePermissionsConfiguration : IEntityTypeConfiguration<RolePermission>
    {
        public void Configure(EntityTypeBuilder<RolePermission> builder)
        {
            builder.ToTable("role_permissions");

            builder.HasKey(x => new { x.RoleId, x.PermissionId });

            builder.HasData(new RolePermission
            {
              RoleId = Role.Registered.Id, 
              PermissionId = Permission.UserReads.Id 
            });
        }
    }
}
