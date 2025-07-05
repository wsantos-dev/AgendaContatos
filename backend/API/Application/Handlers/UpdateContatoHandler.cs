using API.Application.Commands;
using API.Repositories;
using MediatR;

namespace API.Application.Handlers
{
    public class UpdateContatoHandler : IRequestHandler<UpdateContatoCommand, bool>
    {
        private readonly IContatoRepository _repository;

        public UpdateContatoHandler(IContatoRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateContatoCommand request, CancellationToken cancellationToken)
        {
            var contato = await _repository.GetByIdAsync(request.Id);
            
            if (contato == null)
                return false;

            contato.Nome = request.Nome;
            contato.Email = request.Email;
            contato.Telefone = request.Telefone;

            await _repository.UpdateAsync(contato);

            return true;


        }
    }
}
