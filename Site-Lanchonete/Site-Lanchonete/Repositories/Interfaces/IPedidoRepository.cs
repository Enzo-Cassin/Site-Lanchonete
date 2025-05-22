using Site_Lanchonete.Models;

namespace Site_Lanchonete.Repositories.Interfaces
{
    public interface IPedidoRepository
    {
        void CriarPedido(Pedido pedido);
    }
}
