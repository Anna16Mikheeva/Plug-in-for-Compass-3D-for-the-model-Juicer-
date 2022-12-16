using System;
using JuicerPluginParameters;
using NUnit.Framework;

namespace JuicerPlugin.UnitTest
{
    [TestFixture]
    class JuicerPluginBuildTest
    {
        /// <summary>
        /// Объект класса ChangeableParametrs
        /// </summary>
        private ChangeableParametrs _changeableParametrs;
        
        [TestCase(Description = "Позитивный тест геттера PlateDiameter")]
        public void Test_PlateDiameter_Get_CorrectValue()
        {
            _changeableParametrs = new ChangeableParametrs();
            var expected = 200;
            _changeableParametrs.PlateDiameter = expected;
            var actual = _changeableParametrs.PlateDiameter;
            Assert.AreEqual(expected, actual, "Значение должно входить в " +
                                              "диапазон от 166 до 226");

        }
        
        [TestCase(170, Description = "Позитивный тест сеттера PlateDiameter")]
        public void Test_PlateDiameter_Set_CorrectValue(double value)
        {
            _changeableParametrs = new ChangeableParametrs();
            _changeableParametrs.PlateDiameter = 170;
            Assert.AreEqual(value, _changeableParametrs.PlateDiameter, 
                "Значение должно входить в диапазон от 166 до 226");
        }

        [TestCase(150, Description = "Негативный тест сеттера PlateDiameter")]
        [TestCase(230, Description = "Негативный тест сеттера PlateDiameter")]

