using System.ComponentModel;

namespace LoonieTrader.App.ViewModels
{
    public class AccountSummaryViewModel
    {
        [ReadOnly(true), DisplayName("Net Asset Value (NAV)")]
        public string NAV { get; set; }

        [ReadOnly(true)]
        public string Alias { get; set; }

        [ReadOnly(true)]
        public string Balance { get; set; }

        [ReadOnly(true)]
        public int CreatedByUserID { get; set; }

        [ReadOnly(true)]
        public string CreatedTime { get; set; }

        [ReadOnly(true)]
        public string Currency { get; set; }

        [ReadOnly(true)]
        public bool HedgingEnabled { get; set; }

        [ReadOnly(true)]
        public string Id { get; set; }

        [ReadOnly(true)]
        public string LastTransactionID { get; set; }

        [ReadOnly(true)]
        public string MarginAvailable { get; set; }

        [ReadOnly(true)]
        public string MarginCallMarginUsed { get; set; }

        [ReadOnly(true)]
        public string MarginCallPercent { get; set; }

        [ReadOnly(true)]
        public string MarginCloseoutMarginUsed { get; set; }

        [ReadOnly(true)]
        public string MarginCloseoutNAV { get; set; }

        [ReadOnly(true)]
        public string MarginCloseoutPercent { get; set; }

        [ReadOnly(true)]
        public string MarginCloseoutPositionValue { get; set; }

        [ReadOnly(true)]
        public string MarginCloseoutUnrealizedPL { get; set; }

        [ReadOnly(true)]
        public string MarginRate { get; set; }

        [ReadOnly(true)]
        public string MarginUsed { get; set; }

        [ReadOnly(true)]
        public int OpenPositionCount { get; set; }

        [ReadOnly(true)]
        public int OpenTradeCount { get; set; }

        [ReadOnly(true)]
        public int PendingOrderCount { get; set; }

        [ReadOnly(true),DisplayName("Profit/Loss")]
        public string PL { get; set; }

        [ReadOnly(true)]
        public string PositionValue { get; set; }

        [ReadOnly(true)]
        public string ResettablePL { get; set; }

        [ReadOnly(true)]
        public string UnrealizedPL { get; set; }

        [ReadOnly(true)]
        public string WithdrawalLimit { get; set; }
    }
}