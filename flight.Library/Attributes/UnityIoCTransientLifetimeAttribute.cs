using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace flight.library.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct)]
    public class UnityIoCTransientLifetimedAttribute : System.Attribute
    {
        public double version;

        public UnityIoCTransientLifetimedAttribute()
        {
            version = 1.0;
        }
    }
}
