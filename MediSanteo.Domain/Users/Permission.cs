

namespace MediSanteo.Domain.Users
{
    public sealed class Permission
    {
        public static readonly Permission UserReads = new(1, "users:read"); 
        private Permission(int id, string name) 
        { 
            Id = id;
            Name = name;   
        }
        public int Id { get; init; }
        public string Name { get; init; }
    }
}
