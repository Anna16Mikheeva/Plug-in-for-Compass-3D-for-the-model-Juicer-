using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KompasAPI7;
using Kompas6API5;
using Kompas6Constants;
using Kompas6Constants3D;
using System.Runtime.InteropServices;

namespace JuicerPluginBuild
{
    /// <summary>
    /// Класс для запуска Компаса и построения модели соковыжималки
    /// </summary>
    class KompasWrapper
    {
        /// <summary>
        /// Объект Компас API
        /// </summary>
        private KompasObject _kompas = null;

        /// <summary>
        /// Запуск Компас-3D
        /// </summary>
        public void StartKompas()
        {
            try
            {
                if (_kompas != null)
                {
                    _kompas.Visible = true;
                    _kompas.ActivateControllerAPI();
                }

                if (_kompas == null)
                {
                    Type kompasType = Type.GetTypeFromProgID("KOMPAS.Application.5");
                    _kompas = (KompasObject)Activator.CreateInstance(kompasType);

                    StartKompas();

                    if (_kompas == null)
                    {
                        throw new Exception("Не удается открыть Koмпас-3D");
                    }
                }
            }
            catch (COMException)
            {
                _kompas = null;
                StartKompas();
            }
        }


        ///// <summary>
        ///// Построение модели соковыжималки
        ///// </summary>
        ///// <param name="bushing"></param>
        //public void BuildingJuicer(Bushing bushing)
        //{
        //    try
        //    {
        //        JuicerBuild detail = new JuicerBuild(_kompas);
        //        detail.CreateDetail(bushing);
        //    }
        //    catch
        //    {
        //        throw new ArgumentException("Не удается построить деталь");
        //    }
        //}
    }
}
