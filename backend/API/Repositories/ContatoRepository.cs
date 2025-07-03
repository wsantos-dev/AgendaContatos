using API.Models;

namespace API.Repositories
{
    public class ContatoRepository : IContatoRepository
    {
        public Task AddAsync(Contato contato)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Contato contato)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Contato>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Contato?> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Contato?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Contato contato)
        {
            throw new NotImplementedException();
        }
    }
}
