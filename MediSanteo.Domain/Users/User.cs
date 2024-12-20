﻿using MediSanteo.Domain.Abstractions;
using MediSanteo.Domain.Shared;
using MediSanteo.Domain.Users.Events;


namespace MediSanteo.Domain.Users
{
    public sealed class User : Entity
    {
        private readonly List<Role> _roles = new();
        private User(Guid id,
            FirstName firstName,
            LastName lastName,
            Email email) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;   
            Email = email;
        }
        public FirstName FirstName{ get; private set; }
        public LastName LastName{ get; private set; }
        public Email Email { get; private set; }
        public string IdentityId { get; private set; }
        public IReadOnlyCollection<Role> Roles => _roles.ToList();

        public static User CreatePatient(FirstName firstName,LastName lastName, Email email)
        {
            var user = new User(Guid.NewGuid(), firstName, lastName, email);

            user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

            user._roles.Add(Role.Patient);

            return user;
        }

        public static User CreateDoctor(FirstName firstName, LastName lastName, Email email)
        {
            var user = new User(Guid.NewGuid(), firstName, lastName, email);

            user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

            user._roles.Add(Role.Doctor);

            return user;
        }

        public static User CreateAssistant(FirstName firstName, LastName lastName, Email email)
        {
            var user = new User(Guid.NewGuid(), firstName, lastName, email);

            user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

            user._roles.Add(Role.Assistant);

            return user;
        }
        public void SetIdentityId(string identityId)
        {
            IdentityId = identityId;
        }
    }
}
