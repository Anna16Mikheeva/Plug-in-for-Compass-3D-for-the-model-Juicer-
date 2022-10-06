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
        private KompasObject _kompas = null;

        //Запуск компаса
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

            //Создание файла
            KompasObject kompas = _kompas;
            _kompas = kompas;
            var document = (ksDocument3D)kompas.Document3D();
            document.Create();

            var doc = (ksDocument3D)_kompas.ActiveDocument3D();
            var part = (ksPart)document.GetPart((short)Part_Type.pTop_Part);
            OperationRotated(doc);
            Eskiz2(doc);
            //Eskiz3(doc);
            Eskiz4(doc);
            Eskiz5(doc);
        }

        /// <summary>
        /// Вращение
        /// </summary>
        /// <param name="doc"></param>
        void OperationRotated(ksDocument3D doc)
        {
            // Эскиз
            ksPart part = (ksPart)doc.GetPart((short)Part_Type.pTop_Part);  // новый компонент
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
                                sketchEdit.ksLineSeg(0, 0, 70, 0, 1);
                                sketchEdit.ksLineSeg(80, -10, 80, -17, 1);
                                sketchEdit.ksLineSeg(83, -20, 86, -20, 1);
                                sketchEdit.ksLineSeg(88, -18, 88, -17, 1);
                                //Ось
                                sketchEdit.ksLineSeg(0, 0, 0, -22, 3);
                                //Радиусы
                                sketchEdit.ksArcByPoint(86, -18, 2, 86, -20, 88, -18, 1, 1);
                                sketchEdit.ksArcByPoint(83, -17, 3, 83, -20, 80, -17, -1, 1);
                                sketchEdit.ksArcByPoint(70, -10, 10, 80, -10, 70, 0, 1, 1);
                                sketchDef.EndEdit();    // завершение редактирования эскиза
                            }

                            //Вращение
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

                                    rotateDef.SetThinParam(true, (short)Direction_Type.dtBoth, 2, 0);   // тонкая стенка в два направления
                                    rotateDef.SetSketch(entitySketch);
                                    rotateDef.SetSideParam(true, 360);
                                    rotateDef.SetSketch(entitySketch);  // эскиз операции вращения
                                    entityRotate.Create();              // создать операцию
                                }
                            }
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Кол
        /// </summary>
        public void Eskiz2(ksDocument3D doc)
        {
            // Эскиз
            ksPart part = (ksPart)doc.GetPart((short)Part_Type.pTop_Part);  // новый компонент
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
                                sketchEdit.ksLineSeg(0, 0, 0, -60, 3);
                                sketchEdit.ksLineSeg(0, 0, 30, 0, 1);

                                //Дуги
                                sketchEdit.ksArcByPoint(-2.053551, -45.141234, 15, 0, -60, 7.795265, -56.454979, 1, 1);
                                sketchEdit.ksArcByPoint(-31.6, -11.2, 60, 7.795265, -56.454979, 26, -28, 1, 1);
                                sketchEdit.ksArcByPoint(-70, 0, 100, 26, -28, -30, 0, 1, 1);
                                sketchDef.EndEdit();    // завершение редактирования эскиза
                            }

                            //Вращение
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
                                        //rotproperty.angleNormal = 360; //Тороид
                                    }

                                    rotateDef.SetSketch(entitySketch);
                                    rotateDef.SetSideParam(true, 360);
                                    rotateDef.SetSketch(entitySketch);  // эскиз операции вращения
                                    entityRotate.Create();              // создать операцию
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Кол
        /// </summary>
        public void Eskiz3(ksDocument3D doc)
        {
            ksPart part = (ksPart)doc.GetPart((short)Part_Type.pTop_Part);  // новый компонент
            if (part != null)
            {
                // создадим эскиз
                ksEntity entitySketch = (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
                if (entitySketch != null)
                {
                    ksSketchDefinition sketchDef = (ksSketchDefinition)entitySketch.GetDefinition();
                    if (sketchDef != null)
                    {
                        ksEntity basePlane = (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);    // получим интерфейс базовой плоскости XOY
                        sketchDef.SetPlane(basePlane);  // установим плоскость XOY базовой для эскиза
                        entitySketch.Create();          // создадим эскиз
                        entitySketch.hidden = false;

                        // интерфейс редактора эскиза
                        ksDocument2D sketchEdit = (ksDocument2D)sketchDef.BeginEdit();
                        //if (sketchEdit != null)
                        //    sketchEdit.ksCircle(0, 0, , 1);
                        sketchDef.EndEdit();    // завершение редактирования эскиза
                    }
                }


                // создадим смещенную плоскость, а в ней эскиз
                ksEntity entityOffsetPlane2 = (ksEntity)part.NewEntity((short)Obj3dType.o3d_planeOffset);
                ksEntity entitySketch3 = (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
                if (entityOffsetPlane2 != null && entitySketch3 != null)
                {
                    // интерфейс свойств смещенной плоскости
                    ksPlaneOffsetDefinition offsetDef = (ksPlaneOffsetDefinition)entityOffsetPlane2.GetDefinition();
                    if (offsetDef != null)
                    {
                        offsetDef.offset = 12;  // расстояние от базовой плоскости
                        ksEntity basePlane = (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);
                        basePlane.name = "Смещенная плоскость"; // название для плоскости

                        offsetDef.SetPlane(basePlane);                      // базовая плоскость
                        entityOffsetPlane2.name = "Смещенная плоскость2";   // имя для смещенной плоскости
                        entityOffsetPlane2.hidden = true;
                        entityOffsetPlane2.Create();                        // создать смещенную плоскость 

                        ksSketchDefinition sketchDef = (ksSketchDefinition)entitySketch3.GetDefinition();
                        if (sketchDef != null)
                        {
                            sketchDef.SetPlane(entityOffsetPlane2);         // установим плоскость XOY базовой для эскиза
                            entitySketch3.Create();                         // создадим эскиз

                            // интерфейс редактора эскиза
                            ksDocument2D sketchEdit = (ksDocument2D)sketchDef.BeginEdit();
                            sketchEdit.ksLineSeg(30, 9.54, 22.060387, 0.000961, 1);
                            sketchEdit.ksLineSeg(22.060387, 0.000961, 30, -9.54, 1);
                            sketchEdit.ksLineSeg(30, -9.54, 30, 9.54, 1);
                            sketchDef.EndEdit();                            // завершение редактирования эскиза                
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Зубцы
        /// </summary>
        public void Eskiz4(ksDocument3D doc)
        {
            ksPart part = (ksPart)doc.GetPart((short)Part_Type.pTop_Part);  // новый компонент
            ksEntity entitySketch = (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
            ksEntity entitySketch3 = (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
            if (part != null)
            {
                // создадим эскиз
                //ksEntity entitySketch = (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
                if (entitySketch != null)
                {
                    ksSketchDefinition sketchDef = (ksSketchDefinition)entitySketch.GetDefinition();
                    if (sketchDef != null)
                    {
                        ksEntity basePlane = (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);    // получим интерфейс базовой плоскости XOY
                        sketchDef.SetPlane(basePlane);  // установим плоскость XOY базовой для эскиза
                        entitySketch.Create();          // создадим эскиз
                        entitySketch.hidden = false;

                        // интерфейс редактора эскиза
                        ksDocument2D sketchEdit = (ksDocument2D)sketchDef.BeginEdit();
                        //if (sketchEdit != null)
                        //    sketchEdit.ksCircle(0, 0, , 1);
                        sketchDef.EndEdit();    // завершение редактирования эскиза
                    }
                }


                // создадим смещенную плоскость, а в ней эскиз
                ksEntity entityOffsetPlane2 = (ksEntity)part.NewEntity((short)Obj3dType.o3d_planeOffset);
                //ksEntity entitySketch3 = (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
                entitySketch3 = (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
                if (entityOffsetPlane2 != null && entitySketch3 != null)
                {
                    // интерфейс свойств смещенной плоскости
                    ksPlaneOffsetDefinition offsetDef = (ksPlaneOffsetDefinition)entityOffsetPlane2.GetDefinition();
                    if (offsetDef != null)
                    {
                        offsetDef.offset = 12;  // расстояние от базовой плоскости
                        ksEntity basePlane = (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);
                        basePlane.name = "Смещенная плоскость"; // название для плоскости

                        offsetDef.SetPlane(basePlane);                      // базовая плоскость
                        entityOffsetPlane2.name = "Смещенная плоскость2";   // имя для смещенной плоскости
                        entityOffsetPlane2.hidden = true;
                        entityOffsetPlane2.Create();                        // создать смещенную плоскость 

                        ksSketchDefinition sketchDef = (ksSketchDefinition)entitySketch3.GetDefinition();
                        if (sketchDef != null)
                        {
                            sketchDef.SetPlane(entityOffsetPlane2);         // установим плоскость XOY базовой для эскиза
                            entitySketch3.Create();                         // создадим эскиз

                            // интерфейс редактора эскиза
                            ksDocument2D sketchEdit = (ksDocument2D)sketchDef.BeginEdit();
                            sketchEdit.ksLineSeg(30, 9.54, 22.060387, 0.000961, 1);
                            sketchEdit.ksLineSeg(22.060387, 0.000961, 30, -9.54, 1);
                            sketchEdit.ksLineSeg(30, -9.54, 30, 9.54, 1);
                            sketchDef.EndEdit();                            // завершение редактирования эскиза                
                        }
                    }
                }
            }

            // Эскиз
            ksPart part1 = (ksPart)doc.GetPart((short)Part_Type.pTop_Part);  // новый компонент
            if (part != null)
            {
                ksEntity entitySketch1 = (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
                if (entitySketch1 != null)
                {
                    // интерфейс свойств эскиза
                    ksSketchDefinition sketchDef = (ksSketchDefinition)entitySketch1.GetDefinition();
                    if (sketchDef != null)
                    {

                        // получим интерфейс базовой плоскости XOZ
                        ksEntity basePlane = (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
                        if (basePlane != null)
                        {
                            sketchDef.SetPlane(basePlane);  // установим плоскость XOZ базовой для эскиза
                            entitySketch1.Create();          // создадим эскиз

                            // интерфейс редактора эскиза
                            ksDocument2D sketchEdit = (ksDocument2D)sketchDef.BeginEdit();
                            if (sketchEdit != null)
                            {
                                sketchEdit.ksArcByPoint(-47.930751, -10.887111, 70, 2.5, -59.433371, 22.060387, -12.000961, 1, 1);
                                sketchDef.EndEdit();	// завершение редактирования эскиза
                                ksEntity entityCutEvolution = (ksEntity)part.NewEntity((short)Obj3dType.o3d_cutEvolution);
                                if (entityCutEvolution != null)
                                {
                                    ksCutEvolutionDefinition cutEvolutionDef = (ksCutEvolutionDefinition)entityCutEvolution.GetDefinition();
                                    
                                    if (cutEvolutionDef != null)
                                    {
                                        cutEvolutionDef.SetThinParam(false, (short)Direction_Type.dtBoth, 0, 0);    // тонкая стенка в два направления
                                        cutEvolutionDef.SetSketch(entitySketch3);
                                        ksEntityCollection iPathPartArray = (ksEntityCollection)cutEvolutionDef.PathPartArray();
                                        iPathPartArray.Add(entitySketch1);
                                    }

                                    entityCutEvolution.Create();    // создадим операцию вырезания по траектории

                                    //Отверстия по концетрической сетке
                                    ksEntity circularCopyEntity = (ksEntity)part.NewEntity((short)Obj3dType.o3d_circularCopy);
                                    ksCircularCopyDefinition circularCopyDefinition =
                                        (ksCircularCopyDefinition)circularCopyEntity.GetDefinition();
                                    circularCopyDefinition.SetCopyParamAlongDir(9, 360,
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
        public void Eskiz5(ksDocument3D doc)
        {
            // Эскиз
            ksPart part = (ksPart)doc.GetPart((short)Part_Type.pTop_Part);  // новый компонент
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
                                sketchEdit.ksLineSeg(-0.75, -69.5, -0.75, -35, 1);
                                sketchEdit.ksLineSeg(-0.75, -35, 0.75, -35, 1);
                                sketchEdit.ksLineSeg(0.75, -35, 0.75, -69.5, 1);
                                sketchEdit.ksLineSeg(0.75, -69.5, -0.75, -69.5, 1);
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
                                    ksCircularCopyDefinition circularCopyDefinition =
                                        (ksCircularCopyDefinition)circularCopyEntity.GetDefinition();
                                    circularCopyDefinition.SetCopyParamAlongDir(90, 360,
                                        true, false);
                                    ksEntity baseAxisOZ = (ksEntity)part.GetDefaultEntity((short)
                                        Obj3dType.o3d_axisOZ);
                                    circularCopyDefinition.SetAxis(baseAxisOZ);
                                    ksEntityCollection entityCollection = (ksEntityCollection)
                                        circularCopyDefinition.GetOperationArray();
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
