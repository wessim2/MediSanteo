﻿using MediSanteo.Domain.Users;

namespace MediSanteo.Infrastructure.Repositories
{
    internal sealed class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override void Add(User user)
        {
            foreach (var role in user.Roles)
            {
                DbContext.Attach(role);   
            }

            DbContext.Add(user);
        }
    }
}
