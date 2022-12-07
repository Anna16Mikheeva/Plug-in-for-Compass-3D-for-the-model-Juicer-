using System;
using JuicerPluginBuild;
using JuicerPluginParameters;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualBasic.Devices;

namespace StressTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            var juicerbuilder = new JuicerBuilder();
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var changeableParameters = new ChangeableParametrs();
            changeableParameters.PlateDiameter = 166;
            changeableParameters.StakeDiameter = 70;
            changeableParameters.StakeHeight = 60;
            changeableParameters.NumberOfTeeth = 10;
            changeableParameters.NumberOfHoles = 90;
            changeableParameters.LengthOfHoles = 16;
            var streamWriter = new StreamWriter($"StressTest.txt", true);
            var modelCounter = 0;
            var computerInfo = new ComputerInfo();
            var count = 0;
            while (true)
            {
                juicerbuilder.BuildJuicer(changeableParameters.PlateDiameter,
                    changeableParameters.StakeDiameter,
                    changeableParameters.StakeHeight,
                    changeableParameters.NumberOfTeeth,
                    changeableParameters.NumberOfHoles,
                    changeableParameters.LengthOfHoles);
                var usedMemory = (computerInfo.TotalPhysicalMemory - computerInfo.AvailablePhysicalMemory);
                streamWriter.WriteLine(
                    $"{++modelCounter}\t{stopWatch.Elapsed:hh\\:mm\\:ss}\t{usedMemory}");
                streamWriter.Flush();
                Console.Write($"Модель построена {count++}\n");
            }
            stopWatch.Stop();
            streamWriter.WriteLine("END");
            streamWriter.Close();
            streamWriter.Dispose();
        }
	}
}
