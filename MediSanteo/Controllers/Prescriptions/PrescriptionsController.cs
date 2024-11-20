using MediatR;
using MediSanteo.Application.Prescriptions.CreatePrescription;
using Microsoft.AspNetCore.Mvc;

namespace MediSanteo.Controllers.Prescriptions
{
    [Route("api/prescriptions")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly ISender _sender;

        public PrescriptionsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePrescription(
            CreatePresctionRequest request,
            CancellationToken cancellationToken)
        {
            if (request.Medications == null || !request.Medications.Any())
            {
                return BadRequest("Atleast one medication should be in the prescription");
            }

            try
            {
                 var command = new CreatePrescriptionCommand(
                    request.PatientId,
                    request.Instructions,
                    request.Medications);

                var result = await _sender.Send(command, cancellationToken);

                if (result.IsFailure)
                {
                    return BadRequest("Something wrong!");
                }

                return CreatedAtAction(nameof(CreatePrescription), new { id = result.Value }, result.Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException.Message);
                return BadRequest(ex.Message);
            }

        }
    }
}

