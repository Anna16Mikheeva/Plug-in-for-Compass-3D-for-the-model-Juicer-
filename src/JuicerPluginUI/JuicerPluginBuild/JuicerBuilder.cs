using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// TODO: добавить dll
using KompasAPI7;
using Kompas6API5;
using Kompas6Constants;
using Kompas6Constants3D;
using System.Runtime.InteropServices;


namespace JuicerPluginBuild
{
    /// <summary>
    /// Класс для открытия Компас-3D, создания файла и построения модели
    /// </summary>
    public class JuicerBuilder
    {
        /// <summary>
        /// Построение модели соковыжималки
        /// </summary>
        public void BuildJuicer(double diameterPlate, double diameterStake,
            double stakeHeight, double countTeeth, double countHoles)
        {
            KompasWrapper kompasWrapper = new KompasWrapper();
            kompasWrapper.StartKompas();
            kompasWrapper.CreateFile();
            kompasWrapper.PlateSketch(diameterPlate);
            kompasWrapper.BuildStake(diameterStake, stakeHeight);
            kompasWrapper.BuildStakeTeeth
                (countTeeth, diameterStake, stakeHeight);
            kompasWrapper.BuildHolesInThePlate
                (countHoles, diameterPlate, diameterStake);
        }
    }
}
