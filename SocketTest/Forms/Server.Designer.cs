namespace SocketTest.Forms
{
    partial class Server
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
            this.buttonsText = new System.Windows.Forms.TextBox();
            this.logText = new System.Windows.Forms.TextBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonsText
            // 
            this.buttonsText.Location = new System.Drawing.Point(281, 16);
            this.buttonsText.Multiline = true;
            this.buttonsText.Name = "buttonsText";
            this.buttonsText.Size = new System.Drawing.Size(531, 320);
            this.buttonsText.TabIndex = 7;
            // 
            // logText
            // 
            this.logText.Location = new System.Drawing.Point(38, 16);
            this.logText.Multiline = true;
            this.logText.Name = "logText";
            this.logText.Size = new System.Drawing.Size(237, 202);
            this.logText.TabIndex = 6;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(140, 275);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(140, 246);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 352);
            this.Controls.Add(this.buttonsText);
            this.Controls.Add(this.logText);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Name = "Server";
            this.Text = "Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox buttonsText;
        private System.Windows.Forms.TextBox logText;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
    }
}