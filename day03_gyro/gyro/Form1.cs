using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using Draw3Dbox;

namespace gyro
{
    public partial class Form1 : Form
    {
        SerialPort Comport = new SerialPort();
        private delegate void SetTextDelegate(string getString);

        Cube cube;
        private float Xaxis = 0;
        private float Yaxis = 0;
        private float Zaxis = 0;

        public Form1()
        {
            InitializeComponent();
            Comport.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
        }

        private void DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string rxd = Comport.ReadTo("\n");
            this.BeginInvoke(new SetTextDelegate(SerialReceived), new object[] { rxd });
        }

        private void SerialReceived(string inString)
        {
            string Code = inString.Substring(0, 1);
            string Gyro = inString.Substring(1);

            if(Code == "@")
            {
                string[] Axis = Gyro.Split(',');

                Xaxis = (float)(Convert.ToInt16(Axis[0]) / 50.0);
                Yaxis = (float)(Convert.ToInt16(Axis[1]) / 50.0);
                Zaxis = (float)(Convert.ToInt16(Axis[2]) / 50.0);
                render();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbComport.Items.Clear();
            var portName = System.IO.Ports.SerialPort.GetPortNames();
            cmbComport.Items.AddRange(portName);
            cmbComport.SelectedIndex = cmbComport.Items.Count - 1;

            cube = new Cube(300, 30, 400);
            render();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Comport.IsOpen)
            {
                Comport.Close();
                Comport.Dispose();
                Comport = null;
            }
            Status.Text = "Form Closing";
        }

        private void render()
        {
            cube.RotateX = Xaxis;
            cube.RotateY = Yaxis;
            cube.RotateZ = Zaxis;

            Point origin = new Point(panel1.Width / 2, panel1.Height / 2);
            panel1.BackgroundImage = cube.drawCube(origin);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if(btnConnect.Text == "Connect")
            {
                if (Comport.IsOpen)
                {
                    Status.Text = "Already Used";
                }
                else
                {
                    Comport.PortName = cmbComport.Text;
                    Comport.BaudRate = 115200;
                    Comport.DataBits = 8;
                    Comport.Open();
                    Comport.DiscardInBuffer();
                    btnConnect.Text = "Close";
                    Status.Text = "Port Opened";
                }
            }
            else
            {
                Comport.Close();
                Status.Text = "Port Closed";
                btnConnect.Text = "Connect";
            }
        }
    }
}
