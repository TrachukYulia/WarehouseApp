using BLL.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validation
{
    public class GoodRequestToCreateValidator : AbstractValidator<GoodsRequestToCreate>
    {
        public GoodRequestToCreateValidator()
        {

            RuleFor(x => x.Amount).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.TypeOfGoodId).GreaterThanOrEqualTo(0);
        }
    }
}
