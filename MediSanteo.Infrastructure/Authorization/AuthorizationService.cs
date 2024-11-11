using MediSanteo.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSanteo.Infrastructure.Authorization
{
    public sealed class AuthorizationService
    {
        private readonly ApplicationDbContext _dbContext;
        public AuthorizationService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserRolesResponse> GetRolesUserAsync(string identityId) 
        {
            var roles = await _dbContext.Set<User>()
                .Where(user => user.IdentityId == identityId)
                .Select(user => new UserRolesResponse
                {
                    Id = user.Id,
                    Roles = user.Roles.ToList()
                }).FirstAsync();

            return roles;
        }

        internal async Task<HashSet<string>> GetPermissionsForUserAsync(string identityId)
        {
            var permissions = await _dbContext.Set<User>()
                .Where(user => user.IdentityId == identityId)
                .SelectMany(user => user.Roles.Select(role => role.Permissions))
                .FirstAsync();
    

            var permissionsSet = permissions.Select(permissions => permissions.Name).ToHashSet(); 

            return permissionsSet;

        }
    }
}
