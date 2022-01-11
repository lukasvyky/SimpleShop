using System.Threading.Tasks;

namespace Shop.Domain.Infrastructure
{
    public interface IUserService
    {
        Task CreateManagerUser(string username, string password);
    }
}
