using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuicerPluginbuilder
{
    class ParameterCheck
    {
        public void RangeCheck(double value, double min, double max, ParametrType parameters, Dictionary<ParametrType, string> errors)
        {
            if(value < min || value > max)
            {
                errors.Remove(parameters);
                errors.Add(parameters, "Выход за диапазон");
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
