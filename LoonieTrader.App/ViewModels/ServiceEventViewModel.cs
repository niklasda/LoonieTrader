using System.ComponentModel;

namespace LoonieTrader.App.ViewModels
{
    public class ServiceEventViewModel
    {
        [ReadOnly(true)]
        public string message { get; set; }
        [ReadOnly(true)]
        public string timestamp { get; set; }

    }
}