using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using JuicerPluginBuild;
using System.Drawing;
using JuicerPluginParameters;

namespace JuicerPluginUI
{
    /// <summary>
    /// Класс для взаимодействия с формой
    /// </summary>
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

        /// <summary>
        /// Словарь, cвязывающий параметр соковыжималки 
        /// и соотвествующий ему textbox
        /// </summary>
        private Dictionary<TextBox, Action<double>> _valueTextBox
            = new Dictionary<TextBox, Action<double>>();

        public MainForm()
        {
            InitializeComponent();

            _valueTextBox = new Dictionary<TextBox, Action<double>>();
            _valueTextBox.Add(TextBoxPlateDiameter, (plateDiameter) 
                => _changeableParametrs.PlateDiameter = plateDiameter);
            _valueTextBox.Add(TextBoxStakeDiameter, (stakeDiameter) 
                => _changeableParametrs.StakeDiameter = stakeDiameter);
            _valueTextBox.Add(TextBoxStakeHeight, (stakeHeight) 
                => _changeableParametrs.StakeHeight = stakeHeight);
            _valueTextBox.Add(TextBoxNumberOfTeeth, (numberOfTeeth) 
                => _changeableParametrs.NumberOfTeeth = numberOfTeeth);
            _valueTextBox.Add(TextBoxNumberOfHoles, (numberOfHoles) 
                => _changeableParametrs.NumberOfHoles = numberOfHoles);
        }

        /// <summary>
        /// Обработчик нажатия на кнопку "Построить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonBuild_Click(object sender, EventArgs e)
        {
            if (TextBoxPlateDiameter.Text == string.Empty ||
                TextBoxStakeDiameter.Text == string.Empty ||
                TextBoxStakeHeight.Text == string.Empty ||
                TextBoxNumberOfTeeth.Text == string.Empty ||
                TextBoxNumberOfHoles.Text == string.Empty ||
                _changeableParametrs.Parameters.Count > 0)
            {
                MessageBox.Show("Модель не может быть построена!", "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                JuicerBuilder juicerBuild = new JuicerBuilder();
                juicerBuild.BuildJuicer(_changeableParametrs.PlateDiameter,
                    _changeableParametrs.StakeDiameter, 
                    _changeableParametrs.StakeHeight,
                    _changeableParametrs.NumberOfTeeth,
                    _changeableParametrs.NumberOfHoles);
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
            if (textBox.Text == string.Empty || textBox.Text == ",")
            {
                textBox.Text = string.Empty;
                return;
            }
            try
            {
                _valueTextBox[textBox](double.Parse(textBox.Text));
                textBox.BackColor = _colorWhite;
                if(textBox == TextBoxStakeDiameter)
                {
                    LabelPlateDiametrRange.Text = $"{_changeableParametrs.StakeDiameter + 96}-226 мм";
                    LabelStakeHeightRange.Text = $"60-{_changeableParametrs.StakeDiameter - 10} мм";
                    TextBoxValidator_TextChanged(TextBoxPlateDiameter, e);
                    TextBoxValidator_TextChanged(TextBoxStakeHeight, e);
                    TextBoxStakeDiameter.Focus();
                }
            }
            catch 
            {
                textBox.BackColor = _colorLightPink;
            }
        }

        /// <summary>
        /// Проверка, чтобы textbox содержал только одну запятую и цифры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckForCommasAndNumbers_KeyPress(object sender, KeyPressEventArgs e)
        {
	        if (!(char.IsControl(e.KeyChar))
	            && !(char.IsDigit(e.KeyChar))
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
			if (!(char.IsControl(e.KeyChar))
                && !(char.IsDigit(e.KeyChar))
                && !((e.KeyChar == ',')
                && (((TextBox)sender).Text.IndexOf(",") == 1)
            ))
            {
                e.Handled = true;
            }
        }
    }
}