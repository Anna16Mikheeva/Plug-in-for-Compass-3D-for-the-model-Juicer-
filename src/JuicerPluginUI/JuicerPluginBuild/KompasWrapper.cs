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
    public class KompasWrapper
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
                    Type kompasType = Type.GetTypeFromProgID
                        ("KOMPAS.Application.5");
                    _kompas = (KompasObject)Activator.CreateInstance
                        (kompasType);

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


        /// <summary>
        /// Построение модели соковыжималки
        /// </summary>
        public void BuildingJuicer()
        {
            try
            {
                var document = (ksDocument3D)_kompas.Document3D();
                document.Create();
            }
            catch
            {
                throw new ArgumentException("Не удается построить деталь");
            }
        }

        /// <summary>
        /// Построение эскиза тарелки соковыжималки
        /// </summary>
        public void PlateSketch(double diameterPlate)
        {
            bool thinWallElement = true; // тонкостенный элемент
            ksDocument3D document = (ksDocument3D)_kompas.ActiveDocument3D();
            ksPart part = (ksPart)document.GetPart((short)Part_Type.pTop_Part);  // новый компонент
            if (part != null)
            {
                ksEntity entitySketch = (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
                if (entitySketch != null)
                {
                    // интерфейс свойств эскиза
                    ksSketchDefinition sketchDef = (ksSketchDefinition)entitySketch.GetDefinition();
                    if (sketchDef != null)
                    {

                        // получим интерфейс базовой плоскости XOZ
                        ksEntity basePlane = (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
                        if (basePlane != null)
                        {
                            sketchDef.SetPlane(basePlane);  // установим плоскость XOZ базовой для эскиза
                            entitySketch.Create();          // создадим эскиз

                            // интерфейс редактора эскиза
                            ksDocument2D sketchEdit = (ksDocument2D)sketchDef.BeginEdit();
                            if (sketchEdit != null)
                            {
                                //Построение первого эскиза (тарелки)
                                sketchEdit.ksLineSeg(0, 0, diameterPlate - 10, 0, 1);
                                sketchEdit.ksLineSeg(diameterPlate, -10, diameterPlate, -17, 1);
                                sketchEdit.ksLineSeg(diameterPlate + 3, -20, diameterPlate + 6, -20, 1);
                                sketchEdit.ksLineSeg(diameterPlate + 8, -18, diameterPlate + 8, -17, 1);
                                //Ось
                                sketchEdit.ksLineSeg(0, 0, 0, -22, 3);
                                //Радиусы
                                sketchEdit.ksArcByPoint(diameterPlate + 6, -18, 2, diameterPlate + 6, -20, diameterPlate + 8, -18, 1, 1);
                                sketchEdit.ksArcByPoint(diameterPlate + 3, -17, 3, diameterPlate + 3, -20, diameterPlate, -17, -1, 1);
                                sketchEdit.ksArcByPoint(diameterPlate - 10, -10, 10, diameterPlate, -10, diameterPlate - 10, 0, 1, 1);
                                sketchDef.EndEdit();    // завершение редактирования эскиза
                            }
                            RotationOperation(entitySketch, thinWallElement);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Операция вращения
        /// </summary>
        public void RotationOperation(ksEntity entitySketch, bool thinWallElement)
        {
            ksDocument3D document = (ksDocument3D)_kompas.ActiveDocument3D();
            ksPart part = (ksPart)document.GetPart((short)Part_Type.pTop_Part);  // новый компонент

            // Вращение
            ksEntity entityRotate = (ksEntity)part.NewEntity((short)Obj3dType.o3d_bossRotated);
            if (entityRotate != null)
            {
                ksBossRotatedDefinition rotateDef = (ksBossRotatedDefinition)entityRotate.GetDefinition(); // интерфейс базовой операции вращения
                if (rotateDef != null)
                {
                    ksRotatedParam rotproperty = (ksRotatedParam)rotateDef.RotatedParam();
                    if (rotproperty != null)
                    {
                        rotproperty.direction = (short)Direction_Type.dtBoth;
                        rotproperty.toroidShape = true; //Тороид
                    }

                    rotateDef.SetThinParam(thinWallElement, (short)Direction_Type.dtBoth, 2, 0);   // тонкая стенка в два направления
                    rotateDef.SetSketch(entitySketch);
                    rotateDef.SetSideParam(true, 360);
                    rotateDef.SetSketch(entitySketch);  // эскиз операции вращения
                    entityRotate.Create();              // создать операцию
                }
            }
        }


        /// <summary>
        /// Построение кола
        /// </summary>
        public void StakeBuilding(double diameterStake, double stakeHeight)
        {
            bool thinWallElement = false; // не тонкостенный 
            ksDocument3D document = (ksDocument3D)_kompas.ActiveDocument3D();
            ksPart part = (ksPart)document.GetPart((short)Part_Type.pTop_Part);  // новый компонент
            if (part != null)
            {
                ksEntity entitySketch = (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
                if (entitySketch != null)
                {
                    // интерфейс свойств эскиза
                    ksSketchDefinition sketchDef = (ksSketchDefinition)entitySketch.GetDefinition();
                    if (sketchDef != null)
                    {

                        // получим интерфейс базовой плоскости XOZ
                        ksEntity basePlane = (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
                        if (basePlane != null)
                        {
                            sketchDef.SetPlane(basePlane);  // установим плоскость XOZ базовой для эскиза
                            entitySketch.Create();          // создадим эскиз

                            // интерфейс редактора эскиза
                            ksDocument2D sketchEdit = (ksDocument2D)sketchDef.BeginEdit();
                            if (sketchEdit != null)
                            {
                                sketchEdit.ksLineSeg(0, 0, 0, -diameterStake, 3);
                                sketchEdit.ksLineSeg(0, 0, stakeHeight, 0, 1);

                                sketchEdit.ksLineSeg(0, -diameterStake, stakeHeight-59.012312, -diameterStake + 0.156434, 1);
                                sketchEdit.ksLineSeg(stakeHeight - 59.012312, -diameterStake + 0.156434, stakeHeight-54.697238, -diameterStake + 1.78856, 1);
                                sketchEdit.ksLineSeg(stakeHeight - 54.697238, -diameterStake + 1.78856, stakeHeight-52.695981, -diameterStake+ 3.203695, 1);
                                sketchEdit.ksLineSeg(stakeHeight - 52.695981, -diameterStake + 3.203695, stakeHeight-51, -diameterStake+ 4.62263, 1);
                                sketchEdit.ksLineSeg(stakeHeight - 51, -diameterStake + 4.62263, stakeHeight-49.21806, -diameterStake+6.329173, 1);
                                sketchEdit.ksLineSeg(stakeHeight - 49.21806, -diameterStake + 6.329173, stakeHeight-46.71913, -diameterStake+ 8.979057, 1);
                                sketchEdit.ksLineSeg(stakeHeight - 46.71913, -diameterStake + 8.979057, stakeHeight-43.705286, -diameterStake+ 12.660045, 1);
                                sketchEdit.ksLineSeg(stakeHeight - 43.705286, -diameterStake + 12.660045, stakeHeight-41.058514, -diameterStake+ 16.464908, 1);
                                sketchEdit.ksLineSeg(stakeHeight - 41.058514, -diameterStake + 16.464908, stakeHeight-38.285532, -diameterStake+21.276055, 1);
                                sketchEdit.ksLineSeg(stakeHeight - 38.285532, -diameterStake + 21.276055, stakeHeight-36.579883, -diameterStake + 24.867037, 1);
                                sketchEdit.ksLineSeg(stakeHeight - 36.579883, -diameterStake + 24.867037, stakeHeight-34.80064, -diameterStake+ 29.465247, 1);
                                sketchEdit.ksLineSeg(stakeHeight - 34.80064, -diameterStake + 29.465247, stakeHeight- 33.606017, -diameterStake + 33.387971, 1);
                                sketchEdit.ksLineSeg(stakeHeight - 33.606017, -diameterStake + 33.387971, stakeHeight - 32.706697, -diameterStake + 36.891275, 1);
                                sketchEdit.ksLineSeg(stakeHeight - 32.706697, -diameterStake + 36.891275, stakeHeight-31.667935, -diameterStake+ 41.811957, 1);
                                sketchEdit.ksLineSeg(stakeHeight - 31.667935, -diameterStake + 41.811957, stakeHeight- 30.699933, -diameterStake+ 48.189128, 1);
                                sketchEdit.ksLineSeg(stakeHeight - 30.699933, -diameterStake + 48.189128, stakeHeight- 30.372004, -diameterStake+ 51.382436, 1);
                                sketchEdit.ksLineSeg(stakeHeight - 30.372004, -diameterStake + 51.382436, stakeHeight- 30.095909, -diameterStake+ 55.621345, 1);
                                sketchEdit.ksLineSeg(stakeHeight- 30.095909, -diameterStake+ 55.621345, stakeHeight, 0, 1);

                                //sketchEdit.ksLineSeg(0, -60, 5, -55, 1);
                                //sketchEdit.ksLineSeg(5, -55, 13, -45, 1);
                                //sketchEdit.ksLineSeg(13, -45, 21, -32, 1);
                                //sketchEdit.ksLineSeg(21, -32, 27, -15, 1);
                                //sketchEdit.ksLineSeg(27, -15, 30, 0, 1);

                                // дуги
                                //sketchEdit.ksArcByPoint(-2.053551, -45.141234, 15, 0, -60, 7.795265, -56.454979, 1, 1);
                                //sketchEdit.ksArcByPoint(-31.6, -11.2, 60, 7.795265, -56.454979, 26, -28, 1, 1);
                                //sketchEdit.ksArcByPoint(-70, 0, 100, 26, -28, -30, 0, 1, 1);
                                sketchDef.EndEdit();    // завершение редактирования эскиза
                            }
                            RotationOperation(entitySketch, thinWallElement);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Построение зубцов кола
        /// </summary>
        public void StakeProngs(int count)
        {
            ksDocument3D document = (ksDocument3D)_kompas.ActiveDocument3D();
            ksPart part = (ksPart)document.GetPart((short)Part_Type.pTop_Part);  // новый компонент
            if (part != null)
            {
                ksEntity entitySketchDisplaced = (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
                // создадим смещенную плоскость, а в ней эскиз
                ksEntity entityOffsetPlane2 = (ksEntity)part.NewEntity((short)Obj3dType.o3d_planeOffset);
                if (entityOffsetPlane2 != null && entitySketchDisplaced != null)
                {
                    // интерфейс свойств смещенной плоскости
                    ksPlaneOffsetDefinition offsetDef = (ksPlaneOffsetDefinition)entityOffsetPlane2.GetDefinition();
                    if (offsetDef != null)
                    {
                        offsetDef.offset = 12;  // расстояние от базовой плоскости
                        ksEntity basePlane = (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);

                        offsetDef.SetPlane(basePlane);                      // базовая плоскость
                        entityOffsetPlane2.hidden = true;
                        entityOffsetPlane2.Create();                        // создать смещенную плоскость 

                        ksSketchDefinition sketchDef = (ksSketchDefinition)entitySketchDisplaced.GetDefinition();
                        if (sketchDef != null)
                        {
                            sketchDef.SetPlane(entityOffsetPlane2);         // установим плоскость XOY базовой для эскиза
                            entitySketchDisplaced.Create();                         // создадим эскиз

                            // интерфейс редактора эскиза
                            ksDocument2D sketchEdit = (ksDocument2D)sketchDef.BeginEdit();
                            sketchEdit.ksLineSeg(30, 9.54, 22.060387, 0.000961, 1);
                            sketchEdit.ksLineSeg(22.060387, 0.000961, 30, -9.54, 1);
                            sketchEdit.ksLineSeg(30, -9.54, 30, 9.54, 1);
                            sketchDef.EndEdit();                            // завершение редактирования эскиза                
                        }
                    }
                }

                // Эскиз линии, которая в дальнейшем будет траекторией выреза по траектории
                ksEntity entitySketch = (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
                if (entitySketch != null)
                {
                    // интерфейс свойств эскиза
                    ksSketchDefinition sketchDef = (ksSketchDefinition)entitySketch.GetDefinition();
                    if (sketchDef != null)
                    {

                        // получим интерфейс базовой плоскости XOZ
                        ksEntity basePlane = (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
                        if (basePlane != null)
                        {
                            sketchDef.SetPlane(basePlane);  // установим плоскость XOZ базовой для эскиза
                            entitySketch.Create();          // создадим эскиз

                            // интерфейс редактора эскиза
                            ksDocument2D sketchEdit = (ksDocument2D)sketchDef.BeginEdit();
                            if (sketchEdit != null)
                            {
                                sketchEdit.ksArcByPoint(-47.930751, -10.887111, 70, 2.5, -59.433371, 22.060387, -12.000961, 1, 1);
                                sketchDef.EndEdit();	// завершение редактирования эскиза

                                // Вырез по траектории
                                ksEntity entityCutEvolution = (ksEntity)part.NewEntity((short)Obj3dType.o3d_cutEvolution);
                                if (entityCutEvolution != null)
                                {
                                    ksCutEvolutionDefinition cutEvolutionDef = (ksCutEvolutionDefinition)entityCutEvolution.GetDefinition();

                                    if (cutEvolutionDef != null)
                                    {
                                        cutEvolutionDef.SetThinParam(false, (short)Direction_Type.dtBoth, 0, 0);    // тонкая стенка в два направления
                                        cutEvolutionDef.SetSketch(entitySketchDisplaced);
                                        ksEntityCollection iPathPartArray = (ksEntityCollection)cutEvolutionDef.PathPartArray();
                                        iPathPartArray.Add(entitySketch);
                                    }

                                    entityCutEvolution.Create();    // создадим операцию вырезания по траектории

                                    //Отверстия по концетрической сетке
                                    ksEntity circularCopyEntity = (ksEntity)part.NewEntity((short)Obj3dType.o3d_circularCopy);
                                    ksCircularCopyDefinition circularCopyDefinition =
                                        (ksCircularCopyDefinition)circularCopyEntity.GetDefinition();
                                    circularCopyDefinition.SetCopyParamAlongDir(count, 360,
                                        true, false);
                                    ksEntity baseAxisOZ = (ksEntity)part.GetDefaultEntity((short)
                                        Obj3dType.o3d_axisOZ);
                                    circularCopyDefinition.SetAxis(baseAxisOZ);
                                    ksEntityCollection entityCollection = (ksEntityCollection)
                                        circularCopyDefinition.GetOperationArray();
                                    entityCollection.Add(cutEvolutionDef);
                                    circularCopyEntity.Create();
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Отверстия в тарелке
        /// </summary>
        public void HolesInThePlate(int count, double d)
        {
            ksDocument3D document = (ksDocument3D)_kompas.ActiveDocument3D();
            ksPart part = (ksPart)document.GetPart((short)Part_Type.pTop_Part);  // новый компонент
            if (part != null)
            {
                ksEntity entitySketch = (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
                if (entitySketch != null)
                {
                    // интерфейс свойств эскиза
                    ksSketchDefinition sketchDef = (ksSketchDefinition)entitySketch.GetDefinition();
                    if (sketchDef != null)
                    {

                        // получим интерфейс базовой плоскости XOY
                        ksEntity basePlane = (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);
                        if (basePlane != null)
                        {
                            sketchDef.SetPlane(basePlane);  // установим плоскость XOZ базовой для эскиза
                            entitySketch.Create();          // создадим эскиз

                            // интерфейс редактора эскиза
                            ksDocument2D sketchEdit = (ksDocument2D)sketchDef.BeginEdit();
                            if (sketchEdit != null)
                            {
                                sketchEdit.ksLineSeg(-0.75, -(d-10.5), -0.75, -35, 1);
                                sketchEdit.ksLineSeg(-0.75, -35, 0.75, -35, 1);
                                sketchEdit.ksLineSeg(0.75, -35, 0.75, -(d - 10.5), 1);
                                sketchEdit.ksLineSeg(0.75, -(d - 10.5), -0.75, -(d - 10.5), 1);
                                sketchDef.EndEdit();    // завершение редактирования эскиза

                                //Вырезать выдавливанием
                                ksEntity entityCutExtr = (ksEntity)part.NewEntity((short)Obj3dType.o3d_cutExtrusion);
                                if (entityCutExtr != null)
                                {
                                    ksCutExtrusionDefinition cutExtrDef = (ksCutExtrusionDefinition)entityCutExtr.GetDefinition();
                                    if (cutExtrDef != null)
                                    {
                                        cutExtrDef.SetSketch(entitySketch);    // установим эскиз операции
                                        cutExtrDef.directionType = (short)Direction_Type.dtBoth; //прямое направление
                                        cutExtrDef.SetSideParam(true, (short)End_Type.etBlind, 5, 0, true);
                                        cutExtrDef.SetThinParam(false, 0, 0, 0);
                                    }
                                    entityCutExtr.Create(); // создадим операцию вырезание выдавливанием

                                    //Отверстия по концетрической сетке
                                    ksEntity circularCopyEntity = (ksEntity)part.NewEntity((short)Obj3dType.o3d_circularCopy);
                                    ksCircularCopyDefinition circularCopyDefinition = (ksCircularCopyDefinition)circularCopyEntity.GetDefinition();
                                    circularCopyDefinition.SetCopyParamAlongDir(count, 360, true, false);
                                    ksEntity baseAxisOZ = (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_axisOZ);
                                    circularCopyDefinition.SetAxis(baseAxisOZ);
                                    ksEntityCollection entityCollection = (ksEntityCollection)circularCopyDefinition.GetOperationArray();
                                    entityCollection.Add(cutExtrDef);
                                    circularCopyEntity.Create();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
