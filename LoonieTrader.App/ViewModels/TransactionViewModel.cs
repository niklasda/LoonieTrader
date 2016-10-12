
namespace LoonieTrader.App.ViewModels
{
    public class TransactionViewModel
    {
        public string AccountID { get; set; }
        public int AccountNumber { get; set; }
        public int AccountUserID { get; set; }
        public string BatchID { get; set; }
        public int DivisionID { get; set; }
        public string HomeCurrency { get; set; }
        public string Id { get; set; }
        public int SiteID { get; set; }
        public string Time { get; set; }
        public string Type { get; set; }
        public int UserID { get; set; }
        public string Alias { get; set; }
        public string MarginRate { get; set; }
        public decimal AccountBalance { get; set; }
        public string Amount { get; set; }
        public string FundingReason { get; set; }
        public string Instrument { get; set; }
        public string PositionFill { get; set; }
        public string Reason { get; set; }
        public string TimeInForce { get; set; }
        public string Units { get; set; }
        public string OrderID { get; set; }
        public string GtdTime { get; set; }
        public string Price { get; set; }
        //public Stoplossonfill stopLossOnFill { get; set; }
        //public Takeprofitonfill takeProfitOnFill { get; set; }
        public string TriggerCondition { get; set; }
    }
}