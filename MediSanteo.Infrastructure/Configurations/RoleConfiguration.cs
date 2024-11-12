﻿using MediSanteo.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSanteo.Infrastructure.Configurations
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("roles");

            builder.HasKey(role => role.Id);

            builder.HasMany(role => role.Permissions)
                .WithMany()
                .UsingEntity<RolePermission>();

            builder.HasData(Role.Registered);
            builder.HasData(Role.Patient);
            builder.HasData(Role.Doctor);
        }
    }
}
