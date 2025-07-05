using API.Application.Commands;
using API.Models;
using API.Repositories;
using AutoMapper;
using MediatR;

namespace API.Application.Handlers
{
    public class CreateContatoHandler : IRequestHandler<CreateContatoCommand, Guid>
    {
        private readonly IContatoRepository _repository;
        private readonly IMapper _mapper;

        public CreateContatoHandler(IContatoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateContatoCommand request, CancellationToken cancellationToken)
        {
            var contato = new Contato { Nome = request.Nome, Email = request.Email, Telefone = request.Telefone };
            await _repository.AddAsync(contato);
            return contato.Id;
        }
    }
}
