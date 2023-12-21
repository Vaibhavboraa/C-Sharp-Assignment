using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Exception
{
    internal class IncompleteOrderException:System.Exception
    {
        public IncompleteOrderException() : base()
        {
        }

        public IncompleteOrderException(string message) : base(message)
        {
        }

        public IncompleteOrderException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}
