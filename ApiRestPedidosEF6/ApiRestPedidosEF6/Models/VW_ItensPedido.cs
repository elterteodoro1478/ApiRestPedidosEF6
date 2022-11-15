#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ApiRestPedidosEF6.Models
{
    [Keyless]
    public partial class VW_ItensPedido
    {
        [Required]
        [Column("Nº Pedido")]
        [StringLength(50)]
        [Unicode(false)]
        public string Nº_Pedido { get; set; }
        [Column("Dt.Pedido")]
        [StringLength(17)]
        [Unicode(false)]
        public string Dt_Pedido { get; set; }
        [Required]
        [StringLength(255)]
        [Unicode(false)]
        public string Item { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal VrUnitario { get; set; }
        [Column(TypeName = "numeric(10, 2)")]
        public decimal Qtde { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal? VrTotal { get; set; }
        [Required]
        [StringLength(255)]
        [Unicode(false)]
        public string Cliente { get; set; }
        public int IdPedido { get; set; }
    }
}