using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuicerPluginbuilder
{
    /// <summary>
    /// Перечисление, содержащее
    /// список параметров 
    /// </summary>
    public enum ParameterType
    {
        /// <summary>
        /// Диаметр тарелки
        /// </summary>
        PlateDiameter,

        /// <summary>
        /// Диаметр кола
        /// </summary>
        StakeDiameter,

        /// <summary>
        /// Высота кола
        /// </summary>
        StakeHeight,

        /// <summary>
        /// Количество отверстий в тарелке
        /// </summary>
        NumberOfHoles,

        /// <summary>
        /// Количество зубцов
        /// </summary>
        NumberOfTeeth
    }
}
