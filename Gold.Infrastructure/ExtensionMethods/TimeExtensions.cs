using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gold.Infrastructure.ExtensionMethods
{
    
        public static class TimeExtensions
        {
            public static TimeSpan StripMilliseconds(this TimeSpan time)
            {
                return new TimeSpan(time.Days, time.Hours, time.Minutes, time.Seconds);
            }
        }
    
}
