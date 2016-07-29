using System;
using System.Collections;
using System.Collections.Generic;
using GalaSoft.MvvmLight.CommandWpf;
using LoonieTrader.App.Windows;
using LoonieTrader.RestLibrary.Configuration;

namespace LoonieTrader.App.ViewModels
{
    public class AboutViewModel
    {
        public AboutViewModel()
        {
            PerformLoginCommand = new RelayCommand(PerformLogin);
        }

        public string AboutText
        {
            get { return "About it, and credits to used software,  and credits to used software, "+Environment.NewLine+" and credits to used software,  and credits to used software,  and credits to used software, "; }
        }


        public RelayCommand PerformLoginCommand { get; set; }


        private void PerformLogin()
        {

            MainWindow mw = new MainWindow();
            mw.Show();

        }
    }
}