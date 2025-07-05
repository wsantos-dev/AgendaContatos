using API.Application.Commands;
using API.Repositories;
using MediatR;

namespace API.Application.Handlers
{
    public class DeleteContatoHandler : IRequestHandler<DeleteContatoCommand, bool>
    {
        private readonly IContatoRepository _repository;

        public DeleteContatoHandler(IContatoRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteContatoCommand request, CancellationToken cancellationToken)
        {
            var contato = await _repository.GetByIdAsync(request.Id);
            if (contato == null)
                return false;

            await _repository.DeleteAsync(contato);
            return true;
        }
    }
}
