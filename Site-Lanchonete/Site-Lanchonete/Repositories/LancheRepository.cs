using Site_Lanchonete.Context;
using Site_Lanchonete.Models;
using Site_Lanchonete.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Site_Lanchonete.Repositories
{
    public class LancheRepository : ILancheRepository
    {
        private readonly AppDbContext _context;

        public LancheRepository(AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Lanche> Lanches => _context.Lanches.Include(c => c.Categoria);
        public IEnumerable<Lanche> LanchesPreferidos => _context.Lanches.Where(p => p.IsLanchePreferido).Include(c => c.Categoria);
        public Lanche GetLancheById(int lancheId)
        {
            return _context.Lanches.FirstOrDefault(l => l.LancheId == lancheId);
        }
    }
}
