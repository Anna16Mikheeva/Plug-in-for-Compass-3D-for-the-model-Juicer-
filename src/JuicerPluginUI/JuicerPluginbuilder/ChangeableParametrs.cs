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
        /// Параметр количества зубцов кола
        /// </summary>
        private double _numberOfTeeth;

        /// <summary>
        /// Параметр количества отверстий в тарелке
        /// </summary>
        private double _numberOfHoles;

        /// <summary>
        /// Словарь перечисления параметров и ошибки
        /// </summary>
        // TODO: RSDN
        public Dictionary<ParameterType, string> Parameters = 
            new Dictionary<ParameterType, string>();

        /// <summary>
        /// Экземпляр класса ParameterCheck
        /// </summary>
        // TODO: модификатор доступа
        private ParameterCheck _parameterCheck = new ParameterCheck();

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
                const double min = 166;
                const double max = 226;
                _parameterCheck.RangeCheck
                    (value, min, max, 
                    ParameterType.PlateDiameter, Parameters);
                if(value - StakeDiameter < 96)
                {
                    throw new Exception();
                    Parameters.Add(ParameterType.PlateDiameter,
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
                const double min = 70;
                const double max = 130;
                _parameterCheck.RangeCheck
                    (value, min, max, 
                    ParameterType.StakeDiameter, Parameters);
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
                const double min = 60;
                const double max = 120;
                _parameterCheck.RangeCheck
                    (value, min, max, 
                    ParameterType.StakeHeight, Parameters);
                if(StakeDiameter - value < 10)
                {
                    throw new Exception();
                    Parameters.Add(ParameterType.StakeHeight,
                        "Выход за диапазон");
                }
                _stakeHeight = value;
            }
        }

        /// <summary>
        /// Возвращает и устанавливает значение количества зубцов кола
        /// </summary>
        public double NumberOfTeeth
        {
            get
            {
                return _numberOfTeeth;
            }

            set
            {
                const double min = 10;
                const double max = 12;
                _parameterCheck.RangeCheck
                    (value, min, max,
                    ParameterType.NumberOfTeeth, Parameters);
                _numberOfTeeth = value;
            }
        }

        /// <summary>
        /// Возвращает и устанавливает значение количества отвертий в тарелке
        /// </summary>
        public double NumberOfHoles
        {
            get
            {
                return _numberOfHoles;
            }

            set
            {
                const double min = 90;
                const double max = 100;
                _parameterCheck.RangeCheck
                    (value, min, max, 
                    ParameterType.NumberOfHoles, Parameters);
                _numberOfHoles = value;
            }
        }
    }
}
