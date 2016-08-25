using System;
using GalaSoft.MvvmLight;

namespace LoonieTrader.App.ViewModels.Windows
{
    public class AboutWindowViewModel : ViewModelBase
    {
        public AboutWindowViewModel()
        {
        }

        public string AboutText
        {
            get { return "About it, and credits to used software,  and credits to used software, "+Environment.NewLine+" and credits to used software,  and credits to used software,  and credits to used software, "; }
        }
    }
}