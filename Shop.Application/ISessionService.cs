using Microsoft.AspNetCore.Http;

namespace Shop.Application
{
    public interface ISessionService
    {
        ISession GetSession();
    }
}
