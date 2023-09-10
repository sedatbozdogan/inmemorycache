using CachingExample.Models;

namespace CachingExample.Repositories
{
    public interface IUserService
    {
        Task<User> GetUser();
    }
}
