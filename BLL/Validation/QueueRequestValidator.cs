using BLL.DTO;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Validation
{
    public class QueueRequestValidator
    : AbstractValidator<QueueRequest>
    {
        public QueueRequestValidator()
        {
            RuleFor(x => x.OrderId).GreaterThanOrEqualTo(0);
        }
    }
}
