using System;
using Microsoft.Win32;
using System.Security;
using System.Windows.Forms;

namespace RunAsAdminMSIExtTinkerer
{
    internal sealed class RegistryHelper
    {
        /// <summary>
        /// Static and constant variables for Registry access
        /// </summary>    
        #region Static and Constant variables

        private const string _msiPackageShell = "Msi.Package\\shell";
        private const string _msiPackageShellRunas = "Msi.Package\\shell\\runas";
        private const string _msiPackageShellRunasCommand = "Msi.Package\\shell\\runas\\command";
        private static RegistryKey _regMsiPackageShell = default(RegistryKey);
        private static RegistryKey _regMsiPackageShellRunas = default(RegistryKey);
        private static RegistryKey _regMsiPackageShellRunasCommand = default(RegistryKey);

        #endregion

        /// <summary>
        /// Checks the registry to see if Run as Administrator for MSI files is enabled
        /// </summary>
        /// <returns></returns>
        public static string CheckForRunAsForMSI()
        {
            string retVal = default(string);

            _regMsiPackageShell = Registry.ClassesRoot.OpenSubKey(_msiPackageShell, true);
            if (!(_regMsiPackageShell == null))
            {
                _regMsiPackageShellRunas = Registry.ClassesRoot.OpenSubKey(_msiPackageShellRunas, true);
                if (!(_regMsiPackageShellRunas == null))
                {
                    _regMsiPackageShellRunasCommand = _regMsiPackageShellRunas.OpenSubKey("command", true);
                    if (!(_regMsiPackageShellRunasCommand == null))
                    {
                        //passing all th above three if conditions mean that the registry hive HKCR\Msi.Package\shell\runas\command exists
                        //check for the required default values under these sub keys
                        //string temp = Convert.ToString(_regMsiPackageShellRunasCommand.GetValue(""));
                        if (String.Compare(Convert.ToString(_regMsiPackageShellRunasCommand.GetValue("")), "msiexec /i \"%1\" %*", StringComparison.InvariantCultureIgnoreCase) == 0 || String.Compare(Convert.ToString(_regMsiPackageShellRunasCommand.GetValue("")), "msiexec /i \\\"%1\\\" %*", StringComparison.InvariantCultureIgnoreCase) == 0)
                        {
                            //string temp2 = Convert.ToString(_regMsiPackageShellRunas.GetValue(""));
                            if (String.Compare(Convert.ToString(_regMsiPackageShellRunas.GetValue("")), "Run &As Administrator") != 0)
                            {
                                retVal = "FixRunAsOnly";
                            }
                            else
                            {
                                retVal = "DoNothing";
                            }
                        }
                        else
                        {
                            retVal = "FixCommandAndRunAs";
                        }

                    }
                }
                else
                {
                    retVal = "CreateCommandAndRunAs";
                }
            }
            else
            {
                throw new Exception("The was an error opening HKEY_CLASSES_ROOT\\Msi.Package\\shell. The registry hive location does not seem to exist.");
            }
            return retVal;
        }

