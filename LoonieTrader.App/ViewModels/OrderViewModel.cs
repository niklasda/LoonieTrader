using System.ComponentModel;

namespace LoonieTrader.App.ViewModels
{
    public class OrderViewModel
    {
        [ReadOnly(true)]
        public string CreateTime { get; set; }
        [ReadOnly(true)]
        public string GtdTime { get; set; }
        [ReadOnly(true)]
        public string Id { get; set; }
        [ReadOnly(true)]
        public string Instrument { get; set; }
        [ReadOnly(true)]
        public string PartialFill { get; set; }
        [ReadOnly(true)]
        public string PositionFill { get; set; }
        [ReadOnly(true)]
        public string Price { get; set; }
        [ReadOnly(true)]
        public string State { get; set; }
        //public StopLossOnFill stopLossOnFill { get; set; }
        //public TakeProfitOnFill takeProfitOnFill { get; set; }
        [ReadOnly(true)]
        public string TimeInForce { get; set; }
        [ReadOnly(true)]
        public string TradeID { get; set; }
        [ReadOnly(true)]
        public string TriggerCondition { get; set; }
        [ReadOnly(true)]
        public string Type { get; set; }
        [ReadOnly(true)]
        public string Units { get; set; }
    }
}