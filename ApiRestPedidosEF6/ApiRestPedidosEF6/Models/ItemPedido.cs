#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ApiRestPedidosEF6.Models
{
    public partial class ItemPedido
    {
        [Key]
        public int Id { get; set; }
        public int IdItem { get; set; }
        public int IdPedido { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal VrUnitario { get; set; }
        [Column(TypeName = "numeric(10, 2)")]
        public decimal Quantidade { get; set; }

        [ForeignKey("IdItem")]
        [InverseProperty("ItemPedido")]
        public virtual Item IdItemNavigation { get; set; }
        [ForeignKey("IdPedido")]
        [InverseProperty("ItemPedido")]
        public virtual Pedido IdPedidoNavigation { get; set; }
    }
}