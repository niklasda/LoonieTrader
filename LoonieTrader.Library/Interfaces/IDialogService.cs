namespace LoonieTrader.Library.Interfaces
{
    public interface IDialogService
    {
        bool AskYesNo(string message);
        void WarnOk(string message);
    }
}