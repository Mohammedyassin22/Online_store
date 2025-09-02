using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class OrderCreateBadRequstException(string message) : BadRequestException($"Invalid opertion when create or update Order!")
    {
    }
}
