namespace Lundgren.Forms
{
    partial class ActionForm
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
            this.btnWaveshine = new System.Windows.Forms.Button();
            this.btnMultishine = new System.Windows.Forms.Button();
            this.btnSelectFox = new System.Windows.Forms.Button();
            this.btnSwag = new System.Windows.Forms.Button();
            this.btnMoveTowards = new System.Windows.Forms.Button();
            this.btnDoubleLaser = new System.Windows.Forms.Button();
            this.btnFirefoxToCenter = new System.Windows.Forms.Button();
            this.btnRunUpsmash = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnWaveshine
            // 
            this.btnWaveshine.Location = new System.Drawing.Point(12, 48);
            this.btnWaveshine.Name = "btnWaveshine";
            this.btnWaveshine.Size = new System.Drawing.Size(108, 35);
            this.btnWaveshine.TabIndex = 50;
            this.btnWaveshine.Text = "Waveshine";
            this.btnWaveshine.UseVisualStyleBackColor = true;
            this.btnWaveshine.Click += new System.EventHandler(this.btnWaveshine_Click);
            // 
            // btnMultishine
            // 
            this.btnMultishine.Location = new System.Drawing.Point(126, 7);
            this.btnMultishine.Name = "btnMultishine";
            this.btnMultishine.Size = new System.Drawing.Size(108, 35);
            this.btnMultishine.TabIndex = 22;
            this.btnMultishine.Text = "Multishine";
            this.btnMultishine.UseVisualStyleBackColor = true;
            this.btnMultishine.Click += new System.EventHandler(this.btnMultishine_Click);
            // 
            // btnSelectFox
            // 
            this.btnSelectFox.Location = new System.Drawing.Point(12, 7);
            this.btnSelectFox.Name = "btnSelectFox";
            this.btnSelectFox.Size = new System.Drawing.Size(108, 35);
            this.btnSelectFox.TabIndex = 25;
            this.btnSelectFox.Text = "Select Fox";
            this.btnSelectFox.UseVisualStyleBackColor = true;
            this.btnSelectFox.Click += new System.EventHandler(this.btnSelectFox_Click);
            // 
            // btnSwag
            // 
            this.btnSwag.Location = new System.Drawing.Point(126, 48);
            this.btnSwag.Name = "btnSwag";
            this.btnSwag.Size = new System.Drawing.Size(108, 35);
            this.btnSwag.TabIndex = 51;
            this.btnSwag.Text = "Multis + shdl swap";
            this.btnSwag.UseVisualStyleBackColor = true;
            this.btnSwag.Click += new System.EventHandler(this.btnSwag_Click);
            // 
            // btnMoveTowards
            // 
            this.btnMoveTowards.Location = new System.Drawing.Point(240, 48);
            this.btnMoveTowards.Name = "btnMoveTowards";
            this.btnMoveTowards.Size = new System.Drawing.Size(108, 35);
            this.btnMoveTowards.TabIndex = 61;
            this.btnMoveTowards.Text = "move towards ";
            this.btnMoveTowards.UseVisualStyleBackColor = true;
            this.btnMoveTowards.Click += new System.EventHandler(this.btnMoveTowards_Click);
            // 
            // btnDoubleLaser
            // 
            this.btnDoubleLaser.Location = new System.Drawing.Point(12, 89);
            this.btnDoubleLaser.Name = "btnDoubleLaser";
            this.btnDoubleLaser.Size = new System.Drawing.Size(108, 35);
            this.btnDoubleLaser.TabIndex = 62;
            this.btnDoubleLaser.Text = "Double Laser";
            this.btnDoubleLaser.UseVisualStyleBackColor = true;
            this.btnDoubleLaser.Click += new System.EventHandler(this.btnDoubleLaser_Click_1);
            // 
            // btnFirefoxToCenter
            // 
            this.btnFirefoxToCenter.Location = new System.Drawing.Point(126, 89);
            this.btnFirefoxToCenter.Name = "btnFirefoxToCenter";
            this.btnFirefoxToCenter.Size = new System.Drawing.Size(108, 35);
            this.btnFirefoxToCenter.TabIndex = 63;
            this.btnFirefoxToCenter.Text = "Firefox to center";
            this.btnFirefoxToCenter.UseVisualStyleBackColor = true;
            this.btnFirefoxToCenter.Click += new System.EventHandler(this.btnFirefoxToCenter_Click);
            // 
            // btnRunUpsmash
            // 
            this.btnRunUpsmash.Location = new System.Drawing.Point(240, 89);
            this.btnRunUpsmash.Name = "btnRunUpsmash";
            this.btnRunUpsmash.Size = new System.Drawing.Size(108, 35);
            this.btnRunUpsmash.TabIndex = 70;
            this.btnRunUpsmash.Text = "Run up upsmash";
            this.btnRunUpsmash.UseVisualStyleBackColor = true;
            this.btnRunUpsmash.Click += new System.EventHandler(this.btnRunUpsmash_Click);
            // 
            // ActionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 422);
            this.Controls.Add(this.btnRunUpsmash);
            this.Controls.Add(this.btnWaveshine);
            this.Controls.Add(this.btnMultishine);
            this.Controls.Add(this.btnSelectFox);
            this.Controls.Add(this.btnSwag);
            this.Controls.Add(this.btnMoveTowards);
            this.Controls.Add(this.btnDoubleLaser);
            this.Controls.Add(this.btnFirefoxToCenter);
            this.Name = "ActionForm";
            this.Text = "ActionForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnWaveshine;
        private System.Windows.Forms.Button btnMultishine;
        private System.Windows.Forms.Button btnSelectFox;
        private System.Windows.Forms.Button btnSwag;
        private System.Windows.Forms.Button btnMoveTowards;
        private System.Windows.Forms.Button btnDoubleLaser;
        private System.Windows.Forms.Button btnFirefoxToCenter;
        private System.Windows.Forms.Button btnRunUpsmash;
    }
}