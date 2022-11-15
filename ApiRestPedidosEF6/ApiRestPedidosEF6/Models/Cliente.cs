#nullable disable
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ApiRestPedidosEF6.Models
{
    public partial class Cliente
    {
        public Cliente()
        {
            Pedido = new HashSet<Pedido>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        [Unicode(false)]
        public string Nome { get; set; }
        [Required]
        [StringLength(20)]
        [Unicode(false)]
        public string Documento { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string Email { get; set; }

        [InverseProperty("IdClienteNavigation")]
        public virtual ICollection<Pedido> Pedido { get; set; }
    }
}