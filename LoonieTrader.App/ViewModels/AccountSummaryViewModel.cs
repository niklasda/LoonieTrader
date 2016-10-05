using System.ComponentModel;
using GalaSoft.MvvmLight;

namespace LoonieTrader.App.ViewModels
{
    [DisplayName("Account Information")]
    public class AccountSummaryViewModel
    {
        [DisplayName("Net Asset Value (NAV)")]
        public string NAV { get; set; }

        public string Alias { get; set; }

        public string Balance { get; set; }

        public int CreatedByUserID { get; set; }

        public string CreatedTime { get; set; }

        public string Currency { get; set; }

        public bool HedgingEnabled { get; set; }

        public string Id { get; set; }

        public string LastTransactionID { get; set; }

        public string MarginAvailable { get; set; }

        public string MarginCallMarginUsed { get; set; }

        public string MarginCallPercent { get; set; }

        public string MarginCloseoutMarginUsed { get; set; }

        public string MarginCloseoutNAV { get; set; }

        public string MarginCloseoutPercent { get; set; }

        public string MarginCloseoutPositionValue { get; set; }

        public string MarginCloseoutUnrealizedPL { get; set; }

        public string MarginRate { get; set; }

        public string MarginUsed { get; set; }

        public int OpenPositionCount { get; set; }

        public int OpenTradeCount { get; set; }

        public int PendingOrderCount { get; set; }

        [DisplayName("Profit/Loss")]
        public string PL { get; set; }

        public string PositionValue { get; set; }

        public string ResettablePL { get; set; }

        public string UnrealizedPL { get; set; }

        public string WithdrawalLimit { get; set; }
    }
}