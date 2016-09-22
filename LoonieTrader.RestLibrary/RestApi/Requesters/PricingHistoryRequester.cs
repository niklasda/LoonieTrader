using System.IO;
using System.Net;
using System.Text;
using Jil;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.RestApi.Requesters
{
    public class PricingHistoryRequester : RequesterBase, IPricingHistoryRequester
    {
        public PricingHistoryRequester(ISettings settings, IFileReaderWriterService fileReaderWriter, IExtendedLogger logger) : base(settings, fileReaderWriter, logger)
        {
        }

    }
}