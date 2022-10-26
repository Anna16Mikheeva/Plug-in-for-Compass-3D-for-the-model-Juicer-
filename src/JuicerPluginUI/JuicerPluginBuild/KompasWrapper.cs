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
    /// Класс для работы с Компас-3D
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
                        throw new Exception
                            ("Не удается открыть Koмпас-3D");
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
        public void CreateFile()
        {
            try
            {
                var document = (ksDocument3D)_kompas.Document3D();
                document.Create();
            }
            catch
            {
                throw new ArgumentException
                    ("Не удается построить деталь");
            }
        }

        /// <summary>
        /// Построение эскиза тарелки соковыжималки
        /// </summary>
        public void PlateSketch(double diameterPlate)
        {
            bool thinWallElement = true; // тонкостенный элемент
            ksDocument3D document = 
                (ksDocument3D)_kompas.ActiveDocument3D();
            ksPart part = (ksPart)document.GetPart
                ((short)Part_Type.pTop_Part);  // новый компонент
            if (part != null)
            {
                ksEntity entitySketch = (ksEntity)part.NewEntity
                    ((short)Obj3dType.o3d_sketch);
                if (entitySketch != null)
                {
                    // интерфейс свойств эскиза
                    ksSketchDefinition sketchDef = 
                        (ksSketchDefinition)entitySketch.GetDefinition();
                    if (sketchDef != null)
                    {

                        // получим интерфейс базовой плоскости XOZ
                        ksEntity basePlane = (ksEntity)part.
                            GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
                        if (basePlane != null)
                        {
                            // установим плоскость XOZ базовой для эскиза
                            sketchDef.SetPlane(basePlane);
                            // создадим эскиз
                            entitySketch.Create();          

                            // интерфейс редактора эскиза
                            ksDocument2D sketchEdit = 
                                (ksDocument2D)sketchDef.BeginEdit();
                            if (sketchEdit != null)
                            {
                                diameterPlate = diameterPlate / 2;
                                //Построение первого эскиза (тарелки)
                                sketchEdit.ksLineSeg
                                    (0, 0, diameterPlate - 10, 0, 1);
                                sketchEdit.ksLineSeg
                                    (diameterPlate, -10, diameterPlate, -17, 1);
                                sketchEdit.ksLineSeg
                                    (diameterPlate + 3, -20, diameterPlate + 6, -20, 1);
                                sketchEdit.ksLineSeg
                                    (diameterPlate + 8, -18, diameterPlate + 8, -17, 1);
                                //Ось
                                sketchEdit.ksLineSeg(0, 0, 0, -22, 3);
                                //Радиусы
                                sketchEdit.ksArcByPoint
                                    (diameterPlate + 6, -18, 2, diameterPlate + 6, -20,
                                    diameterPlate + 8, -18, 1, 1);
                                sketchEdit.ksArcByPoint
                                    (diameterPlate + 3, -17, 3, diameterPlate + 3, 
                                    -20, diameterPlate, -17, -1, 1);
                                sketchEdit.ksArcByPoint
                                    (diameterPlate - 10, -10, 10, diameterPlate, 
                                    -10, diameterPlate - 10, 0, 1, 1);
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
            ksPart part = (ksPart)document.
                GetPart((short)Part_Type.pTop_Part);  // новый компонент

            // Вращение
            ksEntity entityRotate = (ksEntity)part.
                NewEntity((short)Obj3dType.o3d_bossRotated);
            if (entityRotate != null)
            {
                ksBossRotatedDefinition rotateDef = 
                    (ksBossRotatedDefinition)entityRotate.
                    GetDefinition(); // интерфейс базовой операции вращения
                if (rotateDef != null)
                {
                    ksRotatedParam rotproperty = 
                        (ksRotatedParam)rotateDef.RotatedParam();
                    if (rotproperty != null)
                    {
                        rotproperty.direction = 
                            (short)Direction_Type.dtBoth;
                        rotproperty.toroidShape = true; //Тороид
                    }

                    // тонкая стенка в два направления
                    rotateDef.SetThinParam(thinWallElement, 
                        (short)Direction_Type.dtBoth, 2, 0);   
                    rotateDef.SetSketch(entitySketch);
                    rotateDef.SetSideParam(true, 360);
                    // эскиз операции вращения
                    rotateDef.SetSketch(entitySketch);
                    entityRotate.Create(); // создать операцию
                }
            }
        }


        /// <summary>
        /// Построение кола
        /// </summary>
        public void StakeBuilding(double diameterStake, double stakeHeight)
        {
            bool thinWallElement = false; // не тонкостенный 
            ksDocument3D document = (ksDocument3D)_kompas.
                ActiveDocument3D();
            // новый компонент
            ksPart part = (ksPart)document.
                GetPart((short)Part_Type.pTop_Part);  
            if (part != null)
            {
                ksEntity entitySketch = (ksEntity)part.
                    NewEntity((short)Obj3dType.o3d_sketch);
                if (entitySketch != null)
                {
                    // интерфейс свойств эскиза
                    ksSketchDefinition sketchDef = 
                        (ksSketchDefinition)entitySketch.GetDefinition();
                    if (sketchDef != null)
                    {

                        // получим интерфейс базовой плоскости XOZ
                        ksEntity basePlane = (ksEntity)part.
                            GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
                        if (basePlane != null)
                        {
                            // установим плоскость XOZ базовой для эскиза
                            sketchDef.SetPlane(basePlane);
                            // создадим эскиз
                            entitySketch.Create();          

                            // интерфейс редактора эскиза
                            ksDocument2D sketchEdit = 
                                (ksDocument2D)sketchDef.BeginEdit();
                            if (sketchEdit != null)
                            {
                                double radiusStake = diameterStake / 2;
                                sketchEdit.ksLineSeg
                                    (0, 0, radiusStake, 0, 1);
                                sketchEdit.ksLineSeg
                                    (0, 0, 0, -stakeHeight, 3);

                                sketchEdit.ksLineSeg
                                    (0, -stakeHeight, radiusStake - 10.177112, 
                                    -stakeHeight + 17.885819, 1);
                                sketchEdit.ksLineSeg
                                    (radiusStake - 10.177112, -stakeHeight + 17.885819, 
                                    radiusStake - 7.024134, -stakeHeight + 23.870603, 1);
                                sketchEdit.ksLineSeg
                                    (radiusStake - 7.024134, -stakeHeight + 23.870603, 
                                    radiusStake - 4, -stakeHeight + 32, 1);
                                sketchEdit.ksLineSeg
                                    (radiusStake - 4, -stakeHeight + 32, 
                                    radiusStake - 2.18061, -stakeHeight + 39.230625, 1);
                                sketchEdit.ksLineSeg
                                    (radiusStake - 2.18061, -stakeHeight + 39.230625, 
                                    radiusStake - 1.260657, -stakeHeight + 44.171475, 1);
                                sketchEdit.ksLineSeg
                                    (radiusStake - 1.260657, -stakeHeight + 44.171475, 
                                    radiusStake - 0.531802, -stakeHeight + 49.700599, 1);
                                sketchEdit.ksLineSeg
                                    (radiusStake - 0.531802, -stakeHeight + 49.700599, 
                                    radiusStake - 0.166451, -stakeHeight + 54.232637, 1);
                                sketchEdit.ksLineSeg
                                    (radiusStake - 0.166451, -stakeHeight + 54.232637, 
                                    radiusStake, 0, 1);

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
        public void StakeProngs(int count, double diameterStake, 
            double stakeHeight)
        {
            ksDocument3D document = 
                (ksDocument3D)_kompas.ActiveDocument3D();
            ksPart part = (ksPart)document.
                GetPart((short)Part_Type.pTop_Part);  // новый компонент
            if (part != null)
            {
                ksEntity entitySketchDisplaced = 
                    (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
                // создадим смещенную плоскость, а в ней эскиз
                ksEntity entityOffsetPlane2 = 
                    (ksEntity)part.NewEntity((short)Obj3dType.o3d_planeOffset);
                if (entityOffsetPlane2 != null && 
                    entitySketchDisplaced != null)
                {
                    // интерфейс свойств смещенной плоскости
                    ksPlaneOffsetDefinition offsetDef = 
                        (ksPlaneOffsetDefinition)entityOffsetPlane2.GetDefinition();
                    if (offsetDef != null)
                    {
                        offsetDef.offset = 12;  // расстояние от базовой плоскости
                        ksEntity basePlane = (ksEntity)part.
                            GetDefaultEntity((short)Obj3dType.o3d_planeXOY);

                        offsetDef.SetPlane(basePlane); // базовая плоскость
                        entityOffsetPlane2.hidden = true;
                        entityOffsetPlane2.Create(); // создать смещенную плоскость 

                        ksSketchDefinition sketchDef = 
                            (ksSketchDefinition)entitySketchDisplaced.GetDefinition();
                        if (sketchDef != null)
                        {
                            sketchDef.SetPlane(entityOffsetPlane2); // установим плоскость XOY базовой для эскиза
                            entitySketchDisplaced.Create(); // создадим эскиз

                            // интерфейс редактора эскиза
                            ksDocument2D sketchEdit = 
                                (ksDocument2D)sketchDef.BeginEdit();
                            sketchEdit.ksLineSeg
                                (diameterStake / 2 + 10, 9.54, 22.060387, 0.000961, 1);
                            sketchEdit.ksLineSeg
                                (22.060387, 0.000961, diameterStake / 2+10, -9.54, 1);
                            sketchEdit.ksLineSeg
                                (diameterStake / 2+10, -9.54, diameterStake / 2+10, 9.54, 1);
                            sketchDef.EndEdit(); // завершение редактирования эскиза                
                        }
                    }
                }

                // Эскиз линии, которая в дальнейшем будет траекторией выреза по траектории
                ksEntity entitySketch = (ksEntity)part.
                    NewEntity((short)Obj3dType.o3d_sketch);
                if (entitySketch != null)
                {
                    // интерфейс свойств эскиза
                    ksSketchDefinition sketchDef = 
                        (ksSketchDefinition)entitySketch.GetDefinition();
                    if (sketchDef != null)
                    {

                        // получим интерфейс базовой плоскости XOZ
                        ksEntity basePlane = (ksEntity)part.
                            GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
                        if (basePlane != null)
                        {
                            sketchDef.SetPlane(basePlane);  // установим плоскость XOZ базовой для эскиза
                            entitySketch.Create();          // создадим эскиз

                            // интерфейс редактора эскиза
                            ksDocument2D sketchEdit = 
                                (ksDocument2D)sketchDef.BeginEdit();
                            if (sketchEdit != null)
                            {
                                sketchEdit.ksLineSeg
                                    (9.259353, -stakeHeight - 1.020943, 2.290188, -stakeHeight - 11.995037, 1);
                                sketchEdit.ksLineSeg
                                    (13.435794, -53.535092, 9.259353, -stakeHeight - 1.020943, 1);
                                sketchEdit.ksLineSeg
                                    (13.435794, -53.535092, 16.39727, -45.132963, 1);
                                sketchEdit.ksLineSeg
                                    (16.39727, -45.132963, 19.282811, -33.108725, 1);
                                sketchEdit.ksLineSeg
                                    (19.282811, -33.108725, 20.963563, -20.933876, 1);
                                sketchEdit.ksLineSeg
                                    (20.963563, -20.933876, 22.060387, -12.000961, 1);
                                sketchDef.EndEdit();	// завершение редактирования эскиза

                                // Вырез по траектории
                                ksEntity entityCutEvolution = (ksEntity)part.
                                    NewEntity((short)Obj3dType.o3d_cutEvolution);
                                if (entityCutEvolution != null)
                                {
                                    ksCutEvolutionDefinition cutEvolutionDef = 
                                        (ksCutEvolutionDefinition)entityCutEvolution.GetDefinition();

                                    if (cutEvolutionDef != null)
                                    {
                                        cutEvolutionDef.SetThinParam
                                            (false, (short)Direction_Type.dtBoth, 0, 0);    // тонкая стенка в два направления
                                        cutEvolutionDef.SetSketch(entitySketchDisplaced);
                                        ksEntityCollection iPathPartArray = 
                                            (ksEntityCollection)cutEvolutionDef.PathPartArray();
                                        iPathPartArray.Add(entitySketch);
                                    }

                                    entityCutEvolution.Create();    // создадим операцию вырезания по траектории

                                    //Отверстия по концетрической сетке
                                    ksEntity circularCopyEntity = (ksEntity)part.
                                        NewEntity((short)Obj3dType.o3d_circularCopy);
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
        public void HolesInThePlate(int count, double diameterPlate, 
            double diameterStake)
        {
            ksDocument3D document = 
                (ksDocument3D)_kompas.ActiveDocument3D();
            ksPart part = (ksPart)document.
                GetPart((short)Part_Type.pTop_Part);  // новый компонент
            if (part != null)
            {
                ksEntity entitySketch = 
                    (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
                if (entitySketch != null)
                {
                    // интерфейс свойств эскиза
                    ksSketchDefinition sketchDef = 
                        (ksSketchDefinition)entitySketch.GetDefinition();
                    if (sketchDef != null)
                    {

                        // получим интерфейс базовой плоскости XOY
                        ksEntity basePlane = (ksEntity)part.
                            GetDefaultEntity((short)Obj3dType.o3d_planeXOY);
                        if (basePlane != null)
                        {
                            sketchDef.SetPlane(basePlane);  // установим плоскость XOZ базовой для эскиза
                            entitySketch.Create();          // создадим эскиз

                            // интерфейс редактора эскиза
                            ksDocument2D sketchEdit = 
                                (ksDocument2D)sketchDef.BeginEdit();
                            if (sketchEdit != null)
                            {
                                sketchEdit.ksLineSeg
                                    (-0.75, -(diameterPlate/2-10.5), -0.75, -diameterStake/2-5, 1);
                                sketchEdit.ksLineSeg
                                    (-0.75, -diameterStake/2 - 5, 0.75, -diameterStake/2 - 5, 1);
                                sketchEdit.ksLineSeg
                                    (0.75, -diameterStake/2 - 5, 0.75, -(diameterPlate/2 - 10.5), 1);
                                sketchEdit.ksLineSeg
                                    (0.75, -(diameterPlate/2 - 10.5), -0.75, -(diameterPlate/2 - 10.5), 1);
                                sketchDef.EndEdit();    // завершение редактирования эскиза

                                //Вырезать выдавливанием
                                ksEntity entityCutExtr = (ksEntity)part.
                                    NewEntity((short)Obj3dType.o3d_cutExtrusion);
                                if (entityCutExtr != null)
                                {
                                    ksCutExtrusionDefinition cutExtrDef = 
                                        (ksCutExtrusionDefinition)entityCutExtr.GetDefinition();
                                    if (cutExtrDef != null)
                                    {
                                        cutExtrDef.SetSketch(entitySketch);    // установим эскиз операции
                                        cutExtrDef.directionType = 
                                            (short)Direction_Type.dtBoth; //прямое направление
                                        cutExtrDef.SetSideParam
                                            (true, (short)End_Type.etBlind, 5, 0, true);
                                        cutExtrDef.SetThinParam(false, 0, 0, 0);
                                    }
                                    entityCutExtr.Create(); // создадим операцию вырезание выдавливанием

                                    //Отверстия по концетрической сетке
                                    ksEntity circularCopyEntity = 
                                        (ksEntity)part.NewEntity((short)Obj3dType.o3d_circularCopy);
                                    ksCircularCopyDefinition circularCopyDefinition = 
                                        (ksCircularCopyDefinition)circularCopyEntity.GetDefinition();
                                    circularCopyDefinition.SetCopyParamAlongDir(count, 360, true, false);
                                    ksEntity baseAxisOZ = 
                                        (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_axisOZ);
                                    circularCopyDefinition.SetAxis(baseAxisOZ);
                                    ksEntityCollection entityCollection = 
                                        (ksEntityCollection)circularCopyDefinition.GetOperationArray();
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
