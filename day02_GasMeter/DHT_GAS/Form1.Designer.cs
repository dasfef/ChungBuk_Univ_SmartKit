namespace DHT_GAS
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tempTrack = new System.Windows.Forms.TrackBar();
            this.lblTemp = new System.Windows.Forms.Label();
            this.lblHumi = new System.Windows.Forms.Label();
            this.humiTrack = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbComport = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbBaudRate = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblGas = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.tempTrack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.humiTrack)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tempTrack
            // 
            this.tempTrack.BackColor = System.Drawing.Color.RosyBrown;
            this.tempTrack.Location = new System.Drawing.Point(12, 116);
            this.tempTrack.Maximum = 100;
            this.tempTrack.Minimum = -50;
            this.tempTrack.Name = "tempTrack";
            this.tempTrack.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tempTrack.Size = new System.Drawing.Size(45, 284);
            this.tempTrack.TabIndex = 1;
            this.tempTrack.Value = 50;
            this.tempTrack.Scroll += new System.EventHandler(this.trackBar2_Scroll);
            // 
            // lblTemp
            // 
            this.lblTemp.AutoSize = true;
            this.lblTemp.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTemp.ForeColor = System.Drawing.Color.Crimson;
            this.lblTemp.Location = new System.Drawing.Point(15, 403);
            this.lblTemp.Name = "lblTemp";
            this.lblTemp.Size = new System.Drawing.Size(38, 25);
            this.lblTemp.TabIndex = 2;
            this.lblTemp.Text = "29";
            // 
            // lblHumi
            // 
            this.lblHumi.AutoSize = true;
            this.lblHumi.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHumi.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lblHumi.Location = new System.Drawing.Point(96, 403);
            this.lblHumi.Name = "lblHumi";
            this.lblHumi.Size = new System.Drawing.Size(38, 25);
            this.lblHumi.TabIndex = 3;
            this.lblHumi.Text = "30";
            // 
            // humiTrack
            // 
            this.humiTrack.BackColor = System.Drawing.Color.RosyBrown;
            this.humiTrack.Location = new System.Drawing.Point(92, 116);
            this.humiTrack.Maximum = 100;
            this.humiTrack.Name = "humiTrack";
            this.humiTrack.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.humiTrack.Size = new System.Drawing.Size(45, 284);
            this.humiTrack.TabIndex = 4;
            this.humiTrack.Value = 50;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(50, 407);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 14);
            this.label4.TabIndex = 6;
            this.label4.Text = "`C";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(129, 407);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 14);
            this.label5.TabIndex = 7;
            this.label5.Text = "%";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnConnect);
            this.groupBox1.Controls.Add(this.cmbBaudRate);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.cmbComport);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(595, 100);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Comport";
            // 
            // cmbComport
            // 
            this.cmbComport.FormattingEnabled = true;
            this.cmbComport.Location = new System.Drawing.Point(82, 25);
            this.cmbComport.Name = "cmbComport";
            this.cmbComport.Size = new System.Drawing.Size(121, 20);
            this.cmbComport.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "BaudRate";
            // 
            // cmbBaudRate
            // 
            this.cmbBaudRate.FormattingEnabled = true;
            this.cmbBaudRate.Location = new System.Drawing.Point(82, 61);
            this.cmbBaudRate.Name = "cmbBaudRate";
            this.cmbBaudRate.Size = new System.Drawing.Size(121, 20);
            this.cmbBaudRate.TabIndex = 3;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(209, 25);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(76, 56);
            this.btnConnect.TabIndex = 4;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Controls.Add(this.lblGas);
            this.panel1.Location = new System.Drawing.Point(172, 116);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(411, 284);
            this.panel1.TabIndex = 9;
            // 
            // lblGas
            // 
            this.lblGas.AutoSize = true;
            this.lblGas.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGas.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblGas.Location = new System.Drawing.Point(337, 22);
            this.lblGas.Name = "lblGas";
            this.lblGas.Size = new System.Drawing.Size(28, 29);
            this.lblGas.TabIndex = 0;
            this.lblGas.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(595, 450);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.humiTrack);
            this.Controls.Add(this.lblHumi);
            this.Controls.Add(this.lblTemp);
            this.Controls.Add(this.tempTrack);
            this.Name = "Form1";
            this.Text = "Temp Gas Monitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tempTrack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.humiTrack)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TrackBar tempTrack;
        private System.Windows.Forms.Label lblTemp;
        private System.Windows.Forms.Label lblHumi;
        private System.Windows.Forms.TrackBar humiTrack;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ComboBox cmbBaudRate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbComport;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblGas;
    }
}

