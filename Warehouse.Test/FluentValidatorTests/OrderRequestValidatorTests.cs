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
    public class OrderRequestValidatorTests
    {
        [Fact]
        public void OrderRequestValidator_WrongAmount_ShouldReturnError()
        {
            var validator = new OrderRequestValidator();
            var model = new OrderRequest { Amount = -1 };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Amount);
        }
    }
}
