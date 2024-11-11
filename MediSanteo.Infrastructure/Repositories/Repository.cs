﻿
using Microsoft.EntityFrameworkCore;
using MediSanteo.Domain.Abstractions;

namespace MediSanteo.Infrastructure.Repositories;

    internal abstract class Repository<T>
        where T : Entity
    {
        protected readonly ApplicationDbContext DbContext;

        protected Repository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await DbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public virtual void Add(T entity)
        {
            DbContext.Add(entity);
        }
    }

