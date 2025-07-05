using MediatR;

namespace API.Application.Commands
{
    public class CreateContatoCommand : IRequest<Guid>
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
    }
}
