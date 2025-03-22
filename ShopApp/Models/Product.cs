using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; } //Уникальный идентификатор продукта

        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; } //Название продукта

        public string ProductDescription { get; set; } //Описание продукта

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; } //Цена продукта

        [Required]
        public int Quantity { get; set; } //Количество товара на складе

        [MaxLength(255)]
        public string Image { get; set; } //Изображение продукта(если есть)

        public DateTime CreatedAtProduct { get; set; } = DateTime.UtcNow; //Дата и время добавления продукта

        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<Cart> Carts { get; set; }
    }
}
