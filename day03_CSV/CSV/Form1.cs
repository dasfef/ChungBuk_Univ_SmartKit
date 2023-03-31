using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace CSV
{
    public partial class Form1 : Form
    {
        SerialPort ComPort = new SerialPort();
        private delegate void SetTextDelegate(string getString);

        public string csvFileName;
        public StreamWriter csvStream;
        public string strMessage;

        public int RndValue;
        public Random rnd = new Random();

        int _temp, _humi, _ppm;

        public Form1()
        {
            InitializeComponent();
            ComPort.DataReceived += new SerialDataReceivedEventHandler(DataReceived);
        }

        private void DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string rxd = ComPort.ReadTo("\n");
            this.BeginInvoke(new SetTextDelegate(SerialReceived), new object[] { rxd });
        }

        private void SerialReceived(string inString)
        {
            string temp = inString.Substring(0, 2);
            string humi = inString.Substring(3, 2);
            string ppm = inString.Substring(8);

            lblTemp.Text = temp;
            lblHumi.Text = humi;
            lblPpm.Text = ppm;

            _temp = Convert.ToInt16(temp);
            _humi = Convert.ToInt16(humi);
            _ppm = Convert.ToInt16(ppm);

            CSV_Write(_temp, _humi, _ppm);
        }

        public bool CSV_Init()
        {
            csvFileName = Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".csv";
            csvStream = File.AppendText(csvFileName);
            if(File.Exists(csvFileName) == true)
            {
                strMessage = DateTime.Now.ToString("HH:mm:ss") + "," + "#" + "," + "On System";
                csvStream.WriteLine(strMessage);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CSV_Close()
        {
            if(File.Exists(csvFileName) == true)
            {
                strMessage = DateTime.Now.ToString("HH:mm:ss") + "," + "#" + "," + "On System";
                csvStream.WriteLine(strMessage);
                csvStream.Close();
            }
        }

        public void CSV_Start()
        {
            if(File.Exists(csvFileName) == true)
            {
                strMessage = DateTime.Now.ToString("HH:mm:ss") + "," + "#" + "," + "Start System";
                csvStream.WriteLine(strMessage);
                strMessage = "Time, Class, Temp, Humi, PPM";
                csvStream.WriteLine(strMessage);

            }
        }

        public void CSV_Pause()
        {
            strMessage = DateTime.Now.ToString("HH:mm:ss") + "," + "#" + "," + "Stop System";
            csvStream.WriteLine(strMessage);
        }

        public void CSV_Write(int Data1, int Data2, int Data3)
        {
            if(File.Exists(csvFileName) == true)
            {
                strMessage = DateTime.Now.ToString("HH:mm:ss") + "," + "@" + "," + Data1.ToString() + "," + Data2.ToString() + "," + Data3.ToString();
                csvStream.WriteLine(strMessage);
            }
            else
            {
                csvFileName = Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".csv";
                csvStream = File.AppendText(csvFileName);
                if(File.Exists(csvFileName) == true)
                {
                    strMessage = DateTime.Now.ToString("HH:mm:ss") + "," + "#" + "," + "Rework System";
                    csvStream.WriteLine(strMessage);
                    strMessage = DateTime.Now.ToString("HH:mm:ss") + "," + "@" + "," + Data1.ToString() + "," + Data2.ToString() + "," + Data3.ToString();
                    csvStream.WriteLine(strMessage);
                }
                lblFilePath.Text = csvFileName;
                
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var portName = System.IO.Ports.SerialPort.GetPortNames();
            cmbPort.Items.AddRange(portName);
            cmbPort.SelectedIndex = cmbPort.Items.Count - 1;

            cmbRate.Items.Clear();
            cmbRate.Items.Add("9600");
            cmbRate.Items.Add("19200");
            cmbRate.Items.Add("57600");
            cmbRate.Items.Add("115200");
            cmbRate.SelectedIndex = 0;

            if (CSV_Init())
            {
                lblFilePath.Text = csvFileName;
            }
            else
            {
                lblFilePath.Text = "Failed csv File";
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            CSV_Close();

            if (ComPort.IsOpen)
            {
                ComPort.Close();
                ComPort.Dispose();
                ComPort = null;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if(btnStart.Text == "Start")
            {
                CSV_Start();
                timer1.Start();
                btnStart.Text = "Stop";
            }
            else
            {
                timer1.Stop();
                CSV_Pause();
                btnStart.Text = "Start";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //RndValue = rnd.Next(100);

            lblData.Text = _temp.ToString();
            lblData2.Text = _humi.ToString();
            lblData3.Text = _ppm.ToString();
            //lblData.Text = RndValue.ToString();
            //CSV_Write(RndValue);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (btnConnect.Text == "Connect")
            {
                ComPort.PortName = cmbPort.Text;
                ComPort.BaudRate = Convert.ToInt32(cmbRate.Text);
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
    }
}
