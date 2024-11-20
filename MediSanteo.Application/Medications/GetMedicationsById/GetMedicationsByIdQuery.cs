using MediSanteo.Application.Abstractions.Messaging;
using MediSanteo.Domain.Medications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSanteo.Application.Medications.GetMedicationsById
{
    public sealed record GetMedicationsByIdQuery
    (ICollection<Guid> Ids) : IQuery<IReadOnlyCollection<Medication>> ;
}
