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
        /// Начало координат
        /// /// </summary>
        private const int origin = 0;

        /// <summary>
        /// Стиль линий, где 1 - основная, 3 -вспомогательная
        /// </summary>
        private int[] styleLine = new int[2] { 1, 3 };

        /// <summary>
        /// Угол поворота
        /// </summary>
        const int degreeOfRotation = 360;

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
	        if (part == null)
	        {
		        return;
	        }

            var entitySketch = 
                (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
            if (entitySketch == null)
            {
	            return;
            }

            // Интерфейс свойств эскиза
            var sketchDef = 
                (ksSketchDefinition)entitySketch.GetDefinition();
            if (sketchDef == null)
            {
	            return;
            }

            // Получим интерфейс базовой плоскости XOZ
            var basePlane = 
                (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
            if (basePlane == null)
            {
	            return;
            }

            // Установим плоскость XOZ базовой для эскиза
            sketchDef.SetPlane(basePlane);
            // Создадим эскиз
            entitySketch.Create();          

            // интерфейс редактора эскиза
            var sketchEdit = 
                (ksDocument2D)sketchDef.BeginEdit();
            if (sketchEdit != null)
            {
                // Половина диаметра
                diameterPlate /= 2;
                // Радиусы дуг для скруглений
                int[] radiusArc = new int[3] {2, 3, 10};
                // Высота оси
                const int heightAxis = -22;
                // Координата y2 для дуги с радиусом 2
                const int y2ForArcWithRAdius2 = -18;
                // Координата x1 для дуги с радиусом 2
                const int x1ForArcWithRAdius2 = -20;
                // Массив сдвигов
                int[] shift = new int[5] { 3, 8, 10, 6, 1 };

                //Построение первого эскиза (тарелки)
                // TODO: Магические числа +
                sketchEdit.ksLineSeg
                    (origin, origin, diameterPlate - shift[2], origin, styleLine[0]);
                sketchEdit.ksLineSeg
                    (diameterPlate, -radiusArc[2], diameterPlate, 
                        y2ForArcWithRAdius2 + shift[4], styleLine[0]);
                sketchEdit.ksLineSeg
                    (diameterPlate + shift[0], x1ForArcWithRAdius2, 
                        diameterPlate + shift[3], x1ForArcWithRAdius2, styleLine[0]);
                sketchEdit.ksLineSeg
                    (diameterPlate + shift[1], y2ForArcWithRAdius2, 
                        diameterPlate + shift[1], y2ForArcWithRAdius2 + shift[4], styleLine[0]);

                //Ось
                sketchEdit.ksLineSeg(origin, origin, origin, heightAxis, styleLine[1]);

                //Радиусы
                sketchEdit.ksArcByPoint
                (diameterPlate + shift[3], y2ForArcWithRAdius2, 
                    radiusArc[0], diameterPlate + shift[3], x1ForArcWithRAdius2,
                    diameterPlate + shift[1], y2ForArcWithRAdius2, 1, styleLine[0]);
                sketchEdit.ksArcByPoint
                (diameterPlate + shift[0], y2ForArcWithRAdius2 + shift[4], 
                    radiusArc[1], diameterPlate + shift[0],
                    x1ForArcWithRAdius2, diameterPlate, 
                    y2ForArcWithRAdius2 + shift[4], -1, styleLine[0]);
                sketchEdit.ksArcByPoint
                (diameterPlate - radiusArc[2], -radiusArc[2], 
                    radiusArc[2], diameterPlate, 
                    -radiusArc[2], diameterPlate - shift[2], 
                    origin, 1, styleLine[0]);
                // TODO: все комментарии ставятся перед коментируемой строкой +
                // Завершение редактирования эскиза
                sketchDef.EndEdit();    
            }
            RotateExtrusion(entitySketch, true);
        }

		/// <summary>
		/// Метод операции выдавливания вращением
		/// </summary>
		public void RotateExtrusion(ksEntity entitySketch, bool thinWallElement)
        {

            var document = (ksDocument3D)_kompas.ActiveDocument3D();
            // Новый компонент
			var part = 
                (ksPart)document.GetPart((short)Part_Type.pTop_Part);

            // Вращение
            var entityRotate = 
                (ksEntity)part.NewEntity((short)Obj3dType.o3d_bossRotated);
            if (entityRotate == null)
            {
                return;
            }
            // Интерфейс базовой операции вращения
            var rotateDef = 
                (ksBossRotatedDefinition)entityRotate.GetDefinition(); 
            if (rotateDef == null)
            {
                return;
            }
            var rotproperty = 
                (ksRotatedParam)rotateDef.RotatedParam();
            if (rotproperty != null)
            {
                rotproperty.direction = 
                    (short)Direction_Type.dtBoth;
                // Тороид
                rotproperty.toroidShape = true; 
            }

            // Толщина 
            int[] thickness = new int[2] { 0, 2 };

            // Тонкая стенка в два направления
            rotateDef.SetThinParam
                (thinWallElement, (short)Direction_Type.dtBoth, 
                    thickness[1], thickness[0]);   
            rotateDef.SetSketch(entitySketch);
            rotateDef.SetSideParam(true, degreeOfRotation);
            // Эскиз операции вращения
            rotateDef.SetSketch(entitySketch);
            // TODO: все комментарии ставятся перед коментируемой строкой +
            // Создать операцию
            entityRotate.Create();
        }


        /// <summary>
        /// Метод построения кола
        /// </summary>
        public void BuildStake(double diameterStake, double stakeHeight)
        {
            var document =
                (ksDocument3D)_kompas.ActiveDocument3D();
            // Новый компонент
            var part =
                (ksPart)document.GetPart((short)Part_Type.pTop_Part);
            // TODO: RSDN +
            if (part == null) 
            {
                return;
            }
            var entitySketch = 
                (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
            if (entitySketch == null)
            {
                return;
            }
            // Интерфейс свойств эскиза
            var sketchDef = 
                (ksSketchDefinition)entitySketch.GetDefinition();
            if (sketchDef == null)
            {
                return;
            }
            // Получим интерфейс базовой плоскости XOZ
            var basePlane = 
                (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
            if (basePlane == null)
            {
                return;
            }
            // Установим плоскость XOZ базовой для эскиза
            sketchDef.SetPlane(basePlane);
            // Создадим эскиз
            entitySketch.Create();          

            // Интерфейс редактора эскиза
            var sketchEdit = 
                (ksDocument2D)sketchDef.BeginEdit();
            if (sketchEdit != null)
            {
                //Сдвиг
                double[] shift = new double[14] { 10.177112 , 17.885819, 7.024134, 
                    23.870603, 4, 32,  2.18061, 39.230625, 1.260657, 44.171475, 0.531802,
                49.700599, 0.166451, 54.232637};
                
                diameterStake /= 2;
                sketchEdit.ksLineSeg
                    (origin, origin, diameterStake, origin, styleLine[0]);
                sketchEdit.ksLineSeg
                    (origin, origin, origin, -stakeHeight, styleLine[1]);

                sketchEdit.ksLineSeg
                (origin, -stakeHeight, diameterStake - shift[0], 
                    -stakeHeight + shift[1], styleLine[0]);
                // TODO: обернуть в цикл +
                for (int i=0; i < 11; i+=2)
                {
                    sketchEdit.ksLineSeg
                (diameterStake - shift[i], -stakeHeight + shift[i+1],
                    diameterStake - shift[i+2], -stakeHeight + shift[i+3], styleLine[0]);
                }
                sketchEdit.ksLineSeg
                (diameterStake - shift[12], -stakeHeight + shift[13], 
                    diameterStake, origin, styleLine[0]);

                // Завершение редактирования эскиза
                sketchDef.EndEdit();    
            }
            RotateExtrusion(entitySketch, false);
        }

		/// <summary>
		/// Метод построения зубцов кола
		/// </summary>
		public void BuildStakeTeeth(double count, double diameterStake, 
            double stakeHeight)
        {
            var document = 
                (ksDocument3D)_kompas.ActiveDocument3D();
            // Новый компонент
            var part = 
                (ksPart)document.GetPart((short)Part_Type.pTop_Part);  
            if (part == null)
            {
                return;
            }
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
                        // Радиус диаметра кола
                        diameterStake /= 2;
                        // Сдвиг по координатам
                        const int shift = 10;
                        // Координаты треугольника
                        double[] triangleCootdinates = new double[4] 
                        { 9.54 , 22.060387, 0.000961, 22.060387 };
 
                        // TODO: магические числа +
                        // Интерфейс редактора эскиза
                        var sketchEdit = 
                            (ksDocument2D)sketchDef.BeginEdit();
                        sketchEdit.ksLineSeg
                            (diameterStake + shift, triangleCootdinates[0], 
                            triangleCootdinates[3], triangleCootdinates[2], styleLine[0]);
                        sketchEdit.ksLineSeg
                            (triangleCootdinates[3], triangleCootdinates[2], 
                            diameterStake + shift, -triangleCootdinates[0], styleLine[0]);
                        sketchEdit.ksLineSeg
                            (diameterStake + shift, -triangleCootdinates[0], 
                            diameterStake + shift, triangleCootdinates[0], styleLine[0]);
                        // Завершение редактирования эскиза
                        sketchDef.EndEdit();                 
                    }
                }
            }

            // Эскиз линии, которая в дальнейшем будет траекторией выреза по траектории
            var entitySketch = 
                (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
            if (entitySketch == null)
            {
                return;
            }
            {
                // Интерфейс свойств эскиза
                var sketchDef = 
                    (ksSketchDefinition)entitySketch.GetDefinition();
                if (sketchDef == null)
                {
                    return;
                }
                // Получим интерфейс базовой плоскости XOZ
                var basePlane = 
                    (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_planeXOZ);
                if (basePlane == null)
                {
                    return;
                }
                // Установим плоскость XOZ базовой для эскиза
                sketchDef.SetPlane(basePlane);
                // Создадим эскиз
                entitySketch.Create();          

                // Интерфейс редактора эскиза
                var sketchEdit = 
                    (ksDocument2D)sketchDef.BeginEdit();
                if (sketchEdit == null)
                {
                    return;
                }
                // TODO: Магические числа +
                // Массив сдвигов по координатам
                double[] shift = new double[2] { 11.995037, 1.020943 };
                // Массив координат для линии,
                // которая будет являться траекторией
                // для выреза зубца
                double[] trajectoryCoordinates = new double[14] 
                { 2.290188, -stakeHeight - shift[0], 9.259353, -stakeHeight - shift[1], 13.435794, -53.535092, 16.39727,
                -45.132963, 19.282811, -33.108725, 20.963563, 
                -20.933876, 22.060387, -12.000961};

	            // TODO: обернуть в цикл +
                for(int i=0;i<11;i+=2)
                {
                    sketchEdit.ksLineSeg
                    (trajectoryCoordinates[i], trajectoryCoordinates[i+1],
                    trajectoryCoordinates[i+2], trajectoryCoordinates[i+3], styleLine[0]);
                }
                // Завершение редактирования эскиза
                sketchDef.EndEdit();	

                // Вырез по траектории
                var entityCutEvolution = 
                    (ksEntity)part.NewEntity((short)Obj3dType.o3d_cutEvolution);
                if (entityCutEvolution == null)
                {
                    return;
                }
                var cutEvolutionDef = 
                    (ksCutEvolutionDefinition)entityCutEvolution.GetDefinition();

                if (cutEvolutionDef != null)
                {
                    // Толщина выреза
                    const int thickness = 0;
                    // Тонкая стенка в два направления
                    cutEvolutionDef.SetThinParam
                        (false, (short)Direction_Type.dtBoth, thickness, thickness);    
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
                    (Convert.ToInt32(count), degreeOfRotation, true, false);
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
		public void BuildHolesInThePlate(double count,
            double diameterStake, double lengthOfHoles)
        {
            var document = 
                (ksDocument3D)_kompas.ActiveDocument3D();
            // Новый компонент
            var part = 
                (ksPart)document.GetPart((short)Part_Type.pTop_Part);
            // TODO: RSDN +
            if (part == null)
            { 
                return; 
            }
            var entitySketch = 
                (ksEntity)part.NewEntity((short)Obj3dType.o3d_sketch);
            if (entitySketch == null)
            {
                return;
            }
            // Интерфейс свойств эскиза
            var sketchDef = 
                (ksSketchDefinition)entitySketch.GetDefinition();
            if (sketchDef == null)
            { 
                return; 
            }
            // Получим интерфейс базовой плоскости XOY
            var basePlane = 
                (ksEntity)part.GetDefaultEntity((short)Obj3dType.o3d_planeXOY);
            if (basePlane == null)
            {
                return;
            }
            // Установим плоскость XOZ базовой для эскиза
            sketchDef.SetPlane(basePlane);
            // Создадим эскиз
            entitySketch.Create();          

            // Интерфейс редактора эскиза
            var sketchEdit = 
                (ksDocument2D)sketchDef.BeginEdit();
            if (sketchEdit == null)
            {
                return;
            }
            // Радиус кола со сдвигом
            diameterStake = -diameterStake/2-2;
            // Общая координата
            const double sharedCoordinate = 0.75;

	        // TODO: можно попробовать обернуть в цикл
            sketchEdit.ksLineSeg
                (-sharedCoordinate, diameterStake-lengthOfHoles, 
                -sharedCoordinate, diameterStake, styleLine[0]);
            sketchEdit.ksLineSeg
                (-sharedCoordinate, diameterStake, 
                sharedCoordinate, diameterStake, styleLine[0]);
            sketchEdit.ksLineSeg
                (sharedCoordinate, diameterStake, 
                sharedCoordinate, diameterStake - lengthOfHoles, styleLine[0]);
            sketchEdit.ksLineSeg
                (sharedCoordinate, diameterStake - lengthOfHoles, 
                -sharedCoordinate, diameterStake - lengthOfHoles, styleLine[0]);
            // Завершение редактирования эскиза
            sketchDef.EndEdit();    

            //Вырезать выдавливанием
            var entityCutExtr = 
                (ksEntity)part.NewEntity((short)Obj3dType.o3d_cutExtrusion);
            if (entityCutExtr == null)
            {
                return;
            }
            var cutExtrDef = 
                (ksCutExtrusionDefinition)entityCutExtr.GetDefinition();
            if (cutExtrDef != null)
            {
                // Глубина выреза
                const int thickness = 5;
                // Установим эскиз операции
                cutExtrDef.SetSketch(entitySketch);
                // Прямое направление
                cutExtrDef.directionType = 
                    (short)Direction_Type.dtBoth; 
                cutExtrDef.SetSideParam
                    (true, (short)End_Type.etBlind, thickness, 0, true);
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
                (Convert.ToInt32(count), degreeOfRotation, true, false);
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
