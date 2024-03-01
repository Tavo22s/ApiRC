using ApiR.Data.Repositories;
using ApiR.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteController(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            return Ok(await _clienteRepository.GetAllClients());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientDetails(int id)
        {
            return Ok(await _clienteRepository.GetClientDetails(id));
        }
        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] Cliente cliente)
        {
            if (cliente == null)
                return BadRequest();
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _clienteRepository.InsertClient(cliente);
            return Created("created", created);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClient([FromBody] Cliente cliente)
        {
            if (cliente == null)
                return BadRequest();
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _clienteRepository.UpdateClient(cliente);
            return NoContent();
        }

        [HttpGet]
        [Route("/api/search/")]
        public async Task<IActionResult> SeachClient(string search)
        {
            return Ok(await _clienteRepository.SeachCliente(search));
        }
        
    }
}