        /// <summary>
        /// Adds/Modifies the RunAs and Command Keys and their values under HKCR\Msi.Package\ hive
        /// </summary>
        /// <returns></returns>
        public static string EnableRunAsForMSI()
        {
            string retVal = default(string);
            try
            {
                string action = CheckForRunAsForMSI();
                string actionResultText = default(string);
                string actionResultTextShort = default(string);

                switch (action)
                {
                        //Modify the Key Value under RunAs Key
                    case "FixRunAsOnly":
                        {
                            _regMsiPackageShellRunas.SetValue("", "Run &As Administrator");

                            actionResultText = Utilities.fixedMSIRunAsMessage;
                            actionResultTextShort = Utilities.fixedMSIRunAsMessageShort;
                            break;
                        }
                        //Modify the Key Value under RunAs Key and Command Key
                    case "FixCommandAndRunAs":
                        {
                            _regMsiPackageShellRunasCommand.SetValue("", "msiexec /i \"%1\" %*");
                            _regMsiPackageShellRunas.SetValue("", "Run &As Administrator");

                            actionResultText = Utilities.fixedMSIRunAsMessage;
                            actionResultTextShort = Utilities.fixedMSIRunAsMessageShort;
                            break;
                        }
                        //Create two new Keys Runas and Command and add respective values to them
                    case "CreateCommandAndRunAs":
                        {
                            //under Shell create a new subkey named RunAs and AssignValues to them
                            _regMsiPackageShell.CreateSubKey("runas");
                            _regMsiPackageShellRunas = _regMsiPackageShell.OpenSubKey("runas", true);
                            _regMsiPackageShellRunas.SetValue("", "Run &As Administrator");

                            //under runas create a new subkey named command and AssignValues to them
                            _regMsiPackageShellRunas.CreateSubKey("command");
                            _regMsiPackageShellRunasCommand = _regMsiPackageShellRunas.OpenSubKey("command", true);
                            _regMsiPackageShellRunasCommand.SetValue("", "msiexec /i \"%1\" %*");

                            actionResultText = Utilities.createdMSIRunAsMessage;
                            actionResultTextShort = Utilities.createdMSIRunAsMessageShort;
                            break;
                        }

                    default:
                        break;
                }

                if (!String.IsNullOrEmpty(actionResultTextShort))
                {
                    retVal = actionResultTextShort;
                }
                if (!String.IsNullOrEmpty(actionResultText))
                {
                    Utilities.WriteToEventLog(actionResultText);
                }
            }
            catch (NullReferenceException nEx)
            {
                //MessageBox.Show(nEx.Message + "\n" + nEx.StackTrace);
                Utilities.WriteToEventLog(nEx);
                Application.Exit();
            }
            catch (InvalidOperationException iOEx)
            {
                MessageBox.Show(iOEx.Message + "\n" + iOEx.StackTrace);
                Utilities.WriteToEventLog(iOEx);
                //Application.Exit();
            }
            catch (UnauthorizedAccessException uAEx)
            {
                MessageBox.Show(uAEx.Message + "\n" + uAEx.StackTrace);
                Utilities.WriteToEventLog(uAEx);
                //Application.Exit();
            }
            catch (SecurityException sEx)
            {
                MessageBox.Show(sEx.Message + "\n" + sEx.StackTrace);
                Utilities.WriteToEventLog(sEx);
                //Application.Exit();
            }
            catch (Exception eX)
            {
                MessageBox.Show(eX.Message + "\n" + eX.StackTrace);
                Utilities.WriteToEventLog(eX);
                //Application.Exit();
            }

            finally
            {
                if (!(_msiPackageShell == null))
                {
                    _regMsiPackageShell.Close();
                }
                if (!(_regMsiPackageShellRunas == null))
                {
                    _regMsiPackageShellRunas.Flush();
                    _regMsiPackageShellRunas.Close();
                }
                if (!(_regMsiPackageShellRunasCommand == null))
                {
                    _regMsiPackageShellRunasCommand.Flush();
                    _regMsiPackageShellRunasCommand.Close();
                }
            }
            return retVal;
        }

        /// <summary>
        /// Deleletes the subkey HKCR\Msi.Package\\shell\\runas and its associated values.
        /// </summary>
        /// <returns></returns>
        public static string DisableRunAsForMSI()
        {
            string retVal = default(string);
            string actionResultText = default(string);
            string actionResultTextShort = default(string);
            try
            {
                Registry.ClassesRoot.DeleteSubKeyTree(_msiPackageShellRunas);
                actionResultText = Utilities.disabledMSIRunAsMessage;
                actionResultTextShort = Utilities.disabledMSIRunAsMessageShort;
            }
            catch (NullReferenceException nEx)
            {
                //MessageBox.Show(nEx.Message + "\n" + nEx.StackTrace);
                Utilities.WriteToEventLog(nEx);
                Application.Exit();
            }
            catch (InvalidOperationException iOEx)
            {
                //MessageBox.Show(iOEx.Message + "\n" + iOEx.StackTrace);
                Utilities.WriteToEventLog(iOEx);
                Application.Exit();
            }
            catch (UnauthorizedAccessException uAEx)
            {
                //MessageBox.Show(uAEx.Message + "\n" + uAEx.StackTrace);
                Utilities.WriteToEventLog(uAEx);
                Application.Exit();
            }
            catch (SecurityException sEx)
            {
                //MessageBox.Show(sEx.Message + "\n" + sEx.StackTrace);
                Utilities.WriteToEventLog(sEx);
                Application.Exit();
            }
            catch (Exception eX)
            {
                //MessageBox.Show(eX.Message + "\n" + eX.StackTrace);
                Utilities.WriteToEventLog(eX);
                Application.Exit();
            }
            finally
            {
            }

            if (!String.IsNullOrEmpty(actionResultTextShort))
            {
                retVal = actionResultTextShort;
            }

            if (!String.IsNullOrEmpty(actionResultText))
            {
                Utilities.WriteToEventLog(actionResultText);
            }

            return retVal;
        }
    }
}
