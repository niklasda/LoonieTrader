namespace Oanda.RestLibrary.Interfaces
{
    public interface ISettings
    {
        string Environment { get; set; }
        string ApiKey { get; set; }
        string DefaultAccount { get; set; }
    }
}