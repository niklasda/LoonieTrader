using System.Net;
using JsonPrettyPrinterPlus;
using LoonieTrader.RestLibrary.Configuration;
using LoonieTrader.RestLibrary.Interfaces;
using Serilog;

namespace LoonieTrader.RestLibrary.RestRequesters
{
    public abstract class RequesterBase
    {
        protected RequesterBase(ISettings settings, IFileReaderWriter fileReaderWriter, IExtendedLogger logger)
        {
            _apiKey = settings.ApiKey;
            _fileReaderWriter = fileReaderWriter;
            _logger = logger;
        }

        private readonly string _apiKey;
        private readonly IFileReaderWriter _fileReaderWriter;
        private readonly IExtendedLogger _logger;

        private string BearerApiKey
        {
            get { return string.Format("Bearer {0}", _apiKey); }
        }

        protected string GetRestUrl(string arg)
        {
            return string.Format("https://{0}.oanda.com/v3/{1}", Environments.Practice.Value, arg);
        }

        protected IExtendedLogger Logger
        {
            get { return _logger; }
        }

        protected WebClient GetAuthenticatedWebClient()
        {
            var wc = new WebClient();
            wc.Headers.Add("Authorization", BearerApiKey);
            wc.Headers.Add("Content-Type", "application/json");
            return wc;
        }

        protected void SaveLocalJson(string fileNamePart1, string fileNamePart2, string json)
        {
            _fileReaderWriter.SaveLocalJson(fileNamePart1, fileNamePart2, json);
            _logger.Information("Saved a file with {0}#{1}", fileNamePart1, fileNamePart2);
            _logger.Debug(json.PrettyPrintJson());
        }

        protected void SaveLocalJson(string fileNamePart1, string fileNamePart2, string fileNamePart3, string json)
        {
            _fileReaderWriter.SaveLocalJson(fileNamePart1, fileNamePart2, fileNamePart3, json);
            _logger.Information("Saved a file with {0}#{1}#{2}", fileNamePart1, fileNamePart2, fileNamePart3);
            _logger.Debug(json.PrettyPrintJson());
        }
    }
}