using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace JuicerPluginbuilder
{
    public class ChangeableParametrs
    {

        /// <summary>
        /// Параметр диаметра тарелки
        /// </summary>
        private double _plateDiameter;

        /// <summary>
        /// Параметр диаметра кола
        /// </summary>
        private double _stakeDiameter;

        /// <summary>
        /// Параметр высоты тарелки
        /// </summary>
        private double _stakeHeight;

        /// <summary>
        /// Параметр количества отверстий в тарелке
        /// </summary>
        private int _numberOfHoles;

        /// <summary>
        /// Параметр количества зубцов кола
        /// </summary>
        private int _numberOfTeeth;

        /// <summary>
        /// Возвращает или задает параметр
        /// </summary>
        public Dictionary<ParametrType, string> parameters = new Dictionary<ParametrType, string>();

        ParameterCheck _parameterCheck = new ParameterCheck();

        /// <summary>
        /// Возвращает и устанавливает значение диаметра тарелки
        /// </summary>
        public double PlateDiameter
        {
            get
            {
                return _plateDiameter;
            }

            set
            {
                _parameterCheck.RangeCheck(value, 156, 226, ParametrType.PlateDiameter, parameters);
                if(value - StakeDiameter < 96)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _plateDiameter = value;
            }
        }

        /// <summary>
        /// Возвращает и устанавливает значение диаметра кола
        /// </summary>
        public double StakeDiameter
        {
            get
            {
                return _stakeDiameter;
            }

            set
            {
                _parameterCheck.RangeCheck(value, 60, 130, ParametrType.StakeDiameter, parameters);
                _stakeDiameter = value;
            }
        }

        /// <summary>
        /// Возвращает и устанавливает значение высоты кола
        /// </summary>
        public double StakeHeight
        {
            get
            {
                return _stakeHeight;
            }   




            set
            {
                _parameterCheck.RangeCheck(value, 60, 120, ParametrType.StakeHeight, parameters);
                if(StakeDiameter - value < 10)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _stakeHeight = value;
            }
        }

        /// <summary>
        /// Возвращает и устанавливает значение количества отвертий в тарелке
        /// </summary>
        public int NumberOfHoles
        {
            get
            {
                return _numberOfHoles;
            }

            set
            {
                _parameterCheck.RangeCheck(value, 90, 310, ParametrType.NumberOfHoles, parameters);
                _numberOfHoles = value;
            }
        }

        /// <summary>
        /// Возвращает и устанавливает значение количества зубцов кола
        /// </summary>
        public int NumberOfTeeth
        {
            get
            {
                return _numberOfTeeth;
            }

            set
            {
                _parameterCheck.RangeCheck(value, 10, 318, ParametrType.NumberOfTeeth, parameters);
                _numberOfTeeth = value;
            }
        }
    }
}
