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
    public class TypeOfGoodRequestValiatorTests
    {
        [Fact]
        public void TypeOfGoodRequestValidator_EmptyName_ShouldReturnError()
        {
            var validator = new TypeOfGoodRequestValidator();
            var model = new TypeOfGoodRequest { Name = null };
            TestValidationResult<TypeOfGoodRequest> result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
    }
}
