using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Taskbar;

namespace XIMALAYA.PCDesktop.Tools.Untils
{
    public static class Utilities
    {
        private static void InternalRegisterFileAssociations(
            bool unregister, string progId, bool registerInHKCU,
            string appId, string openWith, string[] extensions)
        {

            ProcessStartInfo psi = new ProcessStartInfo(
                    typeof(Utilities).Assembly.Location);
            psi.Arguments =
                progId + " " +
                registerInHKCU + " "
                + appId
                + " \"" + openWith + "\" " +
                unregister + " "
                + string.Join(" ", extensions);
            psi.UseShellExecute = true;
            psi.Verb = "runas"; //Launch elevated
            Process.Start(psi).WaitForExit();
        }

        /// <summary>
        /// Registers file associations for an application.
        /// </summary>
        /// <param name="progId">The application's ProgID.</param>
        /// <param name="registerInHKCU">Whether to register the
        /// association per-user (in HKCU).  The only supported value
        /// at this time is <b>false</b>.</param>
        /// <param name="appId">The application's app-id.</param>
        /// <param name="openWith">The command and arguments to be used
        /// when opening a shortcut to a document.</param>
        /// <param name="extensions">The extensions to register.</param>
        public static void RegisterFileAssociations(string progId,
            bool registerInHKCU, string appId, string openWith,
            params string[] extensions)
        {
            InternalRegisterFileAssociations(
                false, progId, registerInHKCU, appId, openWith, extensions);
        }

        /// <summary>
        /// Unregisters file associations for an application.
        /// </summary>
        /// <param name="progId">The application's ProgID.</param>
        /// <param name="registerInHKCU">Whether to register the
        /// association per-user (in HKCU).  The only supported value
        /// at this time is <b>false</b>.</param>
        /// <param name="appId">The application's app-id.</param>
        /// <param name="openWith">The command and arguments to be used
        /// when opening a shortcut to a document.</param>
        /// <param name="extensions">The extensions to unregister.</param>
        public static void UnregisterFileAssociations(string progId,
            bool registerInHKCU, string appId, string openWith,
            params string[] extensions)
        {
            InternalRegisterFileAssociations(
                true, progId, registerInHKCU, appId, openWith, extensions);
        }

        public static bool IsApplicationRegistered(string appId)
        {
            try
            {
                using (RegistryKey progIdKey = Registry.ClassesRoot.OpenSubKey(appId))
                {
                    return progIdKey != null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source);
            }
            return false;
        }

        public static bool HasThumbnailPreview(UIElement element)
        {
            return TaskbarManager.Instance.TabbedThumbnail.GetThumbnailPreview(element) != null;
        }

        public static Vector GetOffset(Window parent, Visual visual)
        {
            GeneralTransform ge = visual.TransformToVisual(Application.Current.MainWindow);
            Point offset = ge.Transform(new Point(0, 0));
            return new Vector(offset.X, offset.Y);
        }
    }
}
