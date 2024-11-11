using Microsoft.AspNetCore.Authorization;

namespace MediSanteo.Infrastructure.Authorization
{
    internal sealed class PermissionRequirement : IAuthorizationRequirement
    {
        public PermissionRequirement(string permission ) 
        {
            Permission = permission;
        }
        public string Permission { get; }
    }
}
