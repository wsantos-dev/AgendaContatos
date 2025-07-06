using API.Application.Commands;
using API.Application.Queries;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoCQRSController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContatoCQRSController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("ListarTodos")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var contatos = await _mediator.Send(new GetAllContatosQuery());
            return Ok(contatos);
        }

        [HttpGet("Listar/{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetContatoByIdQuery(id));
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPost("CriarContato")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateContatoCommand command, [FromServices] IValidator<CreateContatoCommand> validator)
        {

            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(errors);
            }

            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(Create), new { id }, new { id });
        }

        [HttpPut("Atualizar/{id}")]
        [Authorize]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateContatoCommand command, [FromServices] IValidator<UpdateContatoCommand> validator)
        {
            //if (id != command.Id) return BadRequest("ID da URL difere do corpo da requisição.");
            command.Id = id;

            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(errors);
            }


            var sucesso = await _mediator.Send(command);
            if (!sucesso) return NotFound();

            return NoContent();
        }

        [HttpDelete("Excluir/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteContatoCommand(id));
            if (!result) return NotFound();

            return NoContent();
        }

    }
}



