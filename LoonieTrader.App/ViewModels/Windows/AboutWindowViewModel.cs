using System;
using GalaSoft.MvvmLight.CommandWpf;
using LoonieTrader.App.Views;

namespace LoonieTrader.App.ViewModels.Windows
{
    public class AboutWindowViewModel
    {
        public AboutWindowViewModel()
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