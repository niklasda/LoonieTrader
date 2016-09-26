using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;

namespace LoonieTrader.Library.RestApi.Requesters
{
    public class StreamingRequester : RequesterBase, IStreamingRequester
    {
        public StreamingRequester(ISettings settings, IFileReaderWriterService fileReaderWriter, IExtendedLogger logger) 
            : base(settings, fileReaderWriter, logger)
        {
        }

    }
}