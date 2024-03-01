using ApiR.Data.Repositories;
using ApiR.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;

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
            var clientes = await _clienteRepository.GetAllClients();
            return Ok(await _clienteRepository.GetAllClients());
        }

        [HttpGet]
        [Route("/api/Cliente/WithCredits")]
        public async Task<IActionResult> GetAllClientsWhitCredits()
        {
            var clientes = await _clienteRepository.GetAllClientsWithCredits();
            var json = SerializeObject(clientes);
            return Ok(json);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientDetails(int id)
        {
            return Ok(await _clienteRepository.GetClientDetails(id));
        }

        [HttpGet("WithCredits/{id}")]
        public async Task<IActionResult> GetClientDetailsWithCredits(int id)
        {
            var clientes = await _clienteRepository.GetClientDetailsWithCredits(id);
            var json = SerializeObject(clientes);
            return Ok(json);
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

        [HttpGet]
        [Route("/api/searchWithCredits/")]
        public async Task<IActionResult> SeachClientWithCredits(string search)
        {
            var clientes = await _clienteRepository.SearchClienteWithCredits(search);
            var json = SerializeObject(clientes);
            return Ok(json);
        }

        private string SerializeObject(object obj)
        {
            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                WriteIndented = true,
            };

            return JsonSerializer.Serialize(obj, options);
        }

    }
}
