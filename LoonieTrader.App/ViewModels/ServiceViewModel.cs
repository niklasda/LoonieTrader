using GalaSoft.MvvmLight;

namespace LoonieTrader.App.ViewModels
{
    public class ServiceViewModel : ViewModelBase
    {
        public string Name
        {
            get
            {
                return Url.Substring(Url.LastIndexOf('/') + 1);

            }
        }

        public string Url { get; set; }

    }
}