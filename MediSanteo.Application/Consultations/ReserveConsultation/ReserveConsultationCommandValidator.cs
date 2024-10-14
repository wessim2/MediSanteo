using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediSanteo.Application.Consultations.ReserveConsultation
{
    public class ReserveConsultationCommandValidator : AbstractValidator<ReserveConsultationCommand>
    {
        public ReserveConsultationCommandValidator()
        {
            RuleFor(c => c.doctorId).NotEmpty();

            RuleFor(c => c.patientId).NotEmpty();

            RuleFor(c => c.price).NotEmpty();
        }
    }
}
