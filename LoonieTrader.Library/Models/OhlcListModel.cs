using System.Collections.Generic;
using LoonieTrader.Library.Enums;

namespace LoonieTrader.Library.Models
{
    public class OhlcListModel
    {
        public OhlcListModel()
        {
            OhlcList = new List<OhlcModel>();
        }

        public string Ticker { get; set; }
        public PricePointType PointType { get; set; }
        public IList<OhlcModel> OhlcList { get; set; }

    }
}