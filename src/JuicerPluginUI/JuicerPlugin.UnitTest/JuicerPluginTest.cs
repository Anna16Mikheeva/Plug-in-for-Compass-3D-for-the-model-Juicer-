using System;
using JuicerPluginbuilder;
using NUnit.Framework;

namespace JuicerPlugin.UnitTest
{
    [TestFixture]
    class JuicerPluginBuildTest
    {
        private ChangeableParametrs _changeableParametrs;

        [SetUp]
        public void InitChangeableParametrs()
        {
            _changeableParametrs = new ChangeableParametrs();
        }

        [Test]
        [TestCase(Description = "Позитивный тест сеттера PlateDiameter")]
        public void Test_PlateDiameter_Set_CorrectValue()
        {
            var expected = 200;
            _changeableParametrs.PlateDiameter = expected;
            var actual = _changeableParametrs.PlateDiameter;
            Assert.AreEqual(expected, actual, "Значение должно входить в " +
                                              "диапазон от 166 до 226");

        }

        [Test]
        [TestCase(150, Description = "Негативный тест сеттера PlateDiameter")]
        [TestCase(230, Description = "Негативный тест сеттера PlateDiameter")]

        public void Test_PlateDiameter_Set_UnCorrectValue(double wrongPlateDiameter)
        {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
        {
            _changeableParametrs.PlateDiameter = wrongPlateDiameter;
         }, "Должно возникать исключение, если значение не входит в " +
            "диапазон от 166 до 226");
        }

        [Test]
        [TestCase(Description = "Позитивный тест сеттера StakeDiameter")]
        public void Test_StakeDiameter_Set_CorrectValue()
        {
            var expected = 100.96;
            _changeableParametrs.StakeDiameter = expected;
            var actual = _changeableParametrs.StakeDiameter;
            Assert.AreEqual(expected, actual, "Значение должно входить в " +
                                              "диапазон от 70 до 130");

        }

        [Test]
        [TestCase(60, Description = "Негативный тест сеттера StakeDiameter")]
        [TestCase(138.7, Description = "Негативный тест сеттера StakeDiameter")]

        public void Test_StakeDiameter_Set_UnCorrectValue(double wrongStakeDiameter)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    _changeableParametrs.StakeDiameter = wrongStakeDiameter;
                }, "Должно возникать исключение, если значение не входит в " +
                   "диапазон от 70 до 130");
        }

        //[Test]
        //[TestCase(Description = "Позитивный тест сеттера StakeHeight")]
        //public void Test_StakeHeight_Set_CorrectValue()
        //{
        //    var expected = 82;
        //    _changeableParametrs.StakeHeight = expected;
        //    var actual = _changeableParametrs.StakeHeight;
        //    Assert.AreEqual(expected, actual, "Значение должно входить в " +
        //                                      "диапазон от 60 до 120");
        //}

        [Test]
        [TestCase(58.4, Description = "Негативный тест сеттера StakeHeight")]
        [TestCase(178.14, Description = "Негативный тест сеттера StakeHeight")]

        public void Test_StakeHeight_Set_UnCorrectValue(double wrongStakeHeight)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    _changeableParametrs.StakeHeight = wrongStakeHeight;
                }, "Должно возникать исключение, если значение не входит в " +
                   "диапазон от 60 до 120");
        }

        [Test]
        [TestCase(Description = "Позитивный тест сеттера NumberOfTeeth")]
        public void Test_NumberOfTeeth_Set_CorrectValue()
        {
            var expected = 11;
            _changeableParametrs.NumberOfTeeth = expected;
            var actual = _changeableParametrs.NumberOfTeeth;
            Assert.AreEqual(expected, actual, "Значение должно входить в " +
                                              "диапазон от 10 до 12");

        }

        [Test]
        [TestCase(8, Description = "Негативный тест сеттера NumberOfTeeth")]
        [TestCase(15, Description = "Негативный тест сеттера NumberOfTeeth")]

        public void Test_NumberOfTeeth_Set_UnCorrectValue(double wrongNumberOfTeeth)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                {
                    _changeableParametrs.NumberOfTeeth = wrongNumberOfTeeth;
                }, "Должно возникать исключение, если значение не входит в " +
                   "диапазон от 10 до 12");
        }
    }
}

