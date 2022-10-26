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
        /// Экземпляр класса ChangeableParametrs
        /// </summary>
        private ChangeableParametrs _changeableParametrs = new ChangeableParametrs();

        /// <summary>
        /// Переменная белого цвета
        /// </summary>  
        private Color _colorWhite = Color.White;

        /// <summary>
        /// Переменная розового цвета
        /// </summary>  
        private Color _colorLightPink = Color.LightPink;

        ///// <summary>
        ///// Словарь, cвязывающий параметр втулки и соотвествующий ему textbox
        ///// </summary>
        //private Dictionary<ParameterType, TextBox> _valueTextBox;

        /// <summary>
        /// Диаметер тарелки
        /// </summary>
        private double _plateDiameter;

        /// <summary>
        /// Диаметер кола
        /// </summary>
        private double _stakeDiameter;

        /// <summary>
        /// Высота кола
        /// </summary>
        private double _stakeHeight;

        /// <summary>
        /// Количество зубцов на коле
        /// </summary>
        private int _numberOfTeeth;

        /// <summary>
        /// Количество отвекрстий на тарелке
        /// </summary>
        private int _numberOfHoles;
        

        public MainForm()
        {
            InitializeComponent();

            //_valueTextBox = new Dictionary<ParameterType, TextBox>();
            //_valueTextBox.Clear();
            //_valueTextBox.Add(ParameterType.PlateDiameter, TextBoxPlateDiameter);
            //_valueTextBox.Add(ParameterType.StakeDiameter, TextBoxStakeDiameter);
            //_valueTextBox.Add(ParameterType.StakeHeight, TextBoxStakeHeight);
            //_valueTextBox.Add(ParameterType.NumberOfTeeth, TextBoxNumberOfTeeth);
            //_valueTextBox.Add(ParameterType.NumberOfHoles, TextBoxNumberOfHoles);
            //_valueTextBox.Add(TextBoxPlateDiameter,_changeableParametrs.PlateDiameter);
            //_valueTextBox.Add(TextBoxStakeDiameter, _changeableParametrs.StakeDiameter);
            //_valueTextBox.Add(TextBoxStakeHeight, _changeableParametrs.StakeHeight);
            ////_valueTextBox.Add(_numberOfHoles, TextBoxNumberOfTeeth);
            ////_valueTextBox.Add(_numberOfTeeth, TextBoxNumberOfHoles);
        }

        /// <summary>
        /// Обработчик нажатия на кнопку "Построить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonBuild_Click(object sender, EventArgs e)
        {
            if (TextBoxPlateDiameter.Text != "" ||
                TextBoxStakeDiameter.Text != "" ||
                TextBoxStakeHeight.Text != "" ||
                TextBoxNumberOfTeeth.Text != "" ||
                TextBoxNumberOfHoles.Text != "")
            {
                if (_changeableParametrs.parameters.Count == 0)
                {
                    JuicerBuilder juicerBuild = new JuicerBuilder();
                    juicerBuild.BuildJuicer(_plateDiameter,
                        _stakeDiameter, _stakeHeight, _numberOfHoles, _numberOfTeeth);
                }
                else
                {
                    MessageBox.Show("Фигура не может быть построена");
                }
            }
        }

        /// <summary>
        /// Валидация для текстбоксов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBoxValidator_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Focus();
            if (textBox.Text == "" || textBox.Text == ",")
            {
                textBox.Text = "";
                return;
            }
            if (textBox == TextBoxPlateDiameter)
            {
                try
                {
                    _changeableParametrs.PlateDiameter = double.Parse(textBox.Text);
                    _plateDiameter = double.Parse(TextBoxPlateDiameter.Text);
                    textBox.BackColor = _colorWhite;
                }
                catch (ArgumentOutOfRangeException exception)
                {
                    textBox.BackColor = _colorLightPink;
                }
            }

            if (textBox == TextBoxStakeDiameter)
            {
                try
                {
                    _changeableParametrs.StakeDiameter = double.Parse(textBox.Text);
                    _stakeDiameter = double.Parse(textBox.Text);
                    textBox.BackColor = _colorWhite;
                    LabelPlateDiametrRange.Text = $"{_changeableParametrs.StakeDiameter + 96}-226 мм";
                    LabelStakeHeightRange.Text = $"60-{_changeableParametrs.StakeDiameter - 10} мм";
                    TextBoxValidator_TextChanged(TextBoxPlateDiameter, e);
                    TextBoxValidator_TextChanged(TextBoxStakeHeight, e);
                }
                catch (ArgumentOutOfRangeException exception)
                {
                    TextBoxStakeDiameter.BackColor = _colorLightPink;
                }
            }

            if (textBox == TextBoxStakeHeight)
            {
                try
                {
                    _changeableParametrs.StakeHeight = double.Parse(textBox.Text);
                    _stakeHeight = double.Parse(textBox.Text);
                    textBox.BackColor = _colorWhite;
                }
                catch (ArgumentOutOfRangeException exception)
                {
                    textBox.BackColor = _colorLightPink;
                }
            }

            if (textBox == TextBoxNumberOfTeeth)
            {
                try
                {
                    _changeableParametrs.NumberOfTeeth = int.Parse(textBox.Text);
                    _numberOfTeeth = int.Parse(textBox.Text);
                    textBox.BackColor = _colorWhite;
                }
                catch (ArgumentException exception)
                {
                    textBox.BackColor = _colorLightPink;
                }
            }

            if (textBox == TextBoxNumberOfHoles)
            {
                try
                {
                    _changeableParametrs.NumberOfHoles = int.Parse(textBox.Text);
                    _numberOfHoles = int.Parse(textBox.Text);
                    textBox.BackColor = _colorWhite;
                }
                catch (ArgumentException exception)
                {
                    textBox.BackColor = _colorLightPink;
                }
            }
        }

        /// <summary>
        /// Проверка, чтобы textbox содержал только одну запятую и цифры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckForCommasAndNumbers_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsControl(e.KeyChar))
                && !(Char.IsDigit(e.KeyChar))
                && !((e.KeyChar == ',')
                && (((TextBox)sender).Text.IndexOf(",") == -1)
            ))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Проверка, чтобы textbox содержал только цифры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IntegerCheck_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(Char.IsControl(e.KeyChar))
                && !(Char.IsDigit(e.KeyChar))
                && !((e.KeyChar == ',')
                && (((TextBox)sender).Text.IndexOf(",") == 1)
            ))
            {
                e.Handled = true;
            }
        }

        ///// <summary>
        ///// Обработчик текстбокса диаметра тарелки
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void TextBoxPlateDiametr_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        _changeableParametrs.PlateDiameter = double.Parse(TextBoxPlateDiameter.Text);
        //        _plateDiameter = double.Parse(TextBoxPlateDiameter.Text);
        //        TextBoxPlateDiameter.BackColor = _colorWhite;
        //    }
        //    catch (ArgumentOutOfRangeException exception)

        //    {
        //        TextBoxPlateDiameter.BackColor = _colorLightPink;
        //    }
        //}

        ///// <summary>
        ///// Обработчик текстбокса Диаметра кола
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void TextBoxStakeDiametr_TextChanged(object sender, EventArgs e)
        //{
        //    if (TextBoxStakeDiameter.Text == "")
        //    {
        //        TextBoxStakeDiameter.BackColor = _colorLightPink;
        //        return;
        //    }
        //    try
        //    {
        //        _changeableParametrs.StakeDiameter = double.Parse(TextBoxStakeDiameter.Text);
        //        _stakeDiameter = double.Parse(TextBoxStakeDiameter.Text);
        //        TextBoxStakeDiameter.BackColor = _colorWhite;
        //        LabelPlateDiametrRange.Text = $"{_changeableParametrs.StakeDiameter + 96}-226 мм";
        //        LabelStakeHeightRange.Text = $"60-{_changeableParametrs.StakeDiameter - 10} мм";
        //        TextBoxPlateDiametr_TextChanged(sender, e);
        //        TextBoxStakeHeight_TextChanged(sender, e);
        //    }
        //    catch (ArgumentOutOfRangeException exception)
        //    {
        //        TextBoxStakeDiameter.BackColor = _colorLightPink;
        //    }
        //}

        //private void TextBoxStakeHeight_TextChanged(object sender, EventArgs e)
        //{
        //    if (TextBoxStakeHeight.Text == "")
        //    {
        //        TextBoxStakeHeight.BackColor = _colorLightPink;
        //        return;
        //    }
        //    try
        //    {
        //        _changeableParametrs.StakeHeight = double.Parse(TextBoxStakeHeight.Text);
        //        _stakeHeight = double.Parse(TextBoxStakeHeight.Text);
        //        TextBoxStakeHeight.BackColor = _colorWhite;
        //    }
        //    catch (ArgumentOutOfRangeException exception)
        //    {
        //        TextBoxStakeHeight.BackColor = _colorLightPink;
        //    }
        //}

        //private void TextBoxNumberOfTeeth_TextChanged(object sender, EventArgs e)
        //{
        //    if (TextBoxNumberOfTeeth.Text == "")
        //    {
        //        TextBoxNumberOfTeeth.BackColor = _colorLightPink;
        //        return;
        //    }
        //    try
        //    {
        //        _changeableParametrs.NumberOfTeeth = int.Parse(TextBoxNumberOfTeeth.Text);
        //        _numberOfTeeth = int.Parse(TextBoxNumberOfTeeth.Text);
        //        TextBoxNumberOfTeeth.BackColor = _colorWhite;
        //    }
        //    catch (ArgumentException exception)
        //    {
        //        TextBoxNumberOfTeeth.BackColor = _colorLightPink;
        //    }
        //}

        //private void TextBoxNumberOfHoles_TextChanged(object sender, EventArgs e)
        //{
        //    if (TextBoxNumberOfHoles.Text == "")
        //    {
        //        TextBoxNumberOfHoles.BackColor = _colorLightPink;
        //        return;
        //    }
        //    try
        //    {
        //        _changeableParametrs.NumberOfHoles = int.Parse(TextBoxNumberOfHoles.Text);
        //        _numberOfHoles = int.Parse(TextBoxNumberOfHoles.Text);
        //        TextBoxNumberOfHoles.BackColor = _colorWhite;
        //    }
        //    catch (ArgumentException exception)
        //    {
        //        TextBoxNumberOfHoles.BackColor = _colorLightPink;
        //    }
        //}
    }
}