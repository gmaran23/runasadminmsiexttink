using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RunAsAdminMSIExtTinkerer
{
    public partial class frmHelp : Form
    {
        public frmHelp()
        {
            InitializeComponent();
        }

        private void frmHelp_Load(object sender, EventArgs e)
        {
            SetToolTip();
            SetToolTip(label1);
            label1.Text = Utilities.helpMessage;
            label1.Size = new Size(this.Size.Width - 28, this.Size.Height - 40);
        }

        private void frmHelp_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void SetToolTip(object pObject)
        {
            ToolTip toolTip = Utilities.MakeToolTip(30000, 10, 10, true);
            string toolTipText = Utilities.pressEscapeMessage;
            toolTip.SetToolTip((Control)pObject, toolTipText);
        }

        private void SetToolTip()
        {
            ToolTip toolTip = Utilities.MakeToolTip(30000, 10, 10, true);
            string toolTipText = Utilities.pressEscapeMessage;
            toolTip.SetToolTip(this, toolTipText);
        }
    }
}
