using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusQuery.Core
{
    public interface ISendContent
    {
        byte[] serialize();
    }
}
