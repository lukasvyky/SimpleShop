using Shop.Application;

namespace Shop.UI
{
    public class HttpContextGetter : ISessionService
    {
        public IHttpContextAccessor HttpContextAccessor { get; set; }
        public HttpContextGetter(IHttpContextAccessor contextAccessor)
        {
            HttpContextAccessor = contextAccessor;
        }
        public ISession GetSession()
        {
            return HttpContextAccessor.HttpContext.Session;
        }
    }
}
