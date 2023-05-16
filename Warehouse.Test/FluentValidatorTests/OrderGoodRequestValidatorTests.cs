using BLL.DTO;
using BLL.Validation;
using FluentValidation.TestHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Test.FluentValidatorTests
{
    public class OrderGoodRequestValidatorTests
    {
        [Fact]
        public void OrderGoodRequestValidator_WrongAmount_ShouldReturnError()
        {
            var validator = new OrderGoodRequestValidator();
            var model = new OrderGoodRequest { Amount = -1 };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Amount);
        }
    }
}
