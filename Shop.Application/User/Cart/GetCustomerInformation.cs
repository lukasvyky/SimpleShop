using Shop.Application.Infrastructure;

namespace Shop.Application.User.Cart
{
    public class GetCustomerInformation
    {
        private ISessionService SessionService { get; }
        public GetCustomerInformation(ISessionService sessionService)
        {
            SessionService = sessionService;
        }

        public Response Do()
        {
            var customerInformation = SessionService.GetCustomerInformation();

            return customerInformation is null ? null : new Response
            {
                FirstName = customerInformation.FirstName,
                LastName = customerInformation.LastName,
                Email = customerInformation.Email,
                PhoneNumber = customerInformation.PhoneNumber,
                Address = customerInformation.Address,
                Address2 = customerInformation.Address2,
                City = customerInformation.City,
                PostCode = customerInformation.PostCode
            };
        }

        public class Response
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }


            public string Address { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string PostCode { get; set; }
        }
    }
}
