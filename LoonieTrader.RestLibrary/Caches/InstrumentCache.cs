﻿using LoonieTrader.RestLibrary.Models.Responses;

namespace LoonieTrader.RestLibrary.Caches
{
    public static class InstrumentCache
    {
        // persist the caches as yaml

        public static AccountInstrumentsResponse Instruments { get; set; }
    }
}