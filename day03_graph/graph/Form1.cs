using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace graph
{
    public partial class Form1 : Form
    {
        double SinValue;
        double CosValue;

        public Form1()
        {
            InitializeComponent();
        }

        private void DataGeneration()
        {
            for(int k = 0; k < 360; k++)
            {
                SinValue = Math.Sin(k * (3.141692 / 180));
                chart1.Series["Series1"].Points.Add(SinValue);

                CosValue = Math.Cos(k * (3.141692 / 180));
                chart1.Series["Series2"].Points.Add(CosValue);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            DataGeneration();
        }
    }
}
