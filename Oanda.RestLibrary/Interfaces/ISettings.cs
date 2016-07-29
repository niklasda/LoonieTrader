namespace LoonieTrader.RestLibrary.Interfaces
{
    public interface ISettings
    {
        string Environment { get; set; }
        string ApiKey { get; set; }
        string DefaultAccountId { get; set; }
        string UserId { get; set; }
        string[] FavouriteInstruments { get; set; }
    }
}