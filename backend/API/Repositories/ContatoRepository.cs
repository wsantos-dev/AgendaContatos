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
            return await _context.Contatos
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<Contato?> GetByIdAsync(Guid id)
        {
            return await _context.Contatos
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Contato?> GetByEmailAsync(string email)
        {
            return await _context.Contatos
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Email == email);
        }

        public async Task<Contato?> GetByPhoneAsync(string phone)
        {
            return await _context.Contatos
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Telefone == phone);
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
