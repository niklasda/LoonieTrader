﻿using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;

namespace LoonieTrader.Library.RestApi.Requesters
{
    public class PricingHistoryRequester : RequesterBase, IPricingHistoryRequester
    {
        public PricingHistoryRequester(ISettings settings, IFileReaderWriterService fileReaderWriter, IExtendedLogger logger) 
            : base(settings, fileReaderWriter, logger)
        {
        }

    }
}