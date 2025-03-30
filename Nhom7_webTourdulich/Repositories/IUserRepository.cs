using Nhom7_webTourdulich.Models;

namespace Nhom7_webTourdulich.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task<User> GetByUsernameAndPasswordAsync(string username, string password);
    }
}
