using MediSanteo.Domain.Abstractions;
using MediSanteo.Domain.Shared;

namespace MediSanteo.Domain.Doctors
{
    public sealed class Doctor : Entity
    {
        public Doctor(
            Guid Id, FullName fullName, Email email, Speciality speciality,PhoneNumber phoneNumber, Address address) : base(Id)
        {
            FullName = fullName;
            Email = email;
            Speciality = speciality;
            PhoneNumber = phoneNumber;
            Address = address;
        }
        public FullName FullName { get; private set; }
        public Email Email { get; private set; }
        public Speciality Speciality { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public Address Address { get; private set; }
    }
}
