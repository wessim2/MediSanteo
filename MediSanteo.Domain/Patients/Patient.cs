using MediSanteo.Domain.Abstractions;
using MediSanteo.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSanteo.Domain.Patients
{
    public sealed class Patient : Entity
    {
        public Patient(
            Guid Id,
            FullName fullName,
            Email email,
            BirthDate birthDate,
            List<VitalSigns> vitalSignsHistory,
            List<Medication> mediciations
            ) : base(Id)
        {
            FullName = fullName;
            Email = email;
            BirthDate = birthDate;
            VitalSignHistory = vitalSignsHistory;
            Medications = mediciations;
        }

        private Patient() { }
        public FullName FullName { get; private set; }
        public Email Email { get; private set; }
        public BirthDate BirthDate { get; private set; }
        public List<VitalSigns> VitalSignHistory { get; private set; }
        public List<Medication> Medications { get; private set; }
    }
}
