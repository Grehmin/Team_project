using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.Models
{   
    public class Order
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderID { get; set; } //Уникальный идентификатор заказа

        [Required]
        [ForeignKey("User")] 
        public int UserId { get; set; } //Идентификатор пользователя, сделавшего заказ

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal TotalAmount { get; set; } //Общая сумма заказа

        [Required]
        [MaxLength(50)]
        public string StatusOrder { get; set; } //Статус заказа

        public DateTime CreatedAtOrder { get; set; } = DateTime.UtcNow; // Дата и время создания заказа 

        public User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
