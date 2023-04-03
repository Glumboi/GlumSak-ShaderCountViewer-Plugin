using GlumSak_ShaderCountViewer_Plugin.Core;
using GlumSak_ShaderCountViewer_Plugin.GlumSak;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace GlumSak_ShaderCountViewer_Plugin
{
    public class Plugin
    {
        public static int exitCode;
        private static MainWindowDummy mainWindow;

        public static int PluginEntryPoint(IntPtr mainWindowhandle, object[] args) //--> Default Entrypoint of the Plugin
        {
            if (args.Length > 0 &&
                args[0] is bool &&
                (bool)args[0] == true)
            {
                AppDomain.CurrentDomain.ProcessExit
                    += OverrrideExitEvent; //--> Use custom ExitEvent if first param is bool and true
            }
            //Your custom code here, you can also use WindowsForms with this (WPF might be possible too)

            //UI access demo
            mainWindow = new MainWindowDummy(mainWindowhandle);

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Shader Files (*.toc)|*.toc|All files (*.*)|*.*";
            openFileDialog.Title = "Please select a Shader File (shared.toc)";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var shaderCount = Shader.GetShaderCount(openFileDialog.FileName);

                ShowNotification($"ShaderCount of the selected File is: {shaderCount}", Wpf.Ui.Common.SymbolRegular.Info28);
            }
            else
            {
                ShowNotification("Cancelled the Operation!", Wpf.Ui.Common.SymbolRegular.Info28);
            }

            exitCode = 0;
            return exitCode;
        }

        private static void ShowNotification(string message, Wpf.Ui.Common.SymbolRegular icon)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                mainWindow.Notification_SnackBar.Icon = icon;
                mainWindow.Notification_SnackBar.Content = message;

                mainWindow.Notification_SnackBar.Show();
            }));
        }

        private static void OverrrideExitEvent(object sender, EventArgs e)
        {
            Console.WriteLine($"Plugin {Assembly.GetExecutingAssembly().FullName} exited with code: {exitCode}");
            Console.ReadKey();
        }
    }
}