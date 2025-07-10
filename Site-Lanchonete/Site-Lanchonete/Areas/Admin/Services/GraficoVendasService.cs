using Site_Lanchonete.Context;
using Site_Lanchonete.Models;

namespace Site_Lanchonete.Areas.Admin.Services
{
    public class GraficoVendasService
    {
        private readonly AppDbContext _context;

        public GraficoVendasService(AppDbContext context)
        {
            this._context = context;
        }

        public List<LancheGrafico> GetVendasLanches(int dias = 360)
        {
            var data = DateTime.Now.AddDays(-dias);

            var lanches = (from pd in _context.PedidoDetalhes
                           join l in _context.Lanches on pd.LancheID equals l.LancheId
                           where pd.Pedido.PedidoEnviado >= data
                           group pd by new { pd.LancheID, l.Nome }
                           into g
                           select new
                           {
                               LancheNome = g.Key.Nome,
                               LanchesQuantidade = g.Sum(q => q.Quantidade),
                               LanchesValorTotal = g.Sum(a => a.Preco * a.Quantidade)
                           });

            var lista = new List<LancheGrafico>();

            foreach (var l in lanches)
            {
                var lanche = new LancheGrafico();
                lanche.LancheNome = l.LancheNome;
                lanche.LanchesQuantidade = l.LanchesQuantidade;
                lanche.LanchesValorTotal = l.LanchesValorTotal;
                lista.Add(lanche);
            }

            return lista;
        }
    }
}
