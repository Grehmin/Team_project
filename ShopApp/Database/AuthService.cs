using Microsoft.EntityFrameworkCore;

namespace ShopApp.Database;

public class AuthService {
    private readonly AppDbContext context;

    public AuthService(AppDbContext context) {
        this.context = context;
    }

    public async Task<(bool Success, User? User, string? Error)> RegisterAsync(
        string login,
        string password,
        string email,
        string phoneNumber,
        string? fullName = null,
        string? address = null) {
        try {
            if (await context.Users.AnyAsync(u => u.Login == login || u.Email == email)) {
                return (false, null, "User with this login or email already exists");
            }
            var newUser = new User {
                Login = login,
                Password = password,
                Email = email,
                FullName = fullName,
                Address = address,
                PhoneNumber = phoneNumber
            };
            await context.Users.AddAsync(newUser);
            await context.SaveChangesAsync();
            return (true, newUser, null);
        } catch (Exception ex) {
            throw ex;
            //return (false, null, $"Registration failed: {ex.Message}");
        }
    }

    public async Task<(bool Success, User? User, string? Error)> LoginAsync(string loginOrEmail, string password) {
        try {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Login == loginOrEmail || u.Email == loginOrEmail);
            if (user == null) {
                return (false, null, "User not found");
            }
            if (!user.Password.Equals(password)) {
                return (false, null, "Invalid password");
            }
            return (true, user, null);
        } catch (Exception ex) {
            throw ex;
            //return (false, null, $"Login failed: {ex.Message}");
        }
    }
}