        public void Test_PlateDiameter_Set_UnCorrectValue(double wrongPlateDiameter)
        {
            _changeableParametrs = new ChangeableParametrs();
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _changeableParametrs.PlateDiameter = wrongPlateDiameter;
            }, "Должно возникать исключение, если значение не входит в " +
            "диапазон от 166 до 226");
        }
        
        [TestCase(170, Description = "Негативный тест сеттера PlateDiameter")]
        public void Test_PlateDiameter_Set_UnCorrectValueAddiction(double wrongPlateDiameter)
        {
            _changeableParametrs = new ChangeableParametrs();
            _changeableParametrs.StakeDiameter = 125;
            Assert.Throws<Exception>(() =>
                {
                    _changeableParametrs.PlateDiameter = wrongPlateDiameter;
                }, "Диаметр тарелки должен быть не менее, чем на 96мм больше диаметра кола");
        }
        
        [TestCase(Description = "Позитивный тест геттера StakeDiameter")]
        public void Test_StakeDiameter_Get_CorrectValue()
        {
            _changeableParametrs = new ChangeableParametrs();
            var expected = 100.96;
            _changeableParametrs.StakeDiameter = expected;
            var actual = _changeableParametrs.StakeDiameter;
            Assert.AreEqual(expected, actual, "Значение должно входить в " +
                                              "диапазон от 70 до 130");

        }
        
        [TestCase(100, Description = "Позитивный тест сеттера StakeDiameter")]
        public void Test_StakeDiameter_Set_CorrectValue(double value)
        {
            _changeableParametrs = new ChangeableParametrs();
            _changeableParametrs.StakeDiameter = 100;
            Assert.AreEqual(value, _changeableParametrs.StakeDiameter, 
                "Значение должно входить в диапазон от 70 до 130");

        }
        
        [TestCase(47, Description = "Негативный тест сеттера StakeDiameter")]
        [TestCase(158, Description = "Негативный тест сеттера StakeDiameter")]
        public void Test_StakeDiameter_Set_UnCorrectValue(double wrongStakeDiameter)
        {
            _changeableParametrs = new ChangeableParametrs();
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    _changeableParametrs.StakeDiameter = wrongStakeDiameter;
                }, "Должно возникать исключение, если значение не входит в " +
                   "диапазон от 70 до 130");
        }
        
        [TestCase(Description = "Позитивный тест геттера StakeHeight")]
        public void Test_StakeHeight_Get_CorrectValue()
        {
            _changeableParametrs = new ChangeableParametrs();
            _changeableParametrs.StakeDiameter = 72;
            var expected = 61;
            _changeableParametrs.StakeHeight = expected;
            var actual = _changeableParametrs.StakeHeight;
            Assert.AreEqual(expected, actual, "Значение должно входить в " +
                                              "диапазон от 60 до 120");
        }
        
        [TestCase(61, Description = "Позитивный тест сеттера StakeHeight")]
        public void Test_StakeHeight_Set_CorrectValue(double value)
        {
            _changeableParametrs = new ChangeableParametrs();
            _changeableParametrs.StakeDiameter = 115;
            _changeableParametrs.StakeHeight = 61;
            Assert.AreEqual(value, _changeableParametrs.StakeHeight, 
                "Значение должно входить в диапазон от 60 до 120");
        }
        
        [TestCase(110, Description = "Негативный тест сеттера StakeHeight")]
        public void Test_StakeHeight_Set_UnCorrectValueAddiction(double wrongStakeHeight)
        {
            _changeableParametrs = new ChangeableParametrs();
            _changeableParametrs.StakeDiameter = 105;
            Assert.Throws<Exception>(() =>
            {
                _changeableParametrs.StakeHeight = wrongStakeHeight;
            }, "Диаметр кола должен быть не менее, чем на 10мм больше высоты кола");
        }
        
        [TestCase(58.4, Description = "Негативный тест сеттера StakeHeight")]
        [TestCase(178.14, Description = "Негативный тест сеттера StakeHeight")]
        public void Test_StakeHeight_Set_UnCorrectValue(double wrongStakeHeight)
        {
            _changeableParametrs = new ChangeableParametrs();
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    _changeableParametrs.StakeHeight = wrongStakeHeight;
                }, "Должно возникать исключение, если значение не входит в " +
                   "диапазон от 60 до 120");
        }
        
        [TestCase(Description = "Позитивный тест геттера NumberOfTeeth")]
        public void Test_NumberOfTeeth_Get_CorrectValue()
        {
            _changeableParametrs = new ChangeableParametrs();
            var expected = 12;
            _changeableParametrs.NumberOfTeeth = expected;
            var actual = _changeableParametrs.NumberOfTeeth;
            Assert.AreEqual(expected, actual, "Значение должно входить в " +
                                              "диапазон от 10 до 12");

        }
        
        [TestCase(11, Description = "Позитивный тест сеттера NumberOfTeeth")]
        public void Test_NumberOfTeeth_Set_CorrectValue(double value)
        {
            _changeableParametrs = new ChangeableParametrs();
            _changeableParametrs.NumberOfTeeth = 11;
            Assert.AreEqual(value, _changeableParametrs.NumberOfTeeth, 
                "Значение должно входить в диапазон от 10 до 12");

        }
        
        [TestCase(8, Description = "Негативный тест сеттера NumberOfTeeth")]
        [TestCase(15, Description = "Негативный тест сеттера NumberOfTeeth")]
        public void Test_NumberOfTeeth_Set_UnCorrectValue(double wrongNumberOfTeeth)
        {
            _changeableParametrs = new ChangeableParametrs();
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    _changeableParametrs.NumberOfTeeth = wrongNumberOfTeeth;
                }, "Должно возникать исключение, если значение не входит в " +
                   "диапазон от 10 до 12");
        }
        
        [TestCase(Description = "Позитивный тест геттера NumberOfTeeth")]
        public void Test_NumberOfHoles_Get_CorrectValue()
        {
            _changeableParametrs = new ChangeableParametrs();
            var expected = 93;
            _changeableParametrs.NumberOfHoles = expected;
            var actual = _changeableParametrs.NumberOfHoles;
            Assert.AreEqual(expected, actual, "Значение должно входить в " +
                                              "диапазон от 90 до 100");
        }
        
        [TestCase(95, Description = "Позитивный тест сеттера NumberOfTeeth")]
        public void Test_NumberOfHoles_Set_CorrectValue(double value)
        {
            _changeableParametrs = new ChangeableParametrs();
            _changeableParametrs.NumberOfHoles = 95;
            Assert.AreEqual(value, _changeableParametrs.NumberOfHoles, 
                "Значение должно входить в диапазон от 90 до 100");
        }
        
        [TestCase(58, Description = "Негативный тест сеттера NumberOfHoles")]
        [TestCase(200, Description = "Негативный тест сеттера NumberOfHoles")]

        public void Test_NumberOfHoles_Set_UnCorrectValue(double wrongNumberOfHoles)
        {
            _changeableParametrs = new ChangeableParametrs();
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    _changeableParametrs.NumberOfHoles = wrongNumberOfHoles;
                }, "Должно возникать исключение, если значение не входит в " +
                   "диапазон от 90 до 100");
        }


        [TestCase(Description = "Позитивный тест геттера LengthOfHoles")]
        public void Test_LengthOfHoles_Get_CorrectValue()
        {
            _changeableParametrs = new ChangeableParametrs();
            var expected = 25.6;
            _changeableParametrs.LengthOfHoles = expected;
            var actual = _changeableParametrs.LengthOfHoles;
            Assert.AreEqual(expected, actual, "Значение должно входить в " +
                                              "диапазон от 16 до 35.5");
        }

        [TestCase(19.83, Description = "Позитивный тест сеттера LengthOfHoles")]
        public void Test_LengthOfHoles_Set_CorrectValue(double value)
        {
            _changeableParametrs = new ChangeableParametrs();
            _changeableParametrs.LengthOfHoles = 19.83;
            Assert.AreEqual(value, _changeableParametrs.LengthOfHoles,
                "Значение должно входить в диапазон от 16 до 35.5");
        }

        [TestCase(10, Description = "Негативный тест сеттера LengthOfHoles")]
        [TestCase(102.3, Description = "Негативный тест сеттера LengthOfHoles")]

        public void Test_LengthOfHoles_Set_UnCorrectValue(double wrongLengthOfHoles)
        {
            _changeableParametrs = new ChangeableParametrs();
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    _changeableParametrs.LengthOfHoles = wrongLengthOfHoles;
                }, "Должно возникать исключение, если значение не входит в " +
                   "диапазон от 16 до 35.5");
        }
    }
}

