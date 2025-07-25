﻿using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;

namespace Site_Lanchonete.Models
{
    public class PedidoDetalhe
    {
        public int PedidoDetalheId { get; set; }
        public int PedidoId { get; set; }
        public int LancheID { get; set; }
        public int Quantidade { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Preco { get; set; }

        public virtual Lanche Lanche { get; set; }
        public virtual Pedido Pedido { get; set; }
    }
}
