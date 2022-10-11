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
    public class JuicerBuild
    {
        /// <summary>
        /// Построение модели соковыжималки
        /// </summary>
        public void BuildJuicer(KompasObject kompas)
        {
            KompasWrapper kompasWrapper = new KompasWrapper();
            kompasWrapper.PlateSketch(kompas);
            kompasWrapper.StakeBuilding(kompas);
            kompasWrapper.StakeProngs(kompas);
            kompasWrapper.HolesInThePlate(kompas);
        }
    }
}
