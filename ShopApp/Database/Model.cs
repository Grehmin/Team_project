using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopApp.Database;

public class Cart {
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

public class Order {
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

public class OrderItem {
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


public class Product {
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

public class User {
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
    public string? FullName { get; set; } //Полное имя пользователя

    public string? Address { get; set; } //Адрес доставки

    [Required]
    [MaxLength(20)]
    public string PhoneNumber { get; set; } //Номер телефона пользователя

    public DateTime CreatedAtUser { get; set; } = DateTime.UtcNow; //Дата и время регистрации пользователя

    public ICollection<Order> Orders { get; set; }
    public ICollection<Cart> Carts { get; set; }
}
