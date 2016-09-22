using System.IO;
using System.Net;
using System.Text;
using Jil;
using LoonieTrader.RestLibrary.Interfaces;
using LoonieTrader.RestLibrary.RestApi.Interfaces;
using LoonieTrader.RestLibrary.RestApi.Responses;

namespace LoonieTrader.RestLibrary.RestApi.Requesters
{
    public class StreamingRequester : RequesterBase, IStreamingRequester
    {
        public StreamingRequester(ISettings settings, IFileReaderWriterService fileReaderWriter, IExtendedLogger logger) : base(settings, fileReaderWriter, logger)
        {
        }

    }
}