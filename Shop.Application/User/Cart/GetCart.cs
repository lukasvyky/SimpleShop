using Shop.Domain.Infrastructure;

namespace Shop.Application.User.Cart
{
    public class GetCart
    {
        private ISessionService SessionService { get; }

        public GetCart(ISessionService sessionService)
        {
            SessionService = sessionService;
        }

        public IEnumerable<Response> Do()
        {
            return SessionService.GetCart(ci => new Response
            {
                Name = ci.ProductName,
                Value = ci.Value.ConvertPriceToText(),
                RealValue = ci.Value,
                StockId = ci.StockId,
                Qty = ci.Qty
            });
        }

        public class Response
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public decimal RealValue { get; set; }
            public int StockId { get; set; }
            public int Qty { get; set; }
        }
    }
}
