using System.ComponentModel;

namespace LoonieTrader.App.ViewModels
{
    public class OrderViewModel
    {
        [ReadOnly(true)]
        public string CreateTime { get; set; }

        public string GtdTime { get; set; }

        public string Id { get; set; }

        public string Instrument { get; set; }

        public string PartialFill { get; set; }

        public string PositionFill { get; set; }

        public string Price { get; set; }

        public string State { get; set; }

        public string TimeInForce { get; set; }

        public string TradeID { get; set; }

        public string TriggerCondition { get; set; }

        public string Type { get; set; }

        public string Units { get; set; }
    }
}