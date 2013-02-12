using System;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;

namespace RunAsAdminMSIExtTinkerer
{
    internal sealed class Utilities
    {
        /// <summary>
        /// string constants used for status/help messages
        /// </summary>
        #region string constants

        public readonly static string pressEscapeMessage = default(string);
        public readonly static string helpMessage = default(string);
        public readonly static string retryTextForMessageBox = default(string);

        public readonly static string fixedMSIRunAsMessage = default(string);
        public readonly static string fixedMSIRunAsMessageShort = default(string);
        public readonly static string createdMSIRunAsMessage = default(string);
        public readonly static string createdMSIRunAsMessageShort = default(string);
        public readonly static string disabledMSIRunAsMessage = default(string);
        public readonly static string disabledMSIRunAsMessageShort = default(string);

        public readonly static string assemblyProductName = default(string);

        #endregion

        static Utilities()
        {
            assemblyProductName = GetAssemblyProductName();

            pressEscapeMessage = Utilities.assemblyProductName + ". Press ESC to close this box or hit the X button at the top right corner.";
            helpMessage = Utilities.assemblyProductName + ". \n\n1>  Checking the box will enable Run As Administrator for MSI files present in this computer.\n2>  Unchecking the box will disable Run As Administrator for MSI files present in this computer.\n\nThis program interacts with the Windows Registry. Be adviced that there is no harm intended to your computer in using this program.";
            retryTextForMessageBox = "Running this program requires Administrator Privilieges. \n\n Hit Retry to Run the program with Administrator Privileges. When promted, hit Allow/Yes or supply necessary Administrator Credentials to proceed.";

            fixedMSIRunAsMessage = "The existing configuration to enable \"Run As Administrator\" for MSI files seemed to be corrupted. \n\n" + Utilities.assemblyProductName + " has fixed it.";
            fixedMSIRunAsMessageShort = "MSI's\"Run As Administrator\" fixed/enabled.";
            createdMSIRunAsMessage = "\"Run As Administrator\" for MSI files is enabled by " + Utilities.assemblyProductName + ".";
            createdMSIRunAsMessageShort = "MSI's \"Run As Administrator\" enabled.";
            disabledMSIRunAsMessage = "\"Run As Administrator\" for MSI files is disabled by " + Utilities.assemblyProductName + ".";
            disabledMSIRunAsMessageShort = "MSI's \"Run As Administrator\" disabled.";
        }

        /// <summary>
        /// Construcs and Returns a ToolTip object
        /// </summary>
        /// <param name="pAutoPopDelay"></param>
        /// <param name="pInitialDelay"></param>
        /// <param name="pReshowDelay"></param>
        /// <param name="pShowAlways"></param>
        /// <returns></returns>
        public static ToolTip MakeToolTip(int pAutoPopDelay, int pInitialDelay, int pReshowDelay, bool pShowAlways)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = pAutoPopDelay;
            toolTip1.InitialDelay = pInitialDelay;
            toolTip1.ReshowDelay = pReshowDelay;
            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTip1.ShowAlways = pShowAlways;
            return toolTip1;
        }

        /// <summary>
        /// Writes the input pEventMessage to Event Log under an Application Source named - RunAsAdminMSIExtTinkerer
        /// </summary>
        /// <param name="pEventMessage"></param>
        public static void WriteToEventLog(string pEventMessage)
        {
            //check if the source name RunAsAdminMSIExtTinkerer exists already, if not - create one
            if (!EventLog.SourceExists(Utilities.assemblyProductName))
            {
                EventLog.CreateEventSource(Utilities.assemblyProductName, "Application");
            }

            //write to the EventLog
            if (!String.IsNullOrEmpty(pEventMessage))
            {
                EventLog.WriteEntry(Utilities.assemblyProductName, pEventMessage, EventLogEntryType.Information, new Random().Next(0, 65535), 0, new UTF8Encoding().GetBytes(pEventMessage));
            }

        }

        /// <summary>
        /// Writes the Exception details to the Windows Event Log under an Application Source named - RunAsAdminMSIExtTinkerer
        /// </summary>
        /// <param name="pEx"></param>
        public static void WriteToEventLog(Exception pEx)
        {
            //check if the source name RunAsAdminMSIExtTinkerer exists already, if not - create one
            if (!EventLog.SourceExists(Utilities.assemblyProductName))
            {
                EventLog.CreateEventSource(Utilities.assemblyProductName, "Application");
            }

            //write to the EventLog
            if (pEx.InnerException == null)
            {
                EventLog.WriteEntry(Utilities.assemblyProductName, pEx.Message + "\n" + pEx.StackTrace + "\n\nSource: " + pEx.Source + "\n\nTargetSite: " + pEx.TargetSite, EventLogEntryType.Error, new Random().Next(0, 65535), 0, new UTF8Encoding().GetBytes(pEx.StackTrace));
            }
            else
            {
                EventLog.WriteEntry(Utilities.assemblyProductName, pEx.Message + "\n" + pEx.StackTrace + "\n\nSource: " + pEx.Source + "\n\nTargetSite: " + pEx.TargetSite + "\n\nInnerException:\n" + pEx.InnerException.Message + "\n" + pEx.InnerException.StackTrace, EventLogEntryType.Error, new Random().Next(0, 65535), 0, new UTF8Encoding().GetBytes(pEx.StackTrace));
            }

        }

        /// <summary>
        /// Gets the Assembly Name
        /// </summary>
        /// <returns></returns>
        public static string GetAssemblyProductName()
        {
            object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
            if (attributes.Length == 0)
            {
                return "";
            }
            return ((AssemblyProductAttribute)attributes[0]).Product;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        /// <summary>
        /// Sets the focus to the main window of the existing process
        /// </summary>
        public static void SetForeGroundWindowFocus()
        {
            Process currentProcess = Process.GetCurrentProcess();
            foreach (Process process in Process.GetProcessesByName(currentProcess.ProcessName))
            {
                SetForegroundWindow(process.MainWindowHandle);
            }
        }

    }
}
