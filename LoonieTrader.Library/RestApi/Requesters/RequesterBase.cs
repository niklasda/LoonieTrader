using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using JetBrains.Annotations;
using LoonieTrader.Library.Constants;
using LoonieTrader.Library.Extensions;
using LoonieTrader.Library.Interfaces;
using Serilog;

namespace LoonieTrader.Library.RestApi.Requesters;

public abstract class RequesterBase
{
    protected RequesterBase(ISettingsService settingsService, IFileReaderWriterService fileReaderWriter, ILogger logger)
    {
        _settingsService = settingsService;
        _fileReaderWriter = fileReaderWriter;
        _logger = logger;
    }

    private readonly ISettingsService _settingsService;
    private readonly IFileReaderWriterService _fileReaderWriter;
    private readonly ILogger _logger;

    private string BearerApiKey
    {
        get
        {
            var settings = _settingsService.CachedSettings.SelectedEnvironment;
            return $"Bearer {settings.ApiKey}";
        }
    }

    protected string GetRestUrl(string path)
    {
        var settings = _settingsService.CachedSettings.SelectedEnvironment;
        string host = Environments.GetHostValueFor(settings.EnvironmentKey);
        return $"https://{host}.oanda.com/v3/{path}";
    }

    protected string GetStreamingRestUrl(string path)
    {
        var settings = _settingsService.CachedSettings.SelectedEnvironment;
        string host = Environments.GetStreamingHostValueFor(settings.EnvironmentKey);
        return $"https://{host}.oanda.com/v3/{path}";
    }

    //protected string GetHttpRestUrl(string env)
    //{
    //    return string.Format("http://{0}.oanda.com/api/v1/{1}", Environments.Status.Value, env);
    //}

    protected ILogger Logger => _logger;

    private WebClient _authWc;
    //private LoonieWebClient _anonWc;

    protected WebClient GetAuthenticatedWebClient()
    {
        if (_authWc == null)
        {
            _authWc = new WebClient();
            _authWc.Headers.Add("Authorization", BearerApiKey);
            _authWc.Headers.Add("Content-Type", "application/json");
        }

        if (_authWc.Headers.Get("Authorization") != BearerApiKey)
        {
            _authWc.Headers.Set("Authorization", BearerApiKey);
        }

        return _authWc;
    }

    //protected LoonieWebClient GetAnonymousWebClient()
    //{
    //    if (_anonWc == null)
    //    {
    //        _anonWc = new LoonieWebClient();
    //        _anonWc.Headers.Add("Content-Type", "application/json");
    //    }

    //    return _anonWc;
    //}

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
        var address = string.Format(urlFormat, args);

        try
        {
            //var address = string.Format(urlFormat, args);
            var responseBytes = wc.DownloadData(address);
            var responseString = Encoding.UTF8.GetString(responseBytes);
            return responseString;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Failed to GET data from {address}");
            throw;
        }
    }

    [StringFormatMethod("urlFormat")]
    protected string PostData(WebClient wc, string jsonData, string urlFormat, params object[] args)
    {
        var address = string.Format(urlFormat, args);

        try
        {
            var dataBytes = Encoding.UTF8.GetBytes(jsonData);
            var responseBytes = wc.UploadData(address, HttpMethod.Post.Method, dataBytes);
            var responseString = Encoding.UTF8.GetString(responseBytes);
            return responseString;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Failed to POST data from {address}");
            throw;
        }
    }

    [StringFormatMethod("urlFormat")]
    protected string PutData(WebClient wc, string jsonData, string urlFormat, params object[] args)
    {
        var address = string.Format(urlFormat, args);

        try
        {
            byte[] responseBytes;
            if (jsonData != null)
            {
                var dataBytes = Encoding.UTF8.GetBytes(jsonData);
                responseBytes = wc.UploadData(address, HttpMethod.Put.Method, dataBytes);
            }
            else
            {
                //var dataBytes = Encoding.UTF8.GetBytes(jsonData);
                // var address = string.Format(urlFormat, args);
                responseBytes = wc.UploadData(address, HttpMethod.Put.Method, new byte[0]);
            }

            var responseString = Encoding.UTF8.GetString(responseBytes);
            return responseString;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Failed to PUT data from {address}");
            throw;
        }
    }

    [StringFormatMethod("urlFormat")]
    protected string PatchData(WebClient wc, string jsonData, string urlFormat, params object[] args)
    {
        var address = string.Format(urlFormat, args);
        try
        {
            var dataBytes = Encoding.UTF8.GetBytes(jsonData);
            var responseBytes = wc.UploadData(address, "PATCH", dataBytes);
            var responseString = Encoding.UTF8.GetString(responseBytes);
            return responseString;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Failed to PATCH data from {address}");
            throw;
        }
    }

    protected T JsonDeserialize<T>(string jsonString) 
    {
        return JsonSerializer.Deserialize<T>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true } );
            
    }
}