using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuicerPluginbuilder
{
    class ParameterCheck
    {
        public void RangeCheck(double value, double min, double max)
        {
            if(value < min || value > max)
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
