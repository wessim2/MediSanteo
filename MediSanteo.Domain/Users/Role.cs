using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSanteo.Domain.Users
{
    public sealed class Role
    {
        public static readonly Role Registered = new(1, "Registered");
        public static readonly Role Doctor = new(2, "Doctor");
        public static readonly Role Patient = new(3, "Patient");
        public static readonly Role Assistant = new(4, "Assistant");
        public Role(int id, string name)
        {
            Id = id; 
            Name = name;   
        }
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;

        public ICollection<User> Users { get; init; } = new List<User>();
        public ICollection<Permission> Permissions { get; init; } = new List<Permission>();
    }
}
