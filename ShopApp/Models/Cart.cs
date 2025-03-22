using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Models
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartID { get; set; } //Уникальный идентификатор записи в корзине

        [Required]
        [ForeignKey("User")] 
        public int UserId { get; set; } //Идентификатор пользователя

        [Required]
        [ForeignKey("Product")] 
        public int ProductId { get; set; } //Идентификатор продукта

        [Required]
        public int Quantity { get; set; } //Количество товара в корзине

        public DateTime AddedAt { get; set; } = DateTime.UtcNow; //Количество товара в корзине

        public User User { get; set; }
        public Product Product { get; set; }
    }
}
