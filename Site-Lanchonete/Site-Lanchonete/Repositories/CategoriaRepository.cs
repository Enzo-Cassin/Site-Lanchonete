using Site_Lanchonete.Context;
using Site_Lanchonete.Models;
using Site_Lanchonete.Repositories.Interfaces;

namespace Site_Lanchonete.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> Categorias => _context.Categorias;
    }
}
