namespace SocketTest.Forms
{
    partial class Client
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
            this.bButton = new System.Windows.Forms.CheckBox();
            this.AButton = new System.Windows.Forms.CheckBox();
            this.serializedOutput = new System.Windows.Forms.TextBox();
            this.yButton = new System.Windows.Forms.CheckBox();
            this.xButton = new System.Windows.Forms.CheckBox();
            this.cStickX = new System.Windows.Forms.TextBox();
            this.cStickY = new System.Windows.Forms.TextBox();
            this.stickX = new System.Windows.Forms.TextBox();
            this.stickY = new System.Windows.Forms.TextBox();
            this.dDown = new System.Windows.Forms.CheckBox();
            this.dUp = new System.Windows.Forms.CheckBox();
            this.dRight = new System.Windows.Forms.CheckBox();
            this.dLeft = new System.Windows.Forms.CheckBox();
            this.startButton = new System.Windows.Forms.CheckBox();
            this.lButton = new System.Windows.Forms.CheckBox();
            this.rButton = new System.Windows.Forms.CheckBox();
            this.zButton = new System.Windows.Forms.CheckBox();
            this.lAnalog = new System.Windows.Forms.TextBox();
            this.rAnalog = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.enableButton = new System.Windows.Forms.CheckBox();
            this.connectBtn = new System.Windows.Forms.Button();
            this.sendBtn = new System.Windows.Forms.Button();
            this.dcButton = new System.Windows.Forms.Button();
            this.logText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // bButton
            // 
            this.bButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.bButton.Location = new System.Drawing.Point(284, 156);
            this.bButton.Name = "bButton";
            this.bButton.Size = new System.Drawing.Size(34, 33);
            this.bButton.TabIndex = 0;
            this.bButton.Text = "B";
            this.bButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bButton.UseVisualStyleBackColor = true;
            this.bButton.CheckedChanged += new System.EventHandler(this.bButton_CheckedChanged);
            // 
            // AButton
            // 
            this.AButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.AButton.Location = new System.Drawing.Point(325, 156);
            this.AButton.Name = "AButton";
            this.AButton.Size = new System.Drawing.Size(34, 33);
            this.AButton.TabIndex = 1;
            this.AButton.Text = "A";
            this.AButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.AButton.UseVisualStyleBackColor = true;
            this.AButton.CheckedChanged += new System.EventHandler(this.AButton_CheckedChanged);
            // 
            // serializedOutput
            // 
            this.serializedOutput.Location = new System.Drawing.Point(10, 277);
            this.serializedOutput.Name = "serializedOutput";
            this.serializedOutput.Size = new System.Drawing.Size(464, 20);
            this.serializedOutput.TabIndex = 2;
            this.serializedOutput.TextChanged += new System.EventHandler(this.serializedOutput_TextChanged);
            // 
            // yButton
            // 
            this.yButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.yButton.Location = new System.Drawing.Point(325, 116);
            this.yButton.Name = "yButton";
            this.yButton.Size = new System.Drawing.Size(34, 33);
            this.yButton.TabIndex = 3;
            this.yButton.Text = "Y";
            this.yButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.yButton.UseVisualStyleBackColor = true;
            this.yButton.CheckedChanged += new System.EventHandler(this.yButton_CheckedChanged);
            // 
            // xButton
            // 
            this.xButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.xButton.Location = new System.Drawing.Point(365, 116);
            this.xButton.Name = "xButton";
            this.xButton.Size = new System.Drawing.Size(34, 33);
            this.xButton.TabIndex = 4;
            this.xButton.Text = "X";
            this.xButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.xButton.UseVisualStyleBackColor = true;
            this.xButton.CheckedChanged += new System.EventHandler(this.xButton_CheckedChanged);
            // 
            // cStickX
            // 
            this.cStickX.Location = new System.Drawing.Point(95, 93);
            this.cStickX.Name = "cStickX";
            this.cStickX.Size = new System.Drawing.Size(65, 20);
            this.cStickX.TabIndex = 8;
            this.cStickX.TextChanged += new System.EventHandler(this.cStickX_TextChanged);
            // 
            // cStickY
            // 
            this.cStickY.Location = new System.Drawing.Point(95, 139);
            this.cStickY.Name = "cStickY";
            this.cStickY.Size = new System.Drawing.Size(65, 20);
            this.cStickY.TabIndex = 9;
            this.cStickY.TextChanged += new System.EventHandler(this.cStickY_TextChanged);
            // 
            // stickX
            // 
            this.stickX.Location = new System.Drawing.Point(176, 93);
            this.stickX.Name = "stickX";
            this.stickX.Size = new System.Drawing.Size(65, 20);
            this.stickX.TabIndex = 10;
            this.stickX.TextChanged += new System.EventHandler(this.stickX_TextChanged);
            // 
            // stickY
            // 
            this.stickY.Location = new System.Drawing.Point(176, 139);
            this.stickY.Name = "stickY";
            this.stickY.Size = new System.Drawing.Size(65, 20);
            this.stickY.TabIndex = 11;
            this.stickY.TextChanged += new System.EventHandler(this.stickY_TextChanged);
            // 
            // dDown
            // 
            this.dDown.Appearance = System.Windows.Forms.Appearance.Button;
            this.dDown.Location = new System.Drawing.Point(86, 223);
            this.dDown.Name = "dDown";
            this.dDown.Size = new System.Drawing.Size(34, 33);
            this.dDown.TabIndex = 12;
            this.dDown.Text = "v";
            this.dDown.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dDown.UseVisualStyleBackColor = true;
            this.dDown.CheckedChanged += new System.EventHandler(this.dDown_CheckedChanged);
            // 
            // dUp
            // 
            this.dUp.Appearance = System.Windows.Forms.Appearance.Button;
            this.dUp.Location = new System.Drawing.Point(86, 184);
            this.dUp.Name = "dUp";
            this.dUp.Size = new System.Drawing.Size(34, 33);
            this.dUp.TabIndex = 13;
            this.dUp.Text = "^";
            this.dUp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dUp.UseVisualStyleBackColor = true;
            this.dUp.CheckedChanged += new System.EventHandler(this.dUp_CheckedChanged);
            // 
            // dRight
            // 
            this.dRight.Appearance = System.Windows.Forms.Appearance.Button;
            this.dRight.Location = new System.Drawing.Point(126, 203);
            this.dRight.Name = "dRight";
            this.dRight.Size = new System.Drawing.Size(34, 33);
            this.dRight.TabIndex = 14;
            this.dRight.Text = ">";
            this.dRight.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dRight.UseVisualStyleBackColor = true;
            this.dRight.CheckedChanged += new System.EventHandler(this.dRight_CheckedChanged);
            // 
            // dLeft
            // 
            this.dLeft.Appearance = System.Windows.Forms.Appearance.Button;
            this.dLeft.Location = new System.Drawing.Point(46, 203);
            this.dLeft.Name = "dLeft";
            this.dLeft.Size = new System.Drawing.Size(34, 33);
            this.dLeft.TabIndex = 15;
            this.dLeft.Text = "<";
            this.dLeft.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dLeft.UseVisualStyleBackColor = true;
            this.dLeft.CheckedChanged += new System.EventHandler(this.dLeft_CheckedChanged);
            // 
            // startButton
            // 
            this.startButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.startButton.Location = new System.Drawing.Point(365, 155);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(54, 33);
            this.startButton.TabIndex = 16;
            this.startButton.Text = "Start";
            this.startButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.CheckedChanged += new System.EventHandler(this.startButton_CheckedChanged);
            // 
            // lButton
            // 
            this.lButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.lButton.Location = new System.Drawing.Point(285, 77);
            this.lButton.Name = "lButton";
            this.lButton.Size = new System.Drawing.Size(34, 33);
            this.lButton.TabIndex = 17;
            this.lButton.Text = "L";
            this.lButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lButton.UseVisualStyleBackColor = true;
            this.lButton.CheckedChanged += new System.EventHandler(this.lButton_CheckedChanged);
            // 
            // rButton
            // 
            this.rButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.rButton.Location = new System.Drawing.Point(325, 77);
            this.rButton.Name = "rButton";
            this.rButton.Size = new System.Drawing.Size(34, 33);
            this.rButton.TabIndex = 18;
            this.rButton.Text = "R";
            this.rButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.rButton.UseVisualStyleBackColor = true;
            this.rButton.CheckedChanged += new System.EventHandler(this.rButton_CheckedChanged);
            // 
            // zButton
            // 
            this.zButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.zButton.Location = new System.Drawing.Point(365, 77);
            this.zButton.Name = "zButton";
            this.zButton.Size = new System.Drawing.Size(34, 33);
            this.zButton.TabIndex = 19;
            this.zButton.Text = "Z";
            this.zButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.zButton.UseVisualStyleBackColor = true;
            this.zButton.CheckedChanged += new System.EventHandler(this.zButton_CheckedChanged);
            // 
            // lAnalog
            // 
            this.lAnalog.Location = new System.Drawing.Point(95, 43);
            this.lAnalog.Name = "lAnalog";
            this.lAnalog.Size = new System.Drawing.Size(65, 20);
            this.lAnalog.TabIndex = 20;
            this.lAnalog.TextChanged += new System.EventHandler(this.lAnalog_TextChanged);
            // 
            // rAnalog
            // 
            this.rAnalog.Location = new System.Drawing.Point(176, 43);
            this.rAnalog.Name = "rAnalog";
            this.rAnalog.Size = new System.Drawing.Size(65, 20);
            this.rAnalog.TabIndex = 21;
            this.rAnalog.TextChanged += new System.EventHandler(this.rAnalog_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(100, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "L_Analog";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(179, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "R_Analog";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(123, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 24;
            this.label3.Text = "X";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(198, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 25;
            this.label4.Text = "Y";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(123, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(14, 13);
            this.label5.TabIndex = 26;
            this.label5.Text = "X";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(198, 123);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "Y";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(48, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 28;
            this.label7.Text = "C Stick";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(58, 142);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 13);
            this.label8.TabIndex = 29;
            this.label8.Text = "Stick";
            // 
            // enableButton
            // 
            this.enableButton.Appearance = System.Windows.Forms.Appearance.Button;
            this.enableButton.Checked = true;
            this.enableButton.CheckState = System.Windows.Forms.CheckState.Checked;
            this.enableButton.Location = new System.Drawing.Point(285, 36);
            this.enableButton.Name = "enableButton";
            this.enableButton.Size = new System.Drawing.Size(74, 33);
            this.enableButton.TabIndex = 30;
            this.enableButton.Text = "Enable";
            this.enableButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.enableButton.UseVisualStyleBackColor = true;
            this.enableButton.CheckedChanged += new System.EventHandler(this.enableButton_CheckedChanged);
            // 
            // connectBtn
            // 
            this.connectBtn.Location = new System.Drawing.Point(177, 223);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(95, 46);
            this.connectBtn.TabIndex = 31;
            this.connectBtn.Text = "Connect";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // sendBtn
            // 
            this.sendBtn.Location = new System.Drawing.Point(379, 223);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(95, 46);
            this.sendBtn.TabIndex = 32;
            this.sendBtn.Text = "Send";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // dcButton
            // 
            this.dcButton.Location = new System.Drawing.Point(278, 223);
            this.dcButton.Name = "dcButton";
            this.dcButton.Size = new System.Drawing.Size(95, 46);
            this.dcButton.TabIndex = 33;
            this.dcButton.Text = "Disconnect";
            this.dcButton.UseVisualStyleBackColor = true;
            this.dcButton.Click += new System.EventHandler(this.dcButton_Click);
            // 
            // logText
            // 
            this.logText.Location = new System.Drawing.Point(491, 14);
            this.logText.Multiline = true;
            this.logText.Name = "logText";
            this.logText.ReadOnly = true;
            this.logText.Size = new System.Drawing.Size(407, 282);
            this.logText.TabIndex = 34;
            // 
            // Client
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 304);
            this.Controls.Add(this.logText);
            this.Controls.Add(this.dcButton);
            this.Controls.Add(this.sendBtn);
            this.Controls.Add(this.connectBtn);
            this.Controls.Add(this.enableButton);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rAnalog);
            this.Controls.Add(this.lAnalog);
            this.Controls.Add(this.zButton);
            this.Controls.Add(this.rButton);
            this.Controls.Add(this.lButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.dLeft);
            this.Controls.Add(this.dRight);
            this.Controls.Add(this.dUp);
            this.Controls.Add(this.dDown);
            this.Controls.Add(this.stickY);
            this.Controls.Add(this.stickX);
            this.Controls.Add(this.cStickY);
            this.Controls.Add(this.cStickX);
            this.Controls.Add(this.xButton);
            this.Controls.Add(this.yButton);
            this.Controls.Add(this.serializedOutput);
            this.Controls.Add(this.AButton);
            this.Controls.Add(this.bButton);
            this.Name = "Client";
            this.Text = "Test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox bButton;
        private System.Windows.Forms.CheckBox AButton;
        private System.Windows.Forms.TextBox serializedOutput;
        private System.Windows.Forms.CheckBox yButton;
        private System.Windows.Forms.CheckBox xButton;
        private System.Windows.Forms.TextBox cStickX;
        private System.Windows.Forms.TextBox cStickY;
        private System.Windows.Forms.TextBox stickX;
        private System.Windows.Forms.TextBox stickY;
        private System.Windows.Forms.CheckBox dDown;
        private System.Windows.Forms.CheckBox dUp;
        private System.Windows.Forms.CheckBox dRight;
        private System.Windows.Forms.CheckBox dLeft;
        private System.Windows.Forms.CheckBox startButton;
        private System.Windows.Forms.CheckBox lButton;
        private System.Windows.Forms.CheckBox rButton;
        private System.Windows.Forms.CheckBox zButton;
        private System.Windows.Forms.TextBox lAnalog;
        private System.Windows.Forms.TextBox rAnalog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox enableButton;
        private System.Windows.Forms.Button connectBtn;
        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.Button dcButton;
        private System.Windows.Forms.TextBox logText;
    }
}

