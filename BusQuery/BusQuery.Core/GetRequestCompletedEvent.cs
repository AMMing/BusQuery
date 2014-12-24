using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusQuery.Core
{
    public delegate void GetRequestCompletedEvent(byte[] bytes);
    public delegate void RequestFaildEvent();
}
