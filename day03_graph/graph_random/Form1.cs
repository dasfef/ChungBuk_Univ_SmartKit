using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace graph_random
{
    public partial class Form1 : Form
    {
        int RndValue;
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        //private void DataGeneration()
        //{
        //    RndValue = rnd.Next(100);
        //    chart1.Series["Series1"].Points.Add(RndValue);

        //    RndValue = rnd.Next(100);
        //    chart1.Series["Series2"].Points.Add(RndValue);
        //}

        // === Strip Chart ===
        //private void DataGeneration()
        //{
        //    RndValue = rnd.Next(100);
        //    chart1.Series["Series1"].Points.Add(RndValue);
        //    if (chart1.Series["Series1"].Points.Count > 50)
        //    {
        //        chart1.Series["Series1"].Points.RemoveAt(0);
        //    }
        //}

        // === 심전도기 ===
        private void DataGeneration()
        {
            RndValue = rnd.Next(100);
            chart1.Series["Series1"].Points.Add(RndValue);

            for(int i = 0; i <= 50; i++)
            {
                if (chart1.Series["Series1"].Points.Count > 50)
                {
                    chart1.Series["Series1"].Points[0].SetValueY(RndValue);
                    chart1.Series["Series1"].point
                }
            }
            
        }

        // === Scope Chart ===
        //private void DataGeneration()
        //{
        //    RndValue = rnd.Next(100);
        //    chart1.Series["Series1"].Points.Add(RndValue);
        //    if (chart1.Series["Series1"].Points.Count > 50)
        //    {
        //        chart1.Series["Series1"].Points.Clear();
        //    }
        //}

        private void timer1_Tick(object sender, EventArgs e)
        {
            DataGeneration();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            if(timer1.Enabled == true)
            {
                timer1.Stop();
                btnRun.Text = "RUN";
            }
            else
            {
                btnRun.Text = "STOP";
                timer1.Start();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = 100;
            timer1.Stop();
        }
    }
}
