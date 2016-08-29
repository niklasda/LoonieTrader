using System;
using System.Collections.ObjectModel;
using System.Data.SqlTypes;
using System.IO;
using GalaSoft.MvvmLight;
using LoonieTrader.RestLibrary.Caches;
using LoonieTrader.RestLibrary.Configuration;
using LoonieTrader.RestLibrary.Interfaces;
using Syncfusion.Windows.Tools.MVVM;

namespace LoonieTrader.App.ViewModels.Windows
{
    public class LogWindowViewModel : ViewModelBase
    {
        public LogWindowViewModel()
        {
            // _logFile = frw.GetLogFilePattern();

            //// todo do not hard code
            //FileSystemWatcher watcher = new FileSystemWatcher(Path.GetDirectoryName(frw.GetLogFilePattern()),
            //    "LTLog-*.txt");
            //watcher.IncludeSubdirectories = false;
            //watcher.NotifyFilter = NotifyFilters.LastWrite;
            //watcher.Changed += OnChanged;
            //watcher.EnableRaisingEvents = true;
        }

        //private readonly string _logFile;
        //private DateTime _lastTime = DateTime.Today;

        //private void OnChanged(object source, FileSystemEventArgs e)
        //{
        //    if (DateTime.Now > _lastTime)
        //    {
        //        LogFileInfo = e.FullPath;
        //        try
        //        {
        //            using (var logFileStream = File.OpenText(LogFileInfo))
        //            {
        //                string log = logFileStream.ReadToEnd();
        //                LogText = log;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            LogText = ex.Message;
        //        }


        //        _lastTime = DateTime.Now.AddSeconds(1);
        //    }
        //}

        //private string _logText;
        public ObservableCollection<LogEntry> LogEntries
        {
            get { return LogCache.LogEntries; }
        }
    }
}