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
    public class JuicerBuilder
    {
        /// <summary>
        /// Построение модели соковыжималки
        /// </summary>
        public void BuildJuicer(double diameterPlate, double diameterStake, double stakeHeight, int countHoles, int countTeeth)
        {
            KompasWrapper kompasWrapper = new KompasWrapper();
            kompasWrapper.StartKompas();
            kompasWrapper.CreateFile();
            kompasWrapper.PlateSketch(diameterPlate);
            kompasWrapper.StakeBuilding(diameterStake, stakeHeight);
            kompasWrapper.StakeProngs(countTeeth, diameterStake, stakeHeight);
            kompasWrapper.HolesInThePlate(countHoles, diameterPlate, stakeHeight);
        }
    }
}
