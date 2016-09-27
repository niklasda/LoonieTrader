using System.ComponentModel;

namespace LoonieTrader.App.ViewModels
{
    public class ServiceEventViewModel
    {
        [ReadOnly(true)]
        public string Message { get; set; }
        [ReadOnly(true)]
        public string Timestamp { get; set; }

    }
}