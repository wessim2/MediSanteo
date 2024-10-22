using MediatR;
using MediSanteo.Application.Consultations.GetConsultation;
using MediSanteo.Application.Consultations.ReserveConsultation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediSanteo.Controllers.Consultations
{
    [Authorize]
    [ApiController]
    [Route("api/consultations")]
    public class ConsultationsController : ControllerBase
    {
        private readonly ISender _sender;

        public ConsultationsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet]
        public async Task<IActionResult> GetConsultation(
            Guid id,
            CancellationToken cancellationToken)
        {
            var query = new GetConsultationQuery(id);

            var result = await _sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ReserveConsultation(
            ReserveConsultationRequest request,
            CancellationToken cancellationToken
            )
        {
            var command = new ReserveConsultationCommand(
                request.patiendId,
                request.doctorId,
                request.appointmentTime.ToUniversalTime(),
                request.price);

            var result = await _sender.Send(command, cancellationToken);

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(nameof(ReserveConsultation), new { id = result.Value }, result.Value);
        }
    }
}
