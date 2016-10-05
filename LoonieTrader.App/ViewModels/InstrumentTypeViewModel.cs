using System.ComponentModel;
using System.Globalization;
using GalaSoft.MvvmLight;

namespace LoonieTrader.App.ViewModels
{
    [DisplayName(@"Instrument Type")]
    public class InstrumentTypeViewModel : ViewModelBase
    {
        private readonly TextInfo _currentTextInfo = CultureInfo.CurrentUICulture.TextInfo;

        private string _type;
        public string Type
        {
            get
            {
                return _currentTextInfo.ToTitleCase(_type.Length > 3 ? _type.ToLower() : _type.ToUpper());
            }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    RaisePropertyChanged();
                }
            }
        }

        public InstrumentViewModel[] Instruments { get; set; }
    }
}