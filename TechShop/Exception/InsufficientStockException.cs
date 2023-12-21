using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Exception
{
    internal class InsufficientStockException:System.Exception
    {
        public InsufficientStockException() : base()
        {
        }

        public InsufficientStockException(string message) : base(message)
        {
        }

        public InsufficientStockException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}
