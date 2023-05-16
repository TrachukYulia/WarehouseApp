using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.DTO;
using FluentValidation;

using System.Threading.Tasks;

namespace BLL.Validation
{
    public class TypeOfGoodRequestValidator : AbstractValidator<TypeOfGoodRequest>
    {
        public TypeOfGoodRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
