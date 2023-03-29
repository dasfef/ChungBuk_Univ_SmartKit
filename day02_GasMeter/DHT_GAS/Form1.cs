using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DHT_GAS
{
    public partial class Form1 : Form
    {
        SerialPort ComPort = new SerialPort();
        private delegate void SetTextDelegate(string getString);

        Graphics g;
        private Point Center;
        private double radius;

        public Form1()
        {
            InitializeComponent();
            ComPort.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
        }

        private void DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string rxd = ComPort.ReadTo("\n");          // \n 이 들어올때까지 버퍼에 저장
            this.BeginInvoke(new SetTextDelegate(SerialReceived), new object[] { rxd });
        }

        private void SerialReceived(string inString)
        {
            string temp = inString.Substring(0, 2);
            string humi = inString.Substring(3, 2);
            string ppm = inString.Substring(8);

            lblTemp.Text = temp;
            lblHumi.Text = humi;
            lblGas.Text = ppm;

            tempTrack.Value = Convert.ToInt16(temp);
            humiTrack.Value = Convert.ToInt16(humi);
            //string Head = inString.Substring(5);
            //string Data = inString.Substring(1);

            //if (Head == "@")
            //{
            //    string[] ParsingData = Data.Split(',');

            //    lblTemp.Text = ParsingData[0];
            //    lblHumi.Text = ParsingData[1];
            //}

            int PPM = Convert.ToInt16(ppm);
            double HandsAngle = 2 * Math.PI * ((PPM * (180.0 / 1000.0)) - 180) / 360;
            int HandsX = Center.X + (int)(radius * Math.Cos(HandsAngle));
            int HandsY = Center.Y + (int)(radius * Math.Sin(HandsAngle));
            Pen p = new Pen(Brushes.Navy, 4);
            g.DrawLine(p, HandsX, HandsY, Center.X, Center.Y);

        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text == "Connect")
            {
                ComPort.PortName = cmbComport.Text;
                ComPort.BaudRate = Convert.ToInt32(cmbBaudRate.Text);
                ComPort.DataBits = 8;
                ComPort.Parity = Parity.None;
                ComPort.StopBits = StopBits.One;
                ComPort.Handshake = Handshake.None;
                ComPort.Open();
                ComPort.DiscardInBuffer();
                btnConnect.Text = "Close";
            }
            else
            {
                ComPort.Close();
                btnConnect.Text = "Connect";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ComPort.IsOpen)
            {
                ComPort.Close();
                ComPort.Dispose();
                ComPort = null;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var portName = System.IO.Ports.SerialPort.GetPortNames();
            cmbComport.Items.AddRange(portName);
            cmbComport.SelectedIndex = cmbComport.Items.Count - 1;          // cmbBox 의 마지막 것 선택

            cmbBaudRate.Items.Clear();
            cmbBaudRate.Items.Add("9600");
            cmbBaudRate.Items.Add("19200");
            cmbBaudRate.Items.Add("57600");
            cmbBaudRate.Items.Add("115200");
            cmbBaudRate.SelectedIndex = 0;

            g = panel1.CreateGraphics();

            Center = new Point(panel1.Width / 2, (int)(panel1.Height * (89.0 / 100.0)));
            radius = (panel1.Height * (80.0 / 100));
        }
    }
}
