using MediatR;

namespace API.Application.Commands
{
    public class DeleteContatoCommand : IRequest<bool>
    {
        public Guid Id { get; }

        public DeleteContatoCommand(Guid id)
        {
            Id = id;
        }
    }
}
