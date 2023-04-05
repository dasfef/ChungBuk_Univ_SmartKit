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

namespace Pulse01
{
    public partial class Form1 : Form
    {
        SerialPort Comport = new SerialPort();
        private delegate void SetTextDelegate(string getString);

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
            string HEAD = inString.Substring(0, 1);
            string DATA = inString.Substring(1);

            if(HEAD == "@")
            {
                string [] parsingData = DATA.Split(',');
                int PPG = Convert.ToInt16(parsingData[0]);
                int diffPPG = Convert.ToInt16(parsingData[1]);

                chart1.Series["Series1"].Points.Add(PPG);
                chart1.Series["Series2"].Points.Add(diffPPG);
                if (chart1.Series["Series1"].Points.Count > 100)
                {
                    chart1.Series["Series1"].Points.RemoveAt(0);
                    chart1.Series["Series2"].Points.RemoveAt(0);
                }
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbComport.Items.Clear();
            var portName = System.IO.Ports.SerialPort.GetPortNames();
            cmbComport.Items.AddRange(portName);
            cmbComport.SelectedIndex = cmbComport.Items.Count - 1;
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
                    btnConnect.Text = "Disconnect";
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
