using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuicerPluginbuilder
{
    /// <summary>
    /// Класс для проверки диапазона
    /// </summary>
    class ParameterCheck
    {
        /// <summary>
        /// Метод для проверки на диапазон
        /// </summary>
        /// <param name="value">Значение параметра</param>
        /// <param name="min">Минимальное значение диапазона</param>
        /// <param name="max">Максимальное значение диапазона</param>
        /// <param name="parameters">Перечисление</param>
        /// <param name="errors">Словарь с параметром и ошибкой</param>
        public void RangeCheck(double value, double min, double max, 
            ParameterType parameters, Dictionary<ParameterType, string> errors)
        {
            errors.Remove(parameters);
            if (value < min || value > max)
            {
                errors.Remove(parameters);
                errors.Add(parameters, "Выход за диапазон");
                throw new ArgumentOutOfRangeException();
            }
        }
    }
}
