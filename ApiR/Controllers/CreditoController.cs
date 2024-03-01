using ApiR.Data.Repositories;
using ApiR.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditoController : ControllerBase
    {
        private readonly ICreditoRepository _creditoRepository;
        public CreditoController(ICreditoRepository creditoRepository)
        {
            _creditoRepository = creditoRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] Credito credito)
        {
            if (credito == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _creditoRepository.InsertCredit(credito);
            return Created("created", created);
        }
    }
}
