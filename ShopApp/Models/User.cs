using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ShopApp.Models
{
    public class User
    {
        [Key] 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; } //Уникальный идентификатор пользователя

        [Required]
        [MaxLength(50)]
        public string Login { get; set; } //Логин пользователя (уникальный)

        [Required]
        [MaxLength(255)]
        public string Password { get; set; } //Пароль

        [Required]
        [MaxLength(100)]
        public string Email { get; set; } //Электронная почта пользователя (уникальная)

        [MaxLength(100)]
        public string FullName { get; set; } //Полное имя пользователя

        public string Address { get; set; } //Адрес доставки

        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; } //Номер телефона пользователя

        public DateTime CreatedAtUser { get; set; } = DateTime.UtcNow; //Дата и время регистрации пользователя

        public ICollection<Order> Orders { get; set; }
        public ICollection<Cart> Carts { get; set; }
    }
}
