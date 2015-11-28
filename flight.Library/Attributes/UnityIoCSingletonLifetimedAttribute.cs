using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flight.library.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct)]
    public class UnityIoCSingletonLifetimedAttribute : System.Attribute
    {
        public double version;

        public UnityIoCSingletonLifetimedAttribute()
        {
            version = 1.0;
        }
    }
}
