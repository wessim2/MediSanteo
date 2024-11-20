using MediatR;
using MediSanteo.Application.Medications.GetMedicationsById;
using Microsoft.AspNetCore.Mvc;

namespace MediSanteo.Controllers.Medications
{
    [Route("api/medications")]
    [ApiController]
    public class MedicationsController : ControllerBase
    {
        private readonly ISender _sender;

        public MedicationsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> GetMedicationsByIds([FromBody]  IList<Guid> Ids, CancellationToken cancellationToken)
        {
            var query = new GetMedicationsByIdQuery(Ids);

            var result = await _sender.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
