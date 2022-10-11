using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using KompasAPI7;
//using Kompas6API5;
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
        public List<Parametrs> Parametr { get; set; } = new List<Parametrs>();

        /// <summary>
        /// Возвращает и устанавливает значение длины диаметра тарелки
        /// </summary>
        public double PlateDiameter
        {
            get
            {
                return _plateDiameter;
            }

            set
            {
                const int minLength = 156;
                const int maxLength = 226;
                _plateDiameter = value;
            }
        }

        /// <summary>
        /// Возвращает и устанавливает значение длины диаметра кола
        /// </summary>
        public double StakeDiameter
        {
            get
            {
                return _stakeDiameter;
            }

            set
            {
                const int minLength = 60;
                const int maxLength = 130;
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
                const int minLength = 60;
                const int maxLength = 120;
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
                const int minLength = 90;
                const int maxLength = 310;
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
                const int minLength = 10;
                const int maxLength = 18;
                _numberOfTeeth = value;
            }
        }
    }
}
