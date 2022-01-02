using Microsoft.AspNetCore.Http;
using Shop.Domain.Models;
using System.Text;
using System.Text.Json;

namespace Shop.Application.Cart
{
    public class GetCustomerInformation
    {
        private ISession Session { get; }
        public GetCustomerInformation(ISession session)
        {
            Session = session;
        }

        public Response Do()
        {
            var hasCookieValue = Session.TryGetValue("customer-information", out byte[] value);
            var customerIntel = hasCookieValue ? JsonSerializer.Deserialize<CustomerInformation>(Encoding.ASCII.GetString(value)) : null;

            if (customerIntel is null)
            {
                return null;
            }

            return new Response
            {
                FirstName = customerIntel.FirstName,
                LastName = customerIntel.LastName,
                Email = customerIntel.Email,
                PhoneNumber = customerIntel.PhoneNumber,
                Address = customerIntel.Address,
                Address2 = customerIntel.Address2,
                City = customerIntel.City,
                PostCode = customerIntel.PostCode
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
