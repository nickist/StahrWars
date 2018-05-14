using System;
using System.Windows.Forms;

namespace Client
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtIP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.panCanvas = new System.Windows.Forms.PictureBox();
            this.chkShowGrid = new System.Windows.Forms.CheckBox();
            this.txtServerMessages = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblSector = new System.Windows.Forms.Label();
            this.chkShowBackground = new System.Windows.Forms.CheckBox();
            this.grpStatus = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.progressBar3 = new System.Windows.Forms.ProgressBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.picShields = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.lblShielsUp = new System.Windows.Forms.Label();
            this.prbHealth = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.panCanvas)).BeginInit();
            this.grpStatus.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picShields)).BeginInit();
            this.SuspendLayout();
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(582, 11);
            this.txtIP.Margin = new System.Windows.Forms.Padding(2);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(111, 20);
            this.txtIP.TabIndex = 1;
            this.txtIP.TabStop = false;
            this.txtIP.Text = "127.0.0.1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(527, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Server IP";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(697, 11);
            this.btnConnect.Margin = new System.Windows.Forms.Padding(2);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(56, 22);
            this.btnConnect.TabIndex = 3;
            this.btnConnect.TabStop = false;
            this.btnConnect.Text = "Join";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(697, 469);
            this.btnQuit.Margin = new System.Windows.Forms.Padding(2);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(56, 22);
            this.btnQuit.TabIndex = 5;
            this.btnQuit.TabStop = false;
            this.btnQuit.Text = "Quit";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // panCanvas
            // 
            this.panCanvas.BackColor = System.Drawing.Color.Black;
            this.panCanvas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panCanvas.Location = new System.Drawing.Point(12, 11);
            this.panCanvas.Name = "panCanvas";
            this.panCanvas.Size = new System.Drawing.Size(480, 480);
            this.panCanvas.TabIndex = 6;
            this.panCanvas.TabStop = false;
            this.panCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.panCanvas_Paint);
            // 
            // chkShowGrid
            // 
            this.chkShowGrid.AutoSize = true;
            this.chkShowGrid.Checked = true;
            this.chkShowGrid.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowGrid.Location = new System.Drawing.Point(507, 474);
            this.chkShowGrid.Name = "chkShowGrid";
            this.chkShowGrid.Size = new System.Drawing.Size(75, 17);
            this.chkShowGrid.TabIndex = 7;
            this.chkShowGrid.Text = "Show Grid";
            this.chkShowGrid.UseVisualStyleBackColor = true;
            this.chkShowGrid.CheckedChanged += new System.EventHandler(this.chkShowGrid_CheckedChanged);
            // 
            // txtServerMessages
            // 
            this.txtServerMessages.Location = new System.Drawing.Point(507, 63);
            this.txtServerMessages.Multiline = true;
            this.txtServerMessages.Name = "txtServerMessages";
            this.txtServerMessages.ReadOnly = true;
            this.txtServerMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtServerMessages.Size = new System.Drawing.Size(245, 401);
            this.txtServerMessages.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(504, 47);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Server Messages";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(9, 494);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Sector:";
            // 
            // lblSector
            // 
            this.lblSector.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSector.ForeColor = System.Drawing.Color.Red;
            this.lblSector.Location = new System.Drawing.Point(80, 494);
            this.lblSector.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSector.Name = "lblSector";
            this.lblSector.Size = new System.Drawing.Size(67, 20);
            this.lblSector.TabIndex = 11;
            this.lblSector.Text = "--";
            this.lblSector.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chkShowBackground
            // 
            this.chkShowBackground.AutoSize = true;
            this.chkShowBackground.Checked = true;
            this.chkShowBackground.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowBackground.Location = new System.Drawing.Point(378, 494);
            this.chkShowBackground.Name = "chkShowBackground";
            this.chkShowBackground.Size = new System.Drawing.Size(114, 17);
            this.chkShowBackground.TabIndex = 12;
            this.chkShowBackground.Text = "Show Background";
            this.chkShowBackground.UseVisualStyleBackColor = true;
            this.chkShowBackground.CheckedChanged += new System.EventHandler(this.chkShowBackground_CheckedChanged);
            // 
            // grpStatus
            // 
            this.grpStatus.Controls.Add(this.label13);
            this.grpStatus.Controls.Add(this.label12);
            this.grpStatus.Controls.Add(this.label11);
            this.grpStatus.Controls.Add(this.label10);
            this.grpStatus.Controls.Add(this.label9);
            this.grpStatus.Controls.Add(this.label8);
            this.grpStatus.Controls.Add(this.label7);
            this.grpStatus.Controls.Add(this.label6);
            this.grpStatus.Controls.Add(this.progressBar1);
            this.grpStatus.Controls.Add(this.progressBar2);
            this.grpStatus.Controls.Add(this.progressBar3);
            this.grpStatus.Controls.Add(this.groupBox1);
            this.grpStatus.Controls.Add(this.prbHealth);
            this.grpStatus.Controls.Add(this.label4);
            this.grpStatus.Location = new System.Drawing.Point(782, 47);
            this.grpStatus.Name = "grpStatus";
            this.grpStatus.Size = new System.Drawing.Size(464, 416);
            this.grpStatus.TabIndex = 13;
            this.grpStatus.TabStop = false;
            this.grpStatus.Text = "Status";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(417, 44);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(19, 13);
            this.label12.TabIndex = 23;
            this.label12.Text = "50";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(417, 70);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(19, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "50";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(417, 96);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(19, 13);
            this.label10.TabIndex = 21;
            this.label10.Text = "50";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(417, 122);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(19, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "10";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(38, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Fuel Pods";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(40, 130);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 18;
            this.label7.Text = "Torpedos";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(47, 103);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "Phasors";
            // 
            // progressBar1
            // 
            this.progressBar1.BackColor = System.Drawing.SystemColors.Control;
            this.progressBar1.Location = new System.Drawing.Point(110, 70);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(290, 20);
            this.progressBar1.Step = 1;
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 14;
            // 
            // progressBar2
            // 
            this.progressBar2.BackColor = System.Drawing.SystemColors.Control;
            this.progressBar2.Location = new System.Drawing.Point(110, 122);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(290, 20);
            this.progressBar2.Step = 1;
            this.progressBar2.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar2.TabIndex = 15;
            // 
            // progressBar3
            // 
            this.progressBar3.BackColor = System.Drawing.SystemColors.Control;
            this.progressBar3.Location = new System.Drawing.Point(110, 96);
            this.progressBar3.Name = "progressBar3";
            this.progressBar3.Size = new System.Drawing.Size(290, 20);
            this.progressBar3.Step = 1;
            this.progressBar3.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar3.TabIndex = 16;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.picShields);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lblShielsUp);
            this.groupBox1.Location = new System.Drawing.Point(22, 189);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(190, 133);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "System";
            // 
            // picShields
            // 
            this.picShields.Location = new System.Drawing.Point(121, 20);
            this.picShields.Name = "picShields";
            this.picShields.Size = new System.Drawing.Size(26, 26);
            this.picShields.TabIndex = 8;
            this.picShields.TabStop = false;
            this.picShields.Click += new System.EventHandler(this.picShields_Click);
            this.picShields.Paint += new System.Windows.Forms.PaintEventHandler(this.picShields_Paint);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(16, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Shields";
            // 
            // lblShielsUp
            // 
            this.lblShielsUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShielsUp.ForeColor = System.Drawing.Color.Red;
            this.lblShielsUp.Location = new System.Drawing.Point(63, 23);
            this.lblShielsUp.Name = "lblShielsUp";
            this.lblShielsUp.Size = new System.Drawing.Size(100, 23);
            this.lblShielsUp.TabIndex = 6;
            this.lblShielsUp.Text = "OFF";
            this.lblShielsUp.Click += new System.EventHandler(this.lblShielsUp_Click);
            // 
            // prbHealth
            // 
            this.prbHealth.BackColor = System.Drawing.SystemColors.Control;
            this.prbHealth.Location = new System.Drawing.Point(110, 44);
            this.prbHealth.Name = "prbHealth";
            this.prbHealth.Size = new System.Drawing.Size(290, 20);
            this.prbHealth.Step = 1;
            this.prbHealth.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.prbHealth.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(54, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Health";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(223, 202);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(19, 13);
            this.label13.TabIndex = 24;
            this.label13.Text = "15";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1266, 683);
            this.Controls.Add(this.grpStatus);
            this.Controls.Add(this.chkShowBackground);
            this.Controls.Add(this.lblSector);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtServerMessages);
            this.Controls.Add(this.chkShowGrid);
            this.Controls.Add(this.panCanvas);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtIP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StahrWars";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.panCanvas)).EndInit();
            this.grpStatus.ResumeLayout(false);
            this.grpStatus.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picShields)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TextBox txtIP;
        private Label label1;
        private Button btnConnect;
        private Button btnQuit;
		private PictureBox panCanvas;
		private CheckBox chkShowGrid;
		private TextBox txtServerMessages;
		private Label label2;
		private Label label3;
		private Label lblSector;
		private CheckBox chkShowBackground;
        private GroupBox grpStatus;
        private Label label4;
        private ProgressBar prbHealth;
        private GroupBox groupBox1;
        private PictureBox picShields;
        private Label label5;
        private Label lblShielsUp;
        private ProgressBar progressBar1;
        private ProgressBar progressBar2;
        private ProgressBar progressBar3;
        private Label label8;
        private Label label7;
        private Label label6;
        private Label label12;
        private Label label11;
        private Label label10;
        private Label label9;
        private Label label13;
    }
}

