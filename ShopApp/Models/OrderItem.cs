using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Models
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderItemsID { get; set; } //Уникальный идентификатор элемента заказа

        [Required]
        [ForeignKey("Order")]
        public int OrderId { get; set; } //Идентификатор заказа

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; } //Идентификатор продукта

        [Required]
        public int Quantity { get; set; } //Количество товара в заказе

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; } //Цена товара на момент заказа

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
