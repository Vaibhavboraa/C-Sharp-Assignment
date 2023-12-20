﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechShop.Exception
{
    internal class InvalidDataException: System.Exception
    {
        public InvalidDataException() : base()
        {
        }

        public InvalidDataException(string message) : base(message)
        {
        }

        public InvalidDataException(string message, System.Exception innerException) : base(message, innerException)
        {
        }
    }
}
