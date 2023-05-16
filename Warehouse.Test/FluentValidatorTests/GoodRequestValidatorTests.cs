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
    public class GoodRequestValidatorTests
    {
        [Fact]
        public void GoodRequestValidator_WrongAmount_ShouldReturnError()
        {
            var validator = new GoodRequestValidator();
            var model = new GoodsRequest { Amount = -1 };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Amount);
        }
    }
}
