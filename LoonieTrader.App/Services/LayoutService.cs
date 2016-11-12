using Jot;
using Jot.Storage;
using LoonieTrader.Library.Services;

namespace LoonieTrader.App.Services
{
    public class LayoutService
    {
        public LayoutService()
        {
            FileReaderWriterService rws = new FileReaderWriterService();
            var fileName = rws.GetLayoutFolderPath();

            Tracker = new StateTracker { StoreFactory = new JsonFileStoreFactory(fileName) };
        }

        public StateTracker Tracker { get; private set; }
    }
}