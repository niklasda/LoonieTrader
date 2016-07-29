using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using GalaSoft.MvvmLight.CommandWpf;
using LoonieTrader.App.Windows;
using LoonieTrader.RestLibrary.Configuration;

namespace LoonieTrader.App.ViewModels
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login, () => CanClose);
        }

        public bool CanClose
        {
            get
            {
                return !string.IsNullOrWhiteSpace(SelectedEnvironment) &&
                    !string.IsNullOrWhiteSpace(ApiKey) &&
                    ApiKey.Split('-').Length == 4;
            }
        }

        public string ApiKey { get; set; } = "123-123-1234567-123";

        public string SelectedEnvironment { get; set; } = "Practice";

        public IList<KeyValuePair<string, string>> AvailableEnvironemnts
        {
            get { return new[] { Environments.Sandbox, Environments.Practice, Environments.Live }; }
        }

        public RelayCommand LoginCommand { get; set; }

        public Action CloseAction { get; set; }

        private void Login()
        {

            MainWindow mw = new MainWindow();
            mw.Show();

            Application.Current.MainWindow = mw;

            CloseAction();
        }
    }
}