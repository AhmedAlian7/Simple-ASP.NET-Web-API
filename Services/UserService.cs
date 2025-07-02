using Back_End.Data;
using Back_End.Models;
using Back_End.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Back_End.Services
{
    public class UserService(AppDbContext context)
    {
        public User? getUser(string username, string password)
        {
            return context.Users
                .FirstOrDefault(user => user.Name == username
                             && user.Password == password);
        }
        public bool isUserExist(string username, string password)
        {
            return (getUser(username, password) != null);
        }

        public bool HasPermission(int userId, Permissions permission)
        {
            return context.UsersPermissions
                .Any(p => p.UserId == userId
                  && p.PermissionId == (int)permission);
        }

        public async Task<int> AddUser(User user)
        {

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return user.Id;
        }

    }
}
