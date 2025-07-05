using API.Application.Queries;
using API.DTOs;
using API.Repositories;
using AutoMapper;
using MediatR;

namespace API.Application.Handlers
{
    public class GetAllContatosHandler : IRequestHandler<GetAllContatosQuery, IEnumerable<ContatoReadDTO>>
    {
        private readonly IContatoRepository _repository;
        private readonly IMapper _mapper;

        public GetAllContatosHandler(IContatoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ContatoReadDTO>> Handle(GetAllContatosQuery request, CancellationToken cancellationToken)
        {
            var contatos = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ContatoReadDTO>>(contatos);
        }
    }
}
