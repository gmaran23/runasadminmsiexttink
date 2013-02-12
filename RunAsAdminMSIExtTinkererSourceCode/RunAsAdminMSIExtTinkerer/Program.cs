using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Security.Principal;
using System.Diagnostics;
using System.ComponentModel;
using System.Threading;

namespace RunAsAdminMSIExtTinkerer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //obtain a Mutex to allow only one instance of the application at a time
            bool createdNew = true;
            using (Mutex mutex = new Mutex(true, Utilities.GetAssemblyProductName(), out createdNew))
            {
                if (createdNew)
                {
                    //before we start the application, check for Admin privileges and proceed
                    if (!IsRunningElevated())
                    {
                        //if not elevated, try starting an elevated instance of the program
                        StartElevatedInstance();
                    }
                    else
                    {
                        //if running elevated already, perform the steps below
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new MainForm());
                    }
                }
                else
                {
                    //bring the existing process' main window to foreground
                    Utilities.SetForeGroundWindowFocus();
                }

            }
        }

        /// <summary>
        /// Checks if the instance of running program is running with Elevated Privileges or not
        /// </summary>
        /// <returns></returns>
        private static bool IsRunningElevated()
        {
            WindowsIdentity winIdentity = WindowsIdentity.GetCurrent();
            WindowsPrincipal winPincipal = new WindowsPrincipal(winIdentity);
            return winPincipal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        /// <summary>
        /// Attempts to start a new Elevated instance of the running program
        /// </summary>
        private static void StartElevatedInstance()
        {
            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo(Utilities.GetAssemblyProductName() + ".exe");
                processStartInfo.UseShellExecute = true;
                processStartInfo.Verb = "runas";
                Process.Start(processStartInfo);
            }
            catch (Win32Exception)
            {
                DialogResult dr = MessageBox.Show(Utilities.retryTextForMessageBox, "Administrator Privileges Required!", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);

                if (dr == DialogResult.Retry)
                {
                    //on the event of Retry, call the same method again to attempt opening the program with Elevated privileges
                    StartElevatedInstance();
                }
                else
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Unexpected Error Occurred. The program will quit now." + "\n\n" + ex.Message + "\n" + ex.StackTrace + "\n\n" + ex.GetType(), "An Error Occurred!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
            }
        }

    }
}
