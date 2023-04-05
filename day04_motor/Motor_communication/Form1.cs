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

namespace Motor_communication
{
    public partial class Form1 : Form
    {
        SerialPort Comport = new SerialPort();
        private delegate void SetTextDelegate(string getString);

        String Motion = "";
        String SetMotion = "1";
        int Speed = 0;
        int SetSpeed = 0;

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
            try
            {
                string Head = inString.Substring(0, 1);
                string Data = inString.Substring(1, inString.Length - 1);

                if(Head == "@")
                {
                    string[] parsingData = Data.Split(',');

                    Motion = parsingData[0];
                    Speed = Convert.ToInt16(parsingData[1]);
                    Status(Motion, Speed);
                }
            }
            catch{            }
        }

        private void SerialWrite(string motion, int speed)
        {
            if (Comport.IsOpen)
            {
                string msg = "@" + motion + "," + speed.ToString() + "\n";
                Comport.Write(msg);
            }
        }

        private void Status(string motion, int speed)
        {
            switch(motion)
            {
                case "0":
                    lblMotion.Text = "STOP"; lblMotion.BackColor = Color.DarkGray; break;
                case "1":
                    lblMotion.Text = "CW"; lblMotion.BackColor = Color.Orange; break;
                case "2":
                    lblMotion.Text = "CCW"; lblMotion.BackColor = Color.YellowGreen; break;
                default:
                    lblMotion.Text = "UNKNOWN"; lblMotion.BackColor = Color.Red; break;
            }

            progressBar1.Value = speed;
            lblSpeed.Text = speed.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbPort.Items.Clear();
            var portName = System.IO.Ports.SerialPort.GetPortNames();
            cmbPort.Items.AddRange(portName);
            cmbPort.SelectedIndex = cmbPort.Items.Count - 1;

            cmbRate.Items.Clear();
            cmbRate.Items.Add("9600");
            cmbRate.Items.Add("19200");
            cmbRate.Items.Add("57600");
            cmbRate.Items.Add("115200");
            cmbRate.SelectedIndex = 0;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Comport.IsOpen)
            {
                SerialWrite("0", 0);
                Comport.Close();
            }
            Comport.Dispose();
            Comport = null;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (btnOpen.Text == "Connect")
            {
                Comport.PortName = cmbPort.Text;
                Comport.BaudRate = Convert.ToInt32(cmbRate.Text);
                Comport.DataBits = 8;
                Comport.Open();
                Comport.DiscardInBuffer();
                btnOpen.Text = "Disconnect";
            }
            else
            {
                SerialWrite("0", 0);

                Comport.Close();
                btnOpen.Text = "Connect";
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            SerialWrite("0", 0);
        }

        private void rdoCW_CheckedChanged(object sender, EventArgs e)
        {
            SetMotion = "1";
        }

        private void rdoCCW_CheckedChanged(object sender, EventArgs e)
        {
            SetMotion = "2";
        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            SetSpeed = trackBar1.Value;
            SerialWrite(SetMotion, SetSpeed);
        }
    }
}
