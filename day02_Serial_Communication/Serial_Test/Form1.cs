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

namespace Serial_Test
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
            this.BeginInvoke(new SetTextDelegate(SerialReceived), new object[]{ rxd });
        }

        private void SerialReceived(string inString)
        {
            textBox1.AppendText(inString + "\r\n");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ComPort.PortName = "COM4";
            ComPort.BaudRate = 9600;
            ComPort.DataBits = 8;
            ComPort.Parity = Parity.None;               // parity bit check : bit 중 1의 개수가 짝수면 1
            ComPort.StopBits = StopBits.One;
            ComPort.Handshake = Handshake.None;
            ComPort.Open();
            ComPort.DiscardInBuffer();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ComPort.Close();
            ComPort.Dispose();
            ComPort = null;
        }
    }
}