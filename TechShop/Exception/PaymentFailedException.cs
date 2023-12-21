using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Exception
{
    internal class PaymentFailedException:System.Exception
    {
        public PaymentFailedException() : base()
        {
        }

        public PaymentFailedException(string message) : base(message)
        {
        }

        public PaymentFailedException (string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}
