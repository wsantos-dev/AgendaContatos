using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ContatoRepository : IContatoRepository
    {
        private readonly AppDbContext _context;

        public ContatoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Contato>> GetAllAsync()
        {
            return await _context.Contatos.ToListAsync();
        }
        public async Task<Contato?> GetByIdAsync(Guid id)
        {
            return await _context.Contatos.FindAsync(id);
        }
        public async Task<Contato?> GetByEmailAsync(string email)
        {
            return await _context.Contatos.FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task AddAsync(Contato contato)
        {
            await _context.Contatos.AddAsync(contato);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Contato contato)
        {
            _context.Contatos.Update(contato);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Contato contato)
        {
            _context.Contatos.Remove(contato);
            await _context.SaveChangesAsync();
        }
    }
}
