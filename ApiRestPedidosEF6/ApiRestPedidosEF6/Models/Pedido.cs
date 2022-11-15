#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ApiRestPedidosEF6.Models
{
    public partial class Pedido
    {
        public Pedido()
        {
            ItemPedido = new HashSet<ItemPedido>();
        }

        [Key]
        public int Id { get; set; }
        public int IdCliente { get; set; }
        [Required]
        [StringLength(50)]
        [Unicode(false)]
        public string NumeroPedido { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DtCriacao { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal VrTotal { get; set; }

        [ForeignKey("IdCliente")]
        [InverseProperty("Pedido")]
        public virtual Cliente IdClienteNavigation { get; set; }
        [InverseProperty("IdPedidoNavigation")]
        public virtual ICollection<ItemPedido> ItemPedido { get; set; }
    }
}