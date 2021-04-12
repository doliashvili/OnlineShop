using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShop.Domain.Enums;

namespace OnlineShop.Domain.Products.Records
{
    public record Weight(WeightType WeightType, float Value);
}
