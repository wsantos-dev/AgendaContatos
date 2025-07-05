using API.Application.Queries;
using API.DTOs;
using API.Repositories;
using AutoMapper;
using MediatR;

namespace API.Application.Handlers
{
    public class GetContatoByIdHandler : IRequestHandler<GetContatoByIdQuery, ContatoReadDTO>
    {
        private IContatoRepository _repository;
        private IMapper _mapper;

        public GetContatoByIdHandler(IContatoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<ContatoReadDTO> Handle(GetContatoByIdQuery request, CancellationToken cancellationToken)
        {
            var contato = await _repository.GetByIdAsync(request.Id);
            if (contato == null)
                return null!;

            return _mapper.Map<ContatoReadDTO>(contato);
        }
    }
}
