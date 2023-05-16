using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Models;
using DAL.Repository;
using BLL.DTO;
using BLL.Validation;
using FluentValidation.TestHelper;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Test.FluentValidatorTests
{
    public class CustomRequestValidatorTests
    {
        [Fact]
        public void CustomerRequestValidator_EmptyName_ShouldReturnError()
        {
            var validator = new CustomerRequestValidator();
            var model = new CustomerRequest { Name = null, PhoneNumber = "+380999909", Surname = "Surname" };
            TestValidationResult<CustomerRequest> result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }
        [Fact]
        public void CustomerRequestValidator_EmptySurname_ShouldReturnError()
        {
            var validator = new CustomerRequestValidator();
            var model = new CustomerRequest { Name = "Name", PhoneNumber = "+380999909", Surname = null };
            TestValidationResult<CustomerRequest> result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.Surname);
        }
        [Fact]
        public void CustomerRequestValidator_WrongPhoneNumber_ShouldReturnError()
        {
            var validator = new CustomerRequestValidator();
            var model = new CustomerRequest { Name = "Name", PhoneNumber = "+aaaaaa", Surname = "Surname" };
            TestValidationResult<CustomerRequest> result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.PhoneNumber);
        }
    }
}
