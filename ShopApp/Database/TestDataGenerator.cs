using Microsoft.EntityFrameworkCore;

namespace ShopApp.Database;

public static class TestDataGenerator {
    public static void Seed(ModelBuilder modelBuilder) {

        // Seed Users
        modelBuilder.Entity<User>().HasData(
            new User {
                UserID = 1,
                Login = "admin",
                Password = "admin",
                Email = "admin@admin.admin",
                PhoneNumber = "1234567890",
                FullName = "Ad Ministrator",
                Address = "123 Main St",
                IsAdmin = true,
            },
            new User {
                UserID = 2,
                Login = "test",
                Password = "test",
                Email = "testuser@example.com",
                PhoneNumber = "0987654321",
                FullName = "Terry Stificate",
                Address = "456 Elm St",
            }
        );

        // Seed Products
        modelBuilder.Entity<Product>().HasData(
            new Product {
                ProductID = 1,
                ProductName = "Bread",
                ProductDescription = "Succulent bread",
                Price = 10.00m,
                Quantity = 100,
            },
            new Product {
                ProductID = 2,
                ProductName = "Croissants",
                ProductDescription = "French feed",
                Price = 20.50m,
                Quantity = 50,
            },
            new Product {
                ProductID = 3,
                ProductName = "Meat Bun",
                ProductDescription = "Yum",
                Price = 5.99m,
                Quantity = 200,
            },
            new Product {
                ProductID = 4,
                ProductName = "Sake",
                ProductDescription = "Drink of a true samurai",
                Price = 50.00m,
                Quantity = 100,
            },
            new Product {
                ProductID = 5,
                ProductName = "Gum Tea",
                ProductDescription = "Liquid Addiction",
                Price = 5.50m,
                Quantity = 50,
            },
            new Product {
                ProductID = 6,
                ProductName = "Banana",
                ProductDescription = "Banana",
                Price = 0.99m,
                Quantity = 200,
            }
        );

        // Seed Carts
        modelBuilder.Entity<Cart>().HasData(
            new Cart {
                CartID = 1,
                UserId = 1,
                ProductId = 1,
                Quantity = 2,
                AddedAt = new DateTime(2023, 1, 2)
            },
            new Cart {
                CartID = 2,
                UserId = 1,
                ProductId = 2,
                Quantity = 1,
                AddedAt = new DateTime(2023, 1, 3)
            },
            new Cart {
                CartID = 3,
                UserId = 2,
                ProductId = 3,
                Quantity = 5,
                AddedAt = new DateTime(2023, 1, 4)
            }
        );

        // Seed Orders
        modelBuilder.Entity<Order>().HasData(
            new Order {
                OrderID = 1,
                UserId = 1,
                TotalAmount = 40.50m,
                StatusOrder = "Completed",
                CreatedAtOrder = new DateTime(2023, 1, 5)
            },
            new Order {
                OrderID = 2,
                UserId = 2,
                TotalAmount = 17.97m,
                StatusOrder = "Pending",
                CreatedAtOrder = new DateTime(2023, 1, 6)
            }
        );

        // Seed OrderItems
        modelBuilder.Entity<OrderItem>().HasData(
            new OrderItem {
                OrderItemsID = 1,
                OrderId = 1,
                ProductId = 1,
                Quantity = 2,
                Price = 10.00m
            },
            new OrderItem {
                OrderItemsID = 2,
                OrderId = 1,
                ProductId = 2,
                Quantity = 1,
                Price = 20.50m
            },
            new OrderItem {
                OrderItemsID = 3,
                OrderId = 2,
                ProductId = 3,
                Quantity = 3,
                Price = 5.99m
            }
        );

    }

}
