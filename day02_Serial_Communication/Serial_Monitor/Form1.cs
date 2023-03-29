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

namespace Serial_Monitor
{
    public partial class Form1 : Form
    {
        SerialPort ComPort = new SerialPort();
        private delegate void SetTextDelegate(string getString);

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
            string Head = inString.Substring(0, 1);
            string Data = inString.Substring(1);

            if(Head == "$")
            {
                string[] ParsingData = Data.Split(',');

                lblData1.Text = ParsingData[0];
                lblData2.Text = ParsingData[1];
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

        private void button1_Click(object sender, EventArgs e)
        {
            if(button1.Text == "Connect")
            {
                ComPort.PortName = cmbComport.Text;
                ComPort.BaudRate = Convert.ToInt32(cmbBaudRate.Text);
                ComPort.DataBits = 8;
                ComPort.Parity = Parity.None;
                ComPort.StopBits = StopBits.One;
                ComPort.Handshake = Handshake.None;
                ComPort.Open();
                ComPort.DiscardInBuffer();
                button1.Text = "Close";
            }
            else
            {
                ComPort.Close();
                button1.Text = "Connect";
            }
        }
    }
}
