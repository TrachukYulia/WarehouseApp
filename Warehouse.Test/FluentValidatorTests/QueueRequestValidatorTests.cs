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
    public class QueueRequestValidatorTests
    {
        [Fact]
        public void QueueRequestValidator_WrongOrderId_ShouldReturnError()
        {
            var validator = new QueueRequestValidator();
            var model = new QueueRequest { OrderId =  -1 };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.OrderId);
        }
    }
}
