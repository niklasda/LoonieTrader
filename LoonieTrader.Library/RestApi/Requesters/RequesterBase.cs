using System;
using System.Net;
using System.Text;
using JsonPrettyPrinterPlus;
using LoonieTrader.Library.Configuration;
using LoonieTrader.Library.Constants;
using LoonieTrader.Library.Interfaces;

namespace LoonieTrader.Library.RestApi.Requesters
{
    public abstract class RequesterBase
    {
        protected RequesterBase(ISettings settings, IFileReaderWriterService fileReaderWriter, IExtendedLogger logger)
        {
            _apiKey = settings.ApiKey;
            _fileReaderWriter = fileReaderWriter;
            _logger = logger;
        }

        private readonly string _apiKey;
        private readonly IFileReaderWriterService _fileReaderWriter;
        private readonly IExtendedLogger _logger;

        private string BearerApiKey
        {
            get { return string.Format("Bearer {0}", _apiKey); }
        }

        protected string GetRestUrl(string arg)
        {
            return string.Format("https://{0}.oanda.com/v3/{1}", Environments.Practice.Value, arg);
        }
        protected string GetHttpRestUrl(string arg)
        {
            return string.Format("http://api-status.oanda.com/api/v1/{0}", arg);
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

        protected WebClient GetAnonymousWebClient()
        {
            var wc = new WebClient();
            //wc.Headers.Add("Authorization", BearerApiKey);
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

        protected string DownloadData(WebClient wc, string urlFormat, params object[] args)
        {
            try
            {
                //return "{}";
                var responseBytes = wc.DownloadData(string.Format(urlFormat, args));
                var responseString = Encoding.UTF8.GetString(responseBytes);
                return responseString;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to Load data");
                throw;
            }
        }
    }
}