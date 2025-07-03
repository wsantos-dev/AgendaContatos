using API.Models;

namespace API.Repositories
{
    public interface IContatoRepository
    {
        Task<IEnumerable<Contato>> GetAllAsync();
        Task<Contato?> GetByIdAsync(Guid id);
        Task<Contato?> GetByEmailAsync(string email);
        Task AddAsync(Contato contato);
        Task UpdateAsync(Contato contato);
        Task DeleteAsync(Contato contato);
    }
}
