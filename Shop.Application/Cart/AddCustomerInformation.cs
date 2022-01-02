using Microsoft.AspNetCore.Http;
using Shop.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;

namespace Shop.Application.Cart
{
    public class AddCustomerInformation
    {

        private ISession Session { get; }
        public AddCustomerInformation(ISession session)
        {
            Session = session;
        }

        public void Do(Request request)
        {
            var customerInformation = new CustomerInformation()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                Address2 = request.Address2,
                City = request.City,
                PostCode = request.PostCode
            };

            var customerAsText = JsonSerializer.Serialize(customerInformation);

            Session.Set("customer-information", Encoding.UTF8.GetBytes(customerAsText));
        }

        public class Request
        {
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }
            [Required]
            [DataType(DataType.EmailAddress)]
            public string Email { get; set; }
            [Required]
            [DataType(DataType.PhoneNumber)]
            public string PhoneNumber { get; set; }

            [Required]
            public string Address { get; set; }
            public string Address2 { get; set; }
            [Required]
            public string City { get; set; }
            [Required]
            public string PostCode { get; set; }
        }
    }
}
