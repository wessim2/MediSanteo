using MediSanteo.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSanteo.Application.Consultations.GetConsultation
{
    public sealed record GetConsultationQuery(Guid ConsultationId) : IQuery<ConsultationResponse>;
  
}
