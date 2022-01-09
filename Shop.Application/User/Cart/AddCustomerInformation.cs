using Shop.Application.Infrastructure;
using Shop.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Shop.Application.User.Cart
{
    public class AddCustomerInformation
    {
        private ISessionService SessionService { get; }
        public AddCustomerInformation(ISessionService sessionService)
        {
            SessionService = sessionService;
        }

        public void Do(Request request)
        {
            SessionService.AddCustomerInformation(new CustomerInformation()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                Address2 = request.Address2,
                City = request.City,
                PostCode = request.PostCode
            });
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
