using MediatR;

namespace API.Application.Commands
{
    public class UpdateContatoCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
    }
}
