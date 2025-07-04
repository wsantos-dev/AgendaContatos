using API.DTOs;
using API.Models;
using API.Repositories;
using AutoMapper;

namespace API.Services
{
    public class ContatoService : IContatoService
    {
        private readonly IContatoRepository _repo;
        private readonly IMapper _mapper;

        public ContatoService(IContatoRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ContatoReadDTO>> GetAllAsync()
        {
            var contatos = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<ContatoReadDTO>>(contatos);
        }

        public async Task<ContatoReadDTO?> GetByIdAsync(Guid id)
        {
            var contato = await _repo.GetByIdAsync(id);
            return contato is null ? null : _mapper.Map<ContatoReadDTO>(contato);
        }

        public async Task<ContatoReadDTO> CreateAsync(ContatoCreateDTO dto)
        {
            if (await _repo.GetByEmailAsync(dto.Email) != null)
                throw new Exception("Contato com este e-mail já existe.");

            var contato = _mapper.Map<Contato>(dto);
            contato.Id = Guid.NewGuid();
            await _repo.AddAsync(contato);
            return _mapper.Map<ContatoReadDTO>(contato);
        }
        public async Task UpdateAsync(Guid id, ContatoCreateDTO dto)
        {
            var contato = await _repo.GetByIdAsync(id);
            if (contato == null)
                throw new Exception("Contato não encontrado.");

            contato.Nome = dto.Nome;
            contato.Email = dto.Email;
            contato.Telefone = dto.Telefone;

            await _repo.UpdateAsync(contato);
        }

        public async Task DeleteAsync(Guid id)
        {
            var contato = await _repo.GetByIdAsync(id);
            if (contato == null)
                throw new Exception("Contato não encontrado.");

            await _repo.DeleteAsync(contato);
        }


    }
}
