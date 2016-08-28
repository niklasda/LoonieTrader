using System;
using System.Data.SqlTypes;
using System.IO;
using GalaSoft.MvvmLight;
using LoonieTrader.RestLibrary.Interfaces;
using Syncfusion.Windows.Tools.MVVM;

namespace LoonieTrader.App.ViewModels.Windows
{
    public class LogWindowViewModel : ViewModelBase
    {
        public LogWindowViewModel(IFileReaderWriter frw)
        {
            // _logFile = frw.GetLogFilePattern();

            // todo do not hard code
            FileSystemWatcher watcher = new FileSystemWatcher(Path.GetDirectoryName(frw.GetLogFilePattern()),
                "LTLog-*.txt");
            watcher.IncludeSubdirectories = false;
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Changed += OnChanged;
            watcher.EnableRaisingEvents = true;
        }

        //private readonly string _logFile;
        private DateTime _lastTime = DateTime.Today;

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            if (DateTime.Now > _lastTime)
            {
                LogFileInfo = e.FullPath;
                try
                {
                    using (var logFileStream = File.OpenText(LogFileInfo))
                    {
                        string log = logFileStream.ReadToEnd();
                        LogText = log;
                    }
                }
                catch (Exception ex)
                {
                    LogText = ex.Message;
                }


                _lastTime = DateTime.Now.AddSeconds(1);
            }
        }

        private string _logText;
        public string LogText
        {
            get { return _logText; }
            set
            {
                if (_logText != value)
                {
                    _logText = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _logFileInfo;
        public string LogFileInfo
        {
            get { return _logFileInfo; }
            set
            {
                if (_logFileInfo != value)
                {
                    _logFileInfo = value;
                    RaisePropertyChanged();
                }
            }
        }
    }
}