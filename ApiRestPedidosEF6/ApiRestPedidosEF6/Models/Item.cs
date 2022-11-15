#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ApiRestPedidosEF6.Models
{
    public partial class Item
    {
        public Item()
        {
            ItemPedido = new HashSet<ItemPedido>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        [Unicode(false)]
        public string Nome { get; set; }
        [Column(TypeName = "numeric(18, 2)")]
        public decimal VrUnitario { get; set; }
        [Column(TypeName = "numeric(10, 2)")]
        public decimal Quantidade { get; set; }

        [InverseProperty("IdItemNavigation")]
        public virtual ICollection<ItemPedido> ItemPedido { get; set; }
    }
}