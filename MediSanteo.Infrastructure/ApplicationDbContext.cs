﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using MediSanteo.Application.Exceptions;
using MediSanteo.Domain.Abstractions;

namespace MediSanteo.Infrastructure
{
    public class ApplicationDbContext : DbContext , IUnitOfWork
    {
        private readonly IPublisher _publisher;
        public ApplicationDbContext(DbContextOptions options, IPublisher publisher) : base(options)
        {
            _publisher = publisher;
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
            var result = await base.SaveChangesAsync(cancellationToken);

            await PublishDomainEventAsync();

            return result;

            } catch(DbUpdateConcurrencyException ex)
            {
                throw new ConcurrencyException("Concurrency Exception has occured", ex);
            }
        }

        private async Task PublishDomainEventAsync()
        {
            var domainEvents = ChangeTracker
                .Entries<Entity>()
                .Select(entry => entry.Entity)
                .SelectMany(entity =>
                {
                    var domainEvents = entity.GetDomainEvents();

                    entity.ClearDomainEvents();

                    return domainEvents;
                }).ToList();

            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent);
            }
        }
    }
}
