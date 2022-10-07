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
        public KompasWrapper kompasWrapper;
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            kompasWrapper = new KompasWrapper();
            kompasWrapper.StartKompas();
            kompasWrapper.BuildingJuicer();
        }
    }
}
