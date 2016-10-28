using System;
using System.Net;
using System.Net.Http;
using System.Text;
using JetBrains.Annotations;
using JsonPrettyPrinterPlus;
using LoonieTrader.Library.Constants;
using LoonieTrader.Library.Interfaces;

namespace LoonieTrader.Library.RestApi.Requesters
{
    public abstract class RequesterBase
    {
        protected RequesterBase(ISettingsService settingsService, IFileReaderWriterService fileReaderWriter, IExtendedLogger logger)
        {
            _settingsService = settingsService;
            _fileReaderWriter = fileReaderWriter;
            _logger = logger;
        }

        private readonly ISettingsService _settingsService;
        private readonly IFileReaderWriterService _fileReaderWriter;
        private readonly IExtendedLogger _logger;

        private string BearerApiKey
        {
            get
            {
                var settings = _settingsService.CachedSettings.SelectedEnvironment;
                return string.Format("Bearer {0}", settings.ApiKey);
            }
        }

        protected string GetRestUrl(string path)
        {
            var settings = _settingsService.CachedSettings.SelectedEnvironment;
            return string.Format("https://{0}.oanda.com/v3/{1}", Environments.GetHostValueFor(settings.EnvironmentKey), path);
        }

        protected string GetStreamingRestUrl(string path)
        {
            var settings = _settingsService.CachedSettings.SelectedEnvironment;
            return string.Format("https://{0}.oanda.com/v3/{1}", Environments.GetStreamingHostValueFor(settings.EnvironmentKey), path);
        }

        protected string GetHttpRestUrl(string env)
        {
            return string.Format("http://{0}.oanda.com/api/v1/{1}", Environments.Status.Value, env);
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

        [StringFormatMethod("urlFormat")]
        protected string GetData(WebClient wc, string urlFormat, params object[] args)
        {
            try
            {
                var address = string.Format(urlFormat, args);
                var responseBytes = wc.DownloadData(address);
                var responseString = Encoding.UTF8.GetString(responseBytes);
                return responseString;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to GET data");
                throw;
            }
        }

        [StringFormatMethod("urlFormat")]
        protected string PostData(WebClient wc, string jsonData, string urlFormat, params object[] args)
        {
            try
            {
                var dataBytes = Encoding.UTF8.GetBytes(jsonData);
                var address = string.Format(urlFormat, args);
                var responseBytes = wc.UploadData(address, HttpMethod.Post.Method, dataBytes);
                var responseString = Encoding.UTF8.GetString(responseBytes);
                return responseString;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to POST data");
                throw;
            }
        }

        [StringFormatMethod("urlFormat")]
        protected string PutData(WebClient wc, string jsonData, string urlFormat, params object[] args)
        {
            try
            {
                var dataBytes = Encoding.UTF8.GetBytes(jsonData);
                var address = string.Format(urlFormat, args);
                var responseBytes = wc.UploadData(address, HttpMethod.Put.Method, dataBytes);
                var responseString = Encoding.UTF8.GetString(responseBytes);
                return responseString;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to PUT data");
                throw;
            }
        }

        [StringFormatMethod("urlFormat")]
        protected string PatchData(WebClient wc, string jsonData, string urlFormat, params object[] args)
        {
            try
            {
                var dataBytes = Encoding.UTF8.GetBytes(jsonData);
                var responseBytes = wc.UploadData(string.Format(urlFormat, args), "PATCH", dataBytes);
                var responseString = Encoding.UTF8.GetString(responseBytes);
                return responseString;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to PATCH data");
                throw;
            }
        }
    }
}