using Soup.Ordersystem.Objects.Order;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soup.Ordersystem.Objects
{
    public class StockAction
    {
        [Key]
        [Column("Stock_ActionsId")]
        public int Id { get; set; }
        [ForeignKey(nameof(Products))]
        public int ProductId { get; set; }
        [Column("StockAction")]
        [Required]
        public StockActionEnum StockActionsEnum { get; set; }
        [Required]
        public int Amount { get; set; }
        public virtual Product Products { get; set; }
    }
    public enum StockActionEnum
    {
        Add = 1,
        Remove = 2,
        Reserve = 3,
    }
}
