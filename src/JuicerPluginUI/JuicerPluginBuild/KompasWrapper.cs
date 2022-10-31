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
    /// Класс для работы с Компас-3D
    /// </summary>
    public class KompasWrapper
    {
        /// <summary>
        /// Объект Компас API
        /// </summary>
        private KompasObject _kompas = null;

        /// <summary>
        /// Метод для запуска Компас-3D
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
                    var kompasType = Type.GetTypeFromProgID
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
        /// Метод создания файла в Компас-3D
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
        /// Метод построения тарелки соковыжималки
        /// </summary>
        public void PlateSketch(double diameterPlate)
        {
            var document = 
                (ksDocument3D)_kompas.ActiveDocument3D();
            // Новый компонент
            var part = 
                (ksPart)document.GetPart((short)Part_Type.pTop_Part);
	        // TODO: большая вложенность
            if (part == null) return;
            var entitySketch = 
                (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
            if (entitySketch == null) return;
            // Интерфейс свойств эскиза
            var sketchDef = 
                (ksSketchDefinition)entitySketch.GetDefinition();
            if (sketchDef == null) return;
            // Получим интерфейс базовой плоскости XOZ
            var basePlane = 
                (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
            if (basePlane == null) return;
            // Установим плоскость XOZ базовой для эскиза
            sketchDef.SetPlane(basePlane);
            // Создадим эскиз
            entitySketch.Create();          

            // интерфейс редактора эскиза
            var sketchEdit = 
                (ksDocument2D)sketchDef.BeginEdit();
            if (sketchEdit != null)
            {
                diameterPlate /= 2;
                //Построение первого эскиза (тарелки)
                // TODO: Магические числа
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
                // TODO: все комментарии ставятся перед коментируемой строкой
                // Завершение редактирования эскиза
                sketchDef.EndEdit();    
            }
            RotateExtrusion(entitySketch, true);
        }

		/// <summary>
		/// Метод операции выдавливания вращением
		/// </summary>
		// TODO: Методы должны начинаться с глагола
		public void RotateExtrusion(ksEntity entitySketch, bool thinWallElement)
        {
            var document = (ksDocument3D)_kompas.ActiveDocument3D();
            // Новый компонент
			var part = 
                (ksPart)document.GetPart((short)Part_Type.pTop_Part);

            // Вращение
            var entityRotate = 
                (ksEntity)part.NewEntity((short)Obj3dType.o3d_bossRotated);
            if (entityRotate == null) return;
            // Интерфейс базовой операции вращения
            var rotateDef = 
                (ksBossRotatedDefinition)entityRotate.GetDefinition(); 
            if (rotateDef == null) return;
            var rotproperty = 
                (ksRotatedParam)rotateDef.RotatedParam();
            if (rotproperty != null)
            {
                rotproperty.direction = 
                    (short)Direction_Type.dtBoth;
                // Тороид
                rotproperty.toroidShape = true; 
            }

            // Тонкая стенка в два направления
            rotateDef.SetThinParam
                (thinWallElement, (short)Direction_Type.dtBoth, 2, 0);   
            rotateDef.SetSketch(entitySketch);
            rotateDef.SetSideParam(true, 360);
            // Эскиз операции вращения
            rotateDef.SetSketch(entitySketch);
            // TODO: все комментарии ставятся перед коментируемой строкой
            // Создать операцию
            entityRotate.Create();
        }


		/// <summary>
		/// Метод построения кола
		/// </summary>
		// TODO: Методы должны начинаться с глагола
		public void BuildStake(double diameterStake, double stakeHeight)
		{
			// TODO: все комментарии ставятся перед коментируемой строкой
            var document = 
                (ksDocument3D)_kompas.ActiveDocument3D();
            // Новый компонент
            var part = 
                (ksPart)document.GetPart((short)Part_Type.pTop_Part);
            if (part == null) return;
            var entitySketch = 
                (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
            if (entitySketch == null) return;
            // Интерфейс свойств эскиза
            var sketchDef = 
                (ksSketchDefinition)entitySketch.GetDefinition();
            if (sketchDef == null) return;
            // Получим интерфейс базовой плоскости XOZ
            var basePlane = 
                (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
            if (basePlane == null) return;
            // Установим плоскость XOZ базовой для эскиза
            sketchDef.SetPlane(basePlane);
            // Создадим эскиз
            entitySketch.Create();          

            // Интерфейс редактора эскиза
            var sketchEdit = 
                (ksDocument2D)sketchDef.BeginEdit();
            if (sketchEdit != null)
            {
                // TODO: магические числа
                diameterStake /= 2;
                sketchEdit.ksLineSeg
                    (0, 0, diameterStake, 0, 1);
                sketchEdit.ksLineSeg
                    (0, 0, 0, -stakeHeight, 3);

                sketchEdit.ksLineSeg
                (0, -stakeHeight, diameterStake - 10.177112, 
                    -stakeHeight + 17.885819, 1);
                sketchEdit.ksLineSeg
                (diameterStake - 10.177112, -stakeHeight + 17.885819, 
                    diameterStake - 7.024134, -stakeHeight + 23.870603, 1);
                sketchEdit.ksLineSeg
                (diameterStake - 7.024134, -stakeHeight + 23.870603, 
                    diameterStake - 4, -stakeHeight + 32, 1);
                sketchEdit.ksLineSeg
                (diameterStake - 4, -stakeHeight + 32, 
                    diameterStake - 2.18061, -stakeHeight + 39.230625, 1);
                sketchEdit.ksLineSeg
                (diameterStake - 2.18061, -stakeHeight + 39.230625, 
                    diameterStake - 1.260657, -stakeHeight + 44.171475, 1);
                sketchEdit.ksLineSeg
                (diameterStake - 1.260657, -stakeHeight + 44.171475, 
                    diameterStake - 0.531802, -stakeHeight + 49.700599, 1);
                sketchEdit.ksLineSeg
                (diameterStake - 0.531802, -stakeHeight + 49.700599, 
                    diameterStake - 0.166451, -stakeHeight + 54.232637, 1);
                sketchEdit.ksLineSeg
                (diameterStake - 0.166451, -stakeHeight + 54.232637, 
                    diameterStake, 0, 1);

                // Завершение редактирования эскиза
                sketchDef.EndEdit();    
            }
            RotateExtrusion(entitySketch, false);
        }

		/// <summary>
		/// Метод построения зубцов кола
		/// </summary>
		// TODO: Методы должны начинаться с глагола
		public void BuildStakeTeeth(double count, double diameterStake, 
            double stakeHeight)
        {
            var document = 
                (ksDocument3D)_kompas.ActiveDocument3D();
            // TODO: комментарий перед вызываемой строкой.
            // Новый компонент
            var part = 
                (ksPart)document.GetPart((short)Part_Type.pTop_Part);  
            if (part == null) return;
            var entitySketchDisplaced = 
                (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
            // Создадим смещенную плоскость, а в ней эскиз
            var entityOffsetPlane2 = 
                (ksEntity)part.NewEntity((short)Obj3dType.o3d_planeOffset);
            if (entityOffsetPlane2 != null && 
                entitySketchDisplaced != null)
            {
                // Интерфейс свойств смещенной плоскости
                var offsetDef = 
                    (ksPlaneOffsetDefinition)entityOffsetPlane2.GetDefinition();
                if (offsetDef != null)
                {
                    // Расстояние от базовой плоскости
                    offsetDef.offset = 12;  
                    var basePlane = 
                        (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);

                    // Базовая плоскость
                    offsetDef.SetPlane(basePlane); 
                    entityOffsetPlane2.hidden = true;
                    // Создать смещенную плоскость 
                    entityOffsetPlane2.Create(); 

                    var sketchDef = 
                        (ksSketchDefinition)entitySketchDisplaced.GetDefinition();
                    if (sketchDef != null)
                    {
                        // Установим плоскость XOY базовой для эскиза
                        sketchDef.SetPlane(entityOffsetPlane2);
                        // Создадим эскиз
                        entitySketchDisplaced.Create(); 

                        // TODO: магические числа
                        // Интерфейс редактора эскиза
                        var sketchEdit = 
                            (ksDocument2D)sketchDef.BeginEdit();
                        sketchEdit.ksLineSeg
                            (diameterStake / 2 + 10, 9.54, 22.060387, 0.000961, 1);
                        sketchEdit.ksLineSeg
                            (22.060387, 0.000961, diameterStake / 2+10, -9.54, 1);
                        sketchEdit.ksLineSeg
                            (diameterStake / 2+10, -9.54, diameterStake / 2+10, 9.54, 1);
                        // Завершение редактирования эскиза
                        sketchDef.EndEdit();                 
                    }
                }
            }

            // Эскиз линии, которая в дальнейшем будет траекторией выреза по траектории
            var entitySketch = 
                (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
            // TODO: Большая вложенность
            if (entitySketch == null) return;
            {
                // Интерфейс свойств эскиза
                var sketchDef = 
                    (ksSketchDefinition)entitySketch.GetDefinition();
                if (sketchDef == null) return;
                // Получим интерфейс базовой плоскости XOZ
                // TODO: либо перенести все, что после равно на другую строку,
                // TODO: либо вместе с точкой перенести вызов метода
                var basePlane = 
                    (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
                if (basePlane == null) return;
                // Установим плоскость XOZ базовой для эскиза
                sketchDef.SetPlane(basePlane);
                // Создадим эскиз
                entitySketch.Create();          

                // Интерфейс редактора эскиза
                var sketchEdit = 
                    (ksDocument2D)sketchDef.BeginEdit();
                if (sketchEdit == null) return;
                // TODO: Магические числа
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
                // Завершение редактирования эскиза
                sketchDef.EndEdit();	

                // Вырез по траектории
                var entityCutEvolution = 
                    (ksEntity)part.NewEntity((short)Obj3dType.o3d_cutEvolution);
                if (entityCutEvolution == null) return;
                var cutEvolutionDef = 
                    (ksCutEvolutionDefinition)entityCutEvolution.GetDefinition();

                if (cutEvolutionDef != null)
                {
                    // Тонкая стенка в два направления
                    cutEvolutionDef.SetThinParam
                        (false, (short)Direction_Type.dtBoth, 0, 0);    
                    cutEvolutionDef.SetSketch(entitySketchDisplaced);
                    var iPathPartArray = 
                        (ksEntityCollection)cutEvolutionDef.PathPartArray();
                    iPathPartArray.Add(entitySketch);
                }

                // Создадим операцию вырезания по траектории
                entityCutEvolution.Create();    

                //Отверстия по концетрической сетке
                var circularCopyEntity = 
                    (ksEntity)part.NewEntity((short)Obj3dType.o3d_circularCopy);
                var circularCopyDefinition =
                    (ksCircularCopyDefinition)circularCopyEntity.GetDefinition();
                circularCopyDefinition.SetCopyParamAlongDir
                    (Convert.ToInt32(count), 360, true, false);
                var baseAxisOz = 
                    (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_axisOZ);
                circularCopyDefinition.SetAxis(baseAxisOz);
                var entityCollection = 
                    (ksEntityCollection)circularCopyDefinition.GetOperationArray();
                entityCollection.Add(cutEvolutionDef);
                circularCopyEntity.Create();
            }
        }

		/// <summary>
		/// Метод построения отверстий в тарелке
		/// </summary>
		// TODO: Методы должны начинаться с глагола
		public void BuildHolesInThePlate(double count, double diameterPlate, 
            double diameterStake)
        {
            var document = 
                (ksDocument3D)_kompas.ActiveDocument3D();
            // Новый компонент
            var part = 
                (ksPart)document.GetPart((short)Part_Type.pTop_Part);  
	        // TODO: большая вложенность
            if (part == null) return;
            var entitySketch = 
                (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
            if (entitySketch == null) return;
            // Интерфейс свойств эскиза
            var sketchDef = 
                (ksSketchDefinition)entitySketch.GetDefinition();
            if (sketchDef == null) return;
            // Получим интерфейс базовой плоскости XOY
            var basePlane = 
                (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);
            if (basePlane == null) return;
            // Установим плоскость XOZ базовой для эскиза
            sketchDef.SetPlane(basePlane);
            // Создадим эскиз
            entitySketch.Create();          

            // Интерфейс редактора эскиза
            var sketchEdit = 
                (ksDocument2D)sketchDef.BeginEdit();
            if (sketchEdit == null) return;
            // TODO магические числа
            sketchEdit.ksLineSeg
                (-0.75, -(diameterPlate/2-10.5), -0.75, -diameterStake/2-2, 1);
            sketchEdit.ksLineSeg
                (-0.75, -diameterStake/2 - 2, 0.75, -diameterStake/2 - 2, 1);
            sketchEdit.ksLineSeg
                (0.75, -diameterStake/2 - 2, 0.75, -(diameterPlate/2 - 10.5), 1);
            sketchEdit.ksLineSeg
                (0.75, -(diameterPlate/2 - 10.5), -0.75, -(diameterPlate/2 - 10.5), 1);
            // Завершение редактирования эскиза
            sketchDef.EndEdit();    

            //Вырезать выдавливанием
            var entityCutExtr = 
                (ksEntity)part.NewEntity((short)Obj3dType.o3d_cutExtrusion);
            if (entityCutExtr == null) return;
            var cutExtrDef = 
                (ksCutExtrusionDefinition)entityCutExtr.GetDefinition();
            if (cutExtrDef != null)
            {
                // Установим эскиз операции
                cutExtrDef.SetSketch(entitySketch);
                // Прямое направление
                cutExtrDef.directionType = 
                    (short)Direction_Type.dtBoth; 
                cutExtrDef.SetSideParam
                    (true, (short)End_Type.etBlind, 5, 0, true);
                cutExtrDef.SetThinParam(false, 0, 0, 0);
            }
            // Создадим операцию вырезание выдавливанием
            entityCutExtr.Create(); 

            //Отверстия по концетрической сетке
            var circularCopyEntity = 
                (ksEntity)part.NewEntity((short)Obj3dType.o3d_circularCopy);
            var circularCopyDefinition = 
                (ksCircularCopyDefinition)circularCopyEntity.GetDefinition();
            circularCopyDefinition.SetCopyParamAlongDir
                (Convert.ToInt32(count), 360, true, false);
            var baseAxisOz = 
                (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_axisOZ);
            circularCopyDefinition.SetAxis(baseAxisOz);
            var entityCollection = 
                (ksEntityCollection)circularCopyDefinition.GetOperationArray();
            entityCollection.Add(cutExtrDef);
            circularCopyEntity.Create();
        }
    }
}
