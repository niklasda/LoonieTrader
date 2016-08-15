using System.ComponentModel;

namespace LoonieTrader.App.ViewModels
{
    public class TransactionViewModel
    {
        [ReadOnly(true)]
        public string AccountID { get; set; }
        [ReadOnly(true)]
        public int AccountNumber { get; set; }
        [ReadOnly(true)]
        public int AccountUserID { get; set; }
        [ReadOnly(true)]
        public string BatchID { get; set; }
        [ReadOnly(true)]
        public int DivisionID { get; set; }
        [ReadOnly(true)]
        public string HomeCurrency { get; set; }
        [ReadOnly(true)]
        public string Id { get; set; }
        [ReadOnly(true)]
        public int SiteID { get; set; }
        [ReadOnly(true)]
        public string Time { get; set; }
        [ReadOnly(true)]
        public string Type { get; set; }
        [ReadOnly(true)]
        public int UserID { get; set; }
        [ReadOnly(true)]
        public string Alias { get; set; }
        [ReadOnly(true)]
        public string MarginRate { get; set; }
        [ReadOnly(true)]
        public decimal AccountBalance { get; set; }
        [ReadOnly(true)]
        public string Amount { get; set; }
        [ReadOnly(true)]
        public string FundingReason { get; set; }
        [ReadOnly(true)]
        public string Instrument { get; set; }
        [ReadOnly(true)]
        public string PositionFill { get; set; }
        [ReadOnly(true)]
        public string Reason { get; set; }
        [ReadOnly(true)]
        public string TimeInForce { get; set; }
        [ReadOnly(true)]
        public string Units { get; set; }
        [ReadOnly(true)]
        public string OrderID { get; set; }
        [ReadOnly(true)]
        public string GtdTime { get; set; }
        [ReadOnly(true)]
        public string Price { get; set; }
        //public Stoplossonfill stopLossOnFill { get; set; }
        //public Takeprofitonfill takeProfitOnFill { get; set; }
        [ReadOnly(true)]
        public string TriggerCondition { get; set; }
    }
}