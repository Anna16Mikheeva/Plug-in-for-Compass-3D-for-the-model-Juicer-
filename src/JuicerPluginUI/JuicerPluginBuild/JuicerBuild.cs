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
        public void BuildJuicer(KompasWrapper kompasWrapper, double diameterPlate, double diameterStake, double stakeHeight, int countHoles, int countTeeth)
        {
            kompasWrapper.PlateSketch(diameterPlate);
            kompasWrapper.StakeBuilding(diameterStake);
            kompasWrapper.StakeProngs(countTeeth);
            kompasWrapper.HolesInThePlate(countHoles, diameterPlate);
        }
    }
}
