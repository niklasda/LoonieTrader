using System;
using System.Windows;
using System.Windows.Threading;

namespace LoonieTrader.App
{
    public partial class App : Application
    {
        public App()
        {
            // todo get the logger and use it
            AppDomain.CurrentDomain.UnhandledException += AppDomain_UnhandledException;

            Dispatcher.UnhandledException += Dispatcher_UnhandledException;
        }

        private void Dispatcher_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Unhandled Dispatcher Exception:" + Environment.NewLine + e.Exception.Message, "Dispatcher Error");
            e.Handled = true;
        }

        private void AppDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MessageBox.Show("Unhandled AppDomain Exception", "AppDomain Error");
        }

    }
}
