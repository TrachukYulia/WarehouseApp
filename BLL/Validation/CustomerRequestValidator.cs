using BLL.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validation
{
    public class CustomerRequestValidator : AbstractValidator<CustomerRequest>
    {
        public CustomerRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Surname).NotEmpty();
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Matches(@"^[0-9\-\+]{6,15}$")
                .WithMessage("Phone number must have only digits, + or -");
        }

    }
}
