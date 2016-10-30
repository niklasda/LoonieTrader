using System;
using System.Windows;
using System.Windows.Threading;
using LoonieTrader.Library.Interfaces;
using Microsoft.Practices.ServiceLocation;

namespace LoonieTrader.App
{
    public partial class App : Application
    {
        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += AppDomain_UnhandledException;

            Dispatcher.UnhandledException += Dispatcher_UnhandledException;
        }

        private void Dispatcher_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            if (ServiceLocator.IsLocationProviderSet)
            {
                var elogger = ServiceLocator.Current.GetInstance<IExtendedLogger>();
                elogger.Error(e.Exception, "Dispatcher_UnhandledException");
            }

            MessageBox.Show("Unhandled Dispatcher Exception:" + Environment.NewLine + e.Exception.Message, "Dispatcher Error");
            e.Handled = true;
        }

        private void AppDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (ServiceLocator.IsLocationProviderSet)
            {
                var elogger = ServiceLocator.Current.GetInstance<IExtendedLogger>();
                elogger.Error(e.ExceptionObject as Exception,  "AppDomain_UnhandledException");
            }

            MessageBox.Show("Unhandled AppDomain Exception: " + Environment.NewLine + e.ExceptionObject, "AppDomain Error");
        }
    }
}
