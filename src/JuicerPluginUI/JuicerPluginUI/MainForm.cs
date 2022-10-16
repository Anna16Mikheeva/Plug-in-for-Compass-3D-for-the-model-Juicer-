using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KompasAPI7;
using Kompas6API5;
using Kompas6Constants;
using Kompas6Constants3D;
using System.Runtime.InteropServices;
using JuicerPluginBuild;
using JuicerPluginbuilder; //Поменять название тк название папки и проекта не соттветсвуют друг другу
using System.Drawing;

namespace JuicerPluginUI
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Экземпляр класса KompasWrapper
        /// </summary>
        KompasWrapper kompasWrapper = new KompasWrapper();

        /// <summary>
        /// Экземпляр класса ChangeableParametrs
        /// </summary>
        ChangeableParametrs cheangeableParametrs = new ChangeableParametrs();

        /// <summary>
        /// Переменная белого цвета
        /// </summary>  
        private Color _colorWhite = Color.White;

        /// <summary>
        /// Переменная розового цвета
        /// </summary>  
        private Color _colorLightPink = Color.LightPink;

        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработсик нажатия на кнопку "Построить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonBuild_Click(object sender, EventArgs e)
        {
            kompasWrapper.StartKompas();
            kompasWrapper.BuildingJuicer();
            JuicerBuild juicerBuild = new JuicerBuild();
            juicerBuild.BuildJuicer(kompasWrapper);
        }

        /// <summary>
        /// Обработчик текстбокса Диаметра тарелки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxPlateDiametr_TextChanged(object sender, EventArgs e)
        {
            if (TextBoxPlateDiameter.Text == "")
            {
                TextBoxPlateDiameter.BackColor = _colorLightPink;
                return;
            }
            try
            {
                cheangeableParametrs.PlateDiameter = double.Parse(TextBoxPlateDiameter.Text);
                TextBoxPlateDiameter.BackColor = _colorWhite;
            }
            catch (ArgumentOutOfRangeException exception)

            {
                TextBoxPlateDiameter.BackColor = _colorLightPink;
            }
        }

        /// <summary>
        /// Обработчик текстбокса Диаметра кола
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxStakeDiametr_TextChanged(object sender, EventArgs e)
        {
            if (TextBoxStakeDiameter.Text == "")
            {
                TextBoxStakeDiameter.BackColor = _colorLightPink;
                return;
            }
            try
            {
                cheangeableParametrs.StakeDiameter = double.Parse(TextBoxStakeDiameter.Text);
                TextBoxStakeDiameter.BackColor = _colorWhite;
                LabelPlateDiametrRange.Text = $"{cheangeableParametrs.StakeDiameter + 96}-226 мм";
                LabelStakeHeightRange.Text = $"60-{cheangeableParametrs.StakeDiameter - 10} мм";
                TextBoxPlateDiametr_TextChanged(sender, e);
                TextBoxStakeHeight_TextChanged(sender, e);
            }
            catch (ArgumentOutOfRangeException exception)
            {
                TextBoxStakeDiameter.BackColor = _colorLightPink;
            }
        }

        private void TextBoxStakeHeight_TextChanged(object sender, EventArgs e)
        {
            if (TextBoxStakeHeight.Text == "")
            {
                TextBoxStakeHeight.BackColor = _colorLightPink;
                return;
            }
            try
            {
                cheangeableParametrs.StakeHeight = double.Parse(TextBoxStakeHeight.Text);
                TextBoxStakeHeight.BackColor = _colorWhite;
            }
            catch (ArgumentOutOfRangeException exception)
            {
                TextBoxStakeHeight.BackColor = _colorLightPink;
            }
        }

        private void TextBoxNumberOfTeeth_TextChanged(object sender, EventArgs e)
        {
            if (TextBoxNumberOfTeeth.Text == "")
            {
                TextBoxNumberOfTeeth.BackColor = _colorLightPink;
                return;
            }
            try
            {
                cheangeableParametrs.NumberOfTeeth = int.Parse(TextBoxNumberOfTeeth.Text);
                TextBoxNumberOfTeeth.BackColor = _colorWhite;
            }
            catch (ArgumentException exception)
            {
                TextBoxNumberOfTeeth.BackColor = _colorLightPink;
            }
        }

        private void TextBoxNumberOfHoles_TextChanged(object sender, EventArgs e)
        {
            if (TextBoxNumberOfHoles.Text == "")
            {
                TextBoxNumberOfHoles.BackColor = _colorLightPink;
                return;
            }
            try
            {
                cheangeableParametrs.NumberOfHoles = int.Parse(TextBoxNumberOfHoles.Text);
                TextBoxNumberOfHoles.BackColor = _colorWhite;
            }
            catch (ArgumentException exception)
            {
                TextBoxNumberOfHoles.BackColor = _colorLightPink;
            }
        }
    }
}
// В сеттере при неправильном вводе выбрасываем исключение, в MainForm исключение перехватывается и поле окрашивается в красный
// Значение мин и макс скорее всего будут не нужны