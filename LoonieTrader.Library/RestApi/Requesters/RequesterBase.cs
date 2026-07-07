using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
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

    private HttpClient _authWc;
    //private LoonieWebClient _anonWc;

    protected HttpClient GetAuthenticatedWebClient()
    {
        if (_authWc == null)
        {
            _authWc = new HttpClient();
            _authWc.DefaultRequestHeaders.Add("Authorization", BearerApiKey);
            _authWc.DefaultRequestHeaders.Add("Content-Type", "application/json");
        }

        //if (_authWc.DefaultRequestHeaders.GetValue("Authorization") != BearerApiKey)
        //{
        //    _authWc.DefaultRequestHeaders.Set("Authorization", BearerApiKey);
        //}

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
    protected string GetData(HttpClient wc, string urlFormat, params object[] args)
    {
        var address = string.Format(urlFormat, args);

        try
        {
            //var address = string.Format(urlFormat, args);
            var responseString = wc.GetStringAsync(address).Result;
            //   var responseString = Encoding.UTF8.GetString(responseBytes);
            return responseString;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Failed to GET data from {address}");
            throw;
        }
    }

    [StringFormatMethod("urlFormat")]
    protected string PostData(HttpClient wc, string jsonData, string urlFormat, params object[] args)
    {
        var address = string.Format(urlFormat, args);

        try
        {

         //   var dataBytes = Encoding.UTF8.GetBytes(jsonData);
            var c = JsonContent.Create(jsonData);
            //          HttpContent c= new JsonContent(dataBytes);
            var responseBytes = wc.PostAsync(address, c).Result;
            var responseString = responseBytes.Content.ReadAsStringAsync().Result;
            return responseString;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Failed to POST data from {address}");
            throw;
        }
    }

    [StringFormatMethod("urlFormat")]
    protected string PutData(HttpClient wc, string jsonData, string urlFormat, params object[] args)
    {
        var address = string.Format(urlFormat, args);

        try
        {
            string responseString;
            if (jsonData != null)
            {
               // var dataBytes = Encoding.UTF8.GetBytes(jsonData);
                var c = JsonContent.Create(jsonData);
                responseString = wc.PutAsync(address, c).Result.Content.ReadAsStringAsync().Result;
            }
            else
            {
                //var dataBytes = Encoding.UTF8.GetBytes(jsonData);
                // var address = string.Format(urlFormat, args);
                //var c = JsonContent.Create();

                responseString = wc.PutAsync(address, null).Result.Content.ReadAsStringAsync().Result;
            }

 //           var responseString = Encoding.UTF8.GetString(responseBytes);
            return responseString;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Failed to PUT data from {address}");
            throw;
        }
    }

    [StringFormatMethod("urlFormat")]
    protected string PatchData(HttpClient wc, string jsonData, string urlFormat, params object[] args)
    {
        var address = string.Format(urlFormat, args);
        try
        {
            // var dataBytes = Encoding.UTF8.GetBytes(jsonData);
            var c = JsonContent.Create(jsonData);
            // HttpContent content = new ByteArrayContent(dataBytes);
            var responseBytes = wc.PatchAsync(address, c).Result;
            var responseString = responseBytes.Content.ReadAsStringAsync().Result;
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
        return JsonSerializer.Deserialize<T>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

    }
}