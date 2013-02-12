using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace RunAsAdminMSIExtTinkerer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
         
        private void SetToolTip(object pObject)
        {
            ToolTip toolTip = Utilities.MakeToolTip(30000, 10, 10, true);
            string toolTipText = Utilities.helpMessage;

            toolTip.SetToolTip((Control)pObject, toolTipText);
        }

        #region EventHandlers

        private void Form1_Load(object sender, EventArgs e)
        {
            this.SetToolTip(this.llblHelp);

            //if Run As Admin for MSI is already enabled, check the checkbox and then subscribe for CheckChanged Event
            if (RegistryHelper.CheckForRunAsForMSI() == "DoNothing")
                checkBox1.Checked = true;

            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Acquire the state of the CheckBox.
            CheckState state = checkBox1.CheckState;
            switch (state)
            {
                case CheckState.Checked:
                    {
                        //enable Run As Administrator
                        lblActionResult.Text = RegistryHelper.EnableRunAsForMSI();
                        break;
                    }
                case CheckState.Indeterminate:
                    {
                        break;
                    }
                case CheckState.Unchecked:
                    {
                        //disable Run As Administrator
                        lblActionResult.Text = RegistryHelper.DisableRunAsForMSI();
                        break;
                    }
            }
        }

        private void llblHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new frmHelp().ShowDialog();
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        private void llblAbout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        private void checkBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                this.Close();
        }

        /// <summary>
        /// Since this form Uses a FixedToolWindow style, it does not show in the task bar unless focussed.
        /// Trying to call the SetForegroundWindow to force the focus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Shown(object sender, EventArgs e)
        {
            Utilities.SetForeGroundWindowFocus();
        }

        #endregion
    }
}
