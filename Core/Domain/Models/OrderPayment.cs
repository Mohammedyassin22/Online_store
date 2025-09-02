using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public enum OrderPayment
    {
        pending=0,
        paymentReceived=1,
        paymentFailed=2
    }
}
