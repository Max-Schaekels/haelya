﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haelya.Application.Exceptions
{
    public class NotFoundException :Exception
    {
        public NotFoundException(string message) : base(message) { }
    }
}
