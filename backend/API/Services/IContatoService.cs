using API.DTOs;

namespace API.Services
{
    public interface IContatoService
    {
        Task<IEnumerable<ContatoReadDTO>> GetAllAsync();
        Task<ContatoReadDTO?> GetByIdAsync(Guid id);
        Task<ContatoReadDTO> CreateAsync(ContatoCreateDTO dto);
        Task UpdateAsync(Guid id, ContatoCreateDTO dto);
        Task DeleteAsync(Guid id);
    }
}
