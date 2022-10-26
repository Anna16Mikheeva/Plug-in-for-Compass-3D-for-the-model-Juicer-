using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace JuicerPluginbuilder
{
    /// <summary>
    /// Класс параметров
    /// </summary>
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
        /// Словарь перечисления параметров и ошибки
        /// </summary>
        public Dictionary<ParameterType, string> parameters = 
            new Dictionary<ParameterType, string>();

        /// <summary>
        /// Экземпляр класса ParameterCheck
        /// </summary>
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
                _parameterCheck.RangeCheck
                    (value, 166, 226, 
                    ParameterType.PlateDiameter, parameters);
                if(value - StakeDiameter < 96)
                {
                    throw new ArgumentOutOfRangeException();
                    parameters.Add(ParameterType.PlateDiameter, 
                        "Выход за диапазон");
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
                _parameterCheck.RangeCheck
                    (value, 70, 130, 
                    ParameterType.StakeDiameter, parameters);
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
                _parameterCheck.RangeCheck
                    (value, 60, 120, 
                    ParameterType.StakeHeight, parameters);
                if(StakeDiameter - value < 10)
                {
                    throw new ArgumentOutOfRangeException();
                    parameters.Add(ParameterType.StakeHeight, 
                        "Выход за диапазон");
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
                _parameterCheck.RangeCheck
                    (value, 90, 100, 
                    ParameterType.NumberOfHoles, parameters);
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
                _parameterCheck.RangeCheck
                    (value, 10, 12, 
                    ParameterType.NumberOfTeeth, parameters);
                _numberOfTeeth = value;
            }
        }
    }
}
