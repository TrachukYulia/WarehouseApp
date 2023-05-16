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
    public class GoodRequestToCreateValidatorTests
    {
        [Fact]
        public void GoodRequestToCreateValidator_WrongAmount_ShouldReturnError()
        {
            var validator = new GoodRequestToCreateValidator();
            var model = new GoodsRequestToCreate { Amount = -1 };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Amount);
        }
        [Fact]
        public void GoodRequestToCreateValidator_WrongPrice_ShouldReturnError()
        {
            var validator = new GoodRequestToCreateValidator();
            var model = new GoodsRequestToCreate { Price = -1 };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Price);
        }
        [Fact]
        public void GoodRequestToCreateValidator_WrongTypeOfId_ShouldReturnError()
        {
            var validator = new GoodRequestToCreateValidator();
            var model = new GoodsRequestToCreate { TypeOfGoodId = -1 };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.TypeOfGoodId);
        }
        [Fact]
        public void GoodRequestToCreateValidator_EmptyName_ShouldReturnError()
        {
            var validator = new GoodRequestToCreateValidator();
            var model = new GoodsRequestToCreate { Name = null };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

    }
}
