﻿using BLL.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validation
{
    public class GoodRequestValidator : AbstractValidator<GoodsRequest>
    {
        public GoodRequestValidator()
        {
            RuleFor(x => x.Amount).GreaterThanOrEqualTo(0);
        }
    }
}
