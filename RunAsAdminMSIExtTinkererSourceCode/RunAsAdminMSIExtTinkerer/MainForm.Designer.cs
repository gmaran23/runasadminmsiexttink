namespace RunAsAdminMSIExtTinkerer
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblActionResult = new System.Windows.Forms.Label();
            this.llblAbout = new System.Windows.Forms.LinkLabel();
            this.llblHelp = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.checkBox1.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.checkBox1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.checkBox1.Location = new System.Drawing.Point(294, 27);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 0;
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.checkBox1_KeyUp);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(276, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enable Run As Administrator for MSI files";
            // 
            // lblActionResult
            // 
            this.lblActionResult.AutoEllipsis = true;
            this.lblActionResult.AutoSize = true;
            this.lblActionResult.Font = new System.Drawing.Font("Verdana", 6.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblActionResult.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblActionResult.Location = new System.Drawing.Point(58, 57);
            this.lblActionResult.Name = "lblActionResult";
            this.lblActionResult.Size = new System.Drawing.Size(0, 15);
            this.lblActionResult.TabIndex = 2;
            this.lblActionResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblActionResult.UseCompatibleTextRendering = true;
            // 
            // llblAbout
            // 
            this.llblAbout.AutoSize = true;
            this.llblAbout.Location = new System.Drawing.Point(285, 0);
            this.llblAbout.Name = "llblAbout";
            this.llblAbout.Size = new System.Drawing.Size(35, 13);
            this.llblAbout.TabIndex = 2;
            this.llblAbout.TabStop = true;
            this.llblAbout.Text = "About";
            this.llblAbout.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblAbout_LinkClicked);
            // 
            // llblHelp
            // 
            this.llblHelp.AutoSize = true;
            this.llblHelp.Location = new System.Drawing.Point(259, 0);
            this.llblHelp.Name = "llblHelp";
            this.llblHelp.Size = new System.Drawing.Size(29, 13);
            this.llblHelp.TabIndex = 2;
            this.llblHelp.TabStop = true;
            this.llblHelp.Text = "Help";
            this.llblHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.llblHelp_LinkClicked);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(321, 81);
            this.Controls.Add(this.llblHelp);
            this.Controls.Add(this.llblAbout);
            this.Controls.Add(this.lblActionResult);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RunAsAdminMSIExtTinkerer  (Administrator)";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblActionResult;
        private System.Windows.Forms.LinkLabel llblAbout;
        private System.Windows.Forms.LinkLabel llblHelp;
    }
}

