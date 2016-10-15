using GalaSoft.MvvmLight;

namespace LoonieTrader.App.ViewModels
{
    public class ServiceViewModel : ViewModelBase
    {
        public string Id
        {
            get
            {
                return Url.Substring(Url.LastIndexOf('/') + 1);

            }
        }

        public string Name
        {
            get
            {
                return Id.Substring(Id.IndexOf('-') + 1).Replace('-', ' ');

            }
        }

        public string Url { get; set; }

    }
}