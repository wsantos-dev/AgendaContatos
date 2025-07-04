using API.DTOs;
using API.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatosController : ControllerBase
    {
        private readonly IContatoService _service;

        public ContatosController(IContatoService service)
        {
            _service = service;
        }

        [HttpGet("ListarTodos")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("Listar/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var contato = await _service.GetByIdAsync(id);
            return contato == null ? NotFound() : Ok(contato);
        }

        [HttpPost("Salvar")]
        public async Task<IActionResult> Post([FromBody] ContatoCreateDTO dto, [FromServices] IValidator<ContatoCreateDTO> validator)
        {
            
            var validationResult = await validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                            .Select(e => e.ErrorMessage)
                            .ToList();

                return BadRequest(errors);
            }

            // Se a validação for bem-sucedida, processa a criação
            var contato = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = contato.Id }, contato);

        }

        [HttpPut("Atualizar/{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] ContatoCreateDTO dto, [FromServices] IValidator<ContatoCreateDTO> validator)
        {
            var validationResult = await validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(errors);
            }

            await _service.UpdateAsync(id, dto);
            return NoContent();
        }

        [HttpDelete("Deletar/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
