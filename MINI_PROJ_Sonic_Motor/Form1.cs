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

namespace Sonic_Motor
{
    public partial class Form1 : Form
    {
        SerialPort Comport = new SerialPort();
        private delegate void SetTextDelegate(string getString);

        String Motion = "";
        String SetMotion = "1";

        int Speed = 0;
        int SetSpeed = 0;

        int cm = 0;
        int Setcm = 0;

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

                if (Head == "@")
                {
                    string[] parsingData = Data.Split(',');

                    Motion = parsingData[0];
                    Speed = Convert.ToInt16(parsingData[1]);
                    cm = Convert.ToInt16(parsingData[2]);
                    Status(Motion, Speed, cm);
                }
            }
            catch { };
        }

        private void SerialWrite(string motion, int speed, int cm)
        {
            if (Comport.IsOpen)
            {
                string msg = "@" + motion + "," + speed.ToString() + "," + cm.ToString() + "\n";
                Comport.Write(msg);
            }
        }

        private void Status(string motion, int speed, int cm)
        {
            switch(motion)
            {
                case "0":
                    lblDoor.Text = "CLOSED"; lblDoor.BackColor = Color.DarkGray; break;
                case "1":
                    lblDoor.Text = "OPENING"; lblDoor.BackColor = Color.Orange; break;
                case "2":
                    lblDoor.Text = "CLOSING"; lblDoor.BackColor = Color.YellowGreen; break;
                default:
                    lblDoor.Text = "STOP"; lblDoor.BackColor = Color.Red; break;
            }

            lblSpeed.Text = cm.ToString();
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
                SerialWrite("0", 0, 400);
                Comport.Close();
            }
            Comport.Dispose();
            Comport = null;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text == "Connect")
            {
                Comport.PortName = cmbPort.Text;
                Comport.BaudRate = Convert.ToInt32(cmbRate.Text);
                Comport.DataBits = 8;
                Comport.Open();
                Comport.DiscardInBuffer();
                btnConnect.Text = "Disconnect";
            }
            else
            {
                SerialWrite("0", 0, 400);

                Comport.Close();
                btnConnect.Text = "Connect";
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            SerialWrite("1", 255, 0);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            SerialWrite("2", 255, 0);
        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            SetSpeed = trackBar1.Value;
            SerialWrite(SetMotion, SetSpeed, Setcm);
        }
    }
}
