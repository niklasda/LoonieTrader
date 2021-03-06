﻿using System;
using System.Windows;
using System.Windows.Threading;
using LoonieTrader.Library.Interfaces;
using SciChart.Charting.Visuals;
using CommonServiceLocator;

namespace LoonieTrader.App
{
    public partial class App : Application
    {
        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += AppDomain_UnhandledException;

            Dispatcher.UnhandledException += Dispatcher_UnhandledException;

            // Ensure SetLicenseKey is called once, before any SciChartSurface instance is created
            // Check this code into your version-control and it will enable SciChart
            // for end-users of your application who are not activated
            SciChartSurface.SetRuntimeLicenseKey(@"<LicenseContract>
                                                      <Customer>nd@sql8r.net</Customer>
                                                      <OrderId>ABT171227-5123-40128</OrderId>
                                                      <LicenseCount>1</LicenseCount>
                                                      <IsTrialLicense>false</IsTrialLicense>
                                                      <SupportExpires>12/30/2017 00:00:00</SupportExpires>
                                                      <ProductCode>SC-WPF-2D-PRO</ProductCode>
                                                      <KeyCode>lwAAAAEAAAA+jKgBMH/TAWwAQ3VzdG9tZXI9bmRAc3FsOHIubmV0O09yZGVySWQ9QUJUMTcxMjI3LTUxMjMtNDAxMjg7U3Vic2NyaXB0aW9uVmFsaWRUbz0zMC1EZWMtMjAxNztQcm9kdWN0Q29kZT1TQy1XUEYtMkQtUFJPapJXLBU2cqw30GPiZOCwu0MycGsuOaQ+XP8RDZNCDk+m+eFmsFdRfUrqD1GYc75X</KeyCode>
                                                    </LicenseContract>");
        }
        // lwAAAAEAAAA+jKgBMH/TAWwAQ3VzdG9tZXI9bmRAc3FsOHIubmV0O09yZGVySWQ9QUJUMTcxMjI3LTUxMjMtNDAxMjg7U3Vic2NyaXB0aW9uVmFsaWRUbz0zMC1EZWMtMjAxNztQcm9kdWN0Q29kZT1TQy1XUEYtMkQtUFJPapJXLBU2cqw30GPiZOCwu0MycGsuOaQ+XP8RDZNCDk+m+eFmsFdRfUrqD1GYc75X
        private void Dispatcher_UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            string exMsg = String.Empty;
            if (ServiceLocator.IsLocationProviderSet)
            {
                var logger = ServiceLocator.Current.GetInstance<IExtendedLogger>();
                logger.Error(e.Exception, "Dispatcher_UnhandledException");
                exMsg = e.Exception.Message;

                if (e.Exception.InnerException != null)
                {
                    logger.Error(e.Exception.InnerException, "Dispatcher_UnhandledException - Inner");
                    exMsg = $"{e.Exception.Message}{Environment.NewLine}{e.Exception.InnerException.Message}";
                }
            }

            MessageBox.Show("Unhandled Dispatcher Exception:" + Environment.NewLine + exMsg, "Dispatcher Error");
            e.Handled = true;
        }

        private void AppDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (ServiceLocator.IsLocationProviderSet)
            {
                var logger = ServiceLocator.Current.GetInstance<IExtendedLogger>();
                var ex = e.ExceptionObject as Exception;
                logger.Error(ex,  "AppDomain_UnhandledException");

                if (ex?.InnerException != null)
                {
                    logger.Error(ex.InnerException, "AppDomain_UnhandledException - Inner");
                }
            }

            MessageBox.Show("Unhandled AppDomain Exception: " + Environment.NewLine + e.ExceptionObject, "AppDomain Error");
        }
    }
}
