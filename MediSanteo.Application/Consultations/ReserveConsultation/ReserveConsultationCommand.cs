using MediSanteo.Application.Abstractions.Messaging;
using MediSanteo.Domain.Consultations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSanteo.Application.Consultations.ReserveConsultation
{
    public sealed record ReserveConsultationCommand
     (Guid patientId,
      Guid doctorId,
      DateTime appointmentTime,
      Money price) : ICommand<Guid>;

}
