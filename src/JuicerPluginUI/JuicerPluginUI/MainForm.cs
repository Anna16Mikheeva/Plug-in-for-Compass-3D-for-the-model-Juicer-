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

namespace JuicerPluginUI
{
    public partial class MainForm : Form
    {
        private KompasWrapper kompasWrapper = new KompasWrapper();

        public MainForm()
        {
            InitializeComponent();
        }

        private void ButtonBuild_Click(object sender, EventArgs e)
        {
            kompasWrapper.StartKompas();
            kompasWrapper.BuildingJuicer();
        }

        private void TextBoxPlateDiametr_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBoxStakeDiametr_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBoxStakeHeight_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBoxNumberOfTeeth_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBoxNumberOfHoles_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
// В сеттере при неправильном вводе выбрасываем исключение, в MainForm исключение перехватывается и поле окрашивается в красный
// Значение мин и макс скорее всего будут не нужны