using Site_Lanchonete.Models;

namespace Site_Lanchonete.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> Categorias { get; }
    }
}
