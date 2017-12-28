using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;
using System;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using LoonieTrader.Library.Enums;
using LoonieTrader.Library.TimeFrames;
using SciChart.Data.Model;
using SciChart.Charting.Model.ChartSeries;
using SciChart.Charting.Model.DataSeries;
using SevenZipExtractor;
using LoonieTrader.Library.Models;
using LoonieTrader.Shared.Indicators;

namespace OfflineAnalyst.App.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            NewChartCommand = new RelayCommand(OpenChartWindow);
            CloseChartCommand = new RelayCommand(CloseChart);
            Find1Command = new RelayCommand(FindPattern1);
            Find2Command = new RelayCommand(FindPattern2);


            this.PriceData = new OhlcDataSeries<DateTime, double>();

            var ds1 = new XyDataSeries<DateTime, double> { SeriesName = "3-Period SMA" };


            RenderableSeriesViewModels = new ObservableCollection<BaseRenderableSeriesViewModel>();

            RenderableSeriesViewModels.Add(new CandlestickRenderableSeriesViewModel() { DataSeries = PriceData });
            RenderableSeriesViewModels.Add(new LineRenderableSeriesViewModel() { DataSeries = ds1 });

            XVisibleRange = new IndexRange(0, 100);
            YVisibleRange = new DoubleRange(0.2d, 1.0d);

            StatusBarLeft = "Bi5 test app";
        }

        public ICommand NewChartCommand { get; set; }
        public ICommand CloseChartCommand { get; set; }
        public ICommand Find1Command { get; set; }
        public ICommand Find2Command { get; set; }



        private IOhlcDataSeries<DateTime, double> _priceSeries;
        public IOhlcDataSeries<DateTime, double> PriceData
        {
            get { return _priceSeries; }
            set
            {
                _priceSeries = value;
                RaisePropertyChanged();
            }
        }

        private string _leftStatusText;
        public string StatusBarLeft
        {
            get { return _leftStatusText; }
            set
            {
                if (_leftStatusText != value)
                {
                    _leftStatusText = value;
                    RaisePropertyChanged();
                }
            }
        }

        private string _rightStatusText;
        public string StatusBarRight
        {
            get { return _rightStatusText; }
            set
            {
                if (_rightStatusText != value)
                {
                    _rightStatusText = value;
                    RaisePropertyChanged();
                }
            }
        }

        public string XAxisTitle
        {
            get { return "Time"; }
        }

        public string YAxisTitle
        {
            get { return "Price"; }
        }

        private IndexRange _xVisibleRange;
        public IndexRange XVisibleRange
        {
            get { return _xVisibleRange; }
            set
            {
                if (!Equals(_xVisibleRange, value))
                {
                    _xVisibleRange = value;
                    RaisePropertyChanged();
                }
            }
        }

        private DoubleRange _yVisibleRange;
        public DoubleRange YVisibleRange
        {
            get { return _yVisibleRange; }
            set
            {
                if (!Equals(_yVisibleRange, value))
                {
                    _yVisibleRange = value;
                    RaisePropertyChanged();
                }
            }
        }

        private ObservableCollection<BaseRenderableSeriesViewModel> _seriesViewModels;

        public ObservableCollection<BaseRenderableSeriesViewModel> RenderableSeriesViewModels
        {
            get { return _seriesViewModels; }
            set
            {
                _seriesViewModels = value;
                RaisePropertyChanged();
            }
        }

        private void CloseChart()
        {
            foreach (var r in RenderableSeriesViewModels)
            {
                r.DataSeries.Clear();
            }

        }

        private void FindPattern1()
        {
        }

        private void FindPattern2()
        {
        }


        private void OpenChartWindow()
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.DefaultExt = ".bi5";
            dlg.Filter = "bi5 Files (*.bi5)|*.bi5|CSV Files (*.csv)|*.csv";
            dlg.Multiselect = true;

            var result = dlg.ShowDialog();

            if (result == true)
            {
                foreach(var fileName in dlg.FileNames.OrderBy(f=>f))
                {
                    StatusBarRight = fileName;

                    using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
                    {
                        var tix = DecodeBinary(fileName, stream);
                        var ohlc = MapToTime(tix);

                        ShowData(ohlc);
                    }
                }
            }
        }

        private void ShowData(OhlcListModel ohlc)
        {
            var ylow = ohlc.OhlcList.Max(o => o.Low);
            var ymax = ohlc.OhlcList.Max(o => o.High);

            

            PriceData.SeriesName = ohlc.Ticker;



            PriceData.Append(ohlc.OhlcList.Select(o => o.DatePlusTime).ToList(), ohlc.OhlcList.Select(o => o.Open).ToList(), ohlc.OhlcList.Select(o => o.High).ToList(), ohlc.OhlcList.Select(o => o.Low).ToList(), ohlc.OhlcList.Select(o => o.Close).ToList());

            MovingAverage sma50 = new MovingAverage(3);

            var ts = (XyDataSeries<DateTime, double>) RenderableSeriesViewModels[1].DataSeries;
            ts.Append(ohlc.OhlcList.Select(o => o.DatePlusTime).ToList(), ohlc.OhlcList.Select(o => o.Close).Select(y => sma50.Push(y).Current));

            XVisibleRange.SetMinMax(0, PriceData.Count);
            YVisibleRange.SetMinMax(ylow - 0.01, ymax + 0.01);
        }

        private TickListModel DecodeBinary(string filename, Stream fileStream)
        {
            // = dlg.FileName; // Files\EURUSD\2017\11\01
            // 2016.08.11 00:00:00.020,  1.1185,  1.11855,  4.42,  2.70 
            // 32 - bit integer: milliseconds since epoch, 
            // 32 - bit float: Ask price, 
            // 32 - bit float: Bid price,
            // 32 - bit float: Ask volume,
            // 32 - bit float: Bid volume. 
            // bigEndian
            // AUSUSD ~   0,75
            // EURUSD ~   1,18
            // GBPUSD ~   1,34
            // NZDUSD ~   0,68
            // USDCAD ~   1,29
            // USDCHF ~   0,99
            // USDJPY ~ 113,54

            var li = filename.LastIndexOf(@"\") + 1;
            var len = filename.Length;
            var hour = filename.Substring(li, 2);
            var day = filename.Substring(li - 3, 2);
            var mon = filename.Substring(li - 6, 2);
            var yr = filename.Substring(li - 11, 4);

            var ccy = filename.Substring(li - 18, 6);

            var baseDate = DateTime.ParseExact($"{yr}-{mon}-{day} {hour}:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture);

            decimal tickDivider = 100000m;

            if (ccy == "USDJPY")
            {
                tickDivider = 1000m;
            }

            TickListModel tickList = new TickListModel();
            tickList.Ticker = ccy;

            using (ArchiveFile archiveFile = new ArchiveFile(fileStream, SevenZipFormat.Lzma))
            {
                Entry entry = archiveFile.Entries.FirstOrDefault();

                if (entry != null)
                {
                    var memoryStream = new MemoryStream();
                    entry.Extract(memoryStream);

                    BinaryReader reader = new BinaryReader(memoryStream);

                    memoryStream.Seek(0, SeekOrigin.Begin);

                    while (memoryStream.Position < memoryStream.Length)
                    {
                        int msec = reader.ReadInt32();
                        int milli = IPAddress.HostToNetworkOrder(msec);

                        var tickDate = baseDate.AddMilliseconds(milli);

                        int ask = reader.ReadInt32();
                        int aaa = IPAddress.HostToNetworkOrder(ask);

                        int bid = reader.ReadInt32();
                        int bbb = IPAddress.HostToNetworkOrder(bid);

                        byte[] floatAskBytes = reader.ReadBytes(4);
                        Array.Reverse(floatAskBytes);
                        float askv = BitConverter.ToSingle(floatAskBytes, 0);


                        byte[] floatBidBytes = reader.ReadBytes(4);
                        Array.Reverse(floatBidBytes);
                        float bidv = BitConverter.ToSingle(floatBidBytes, 0);


                        var tm = new TickModel();
                        tm.TimeStamp = tickDate;
                        tm.Milli = msec;
                        tm.Ask = aaa / tickDivider;
                        tm.AskVolume = (decimal)askv;
                        tm.Bid = bbb / tickDivider;
                        tm.BidVolume = (decimal)bidv;

                        // Console.WriteLine($"{tickDate.ToString("HH:mm:ss.fff")}: ask:{aaa}, bid:{bbb}, av:{askv}, bv:{bidv}");
                        tickList.TickList.Add(tm);
                        Console.WriteLine($"{tm}");
                    }

                    return tickList;
                }

                Console.WriteLine($"Number of ticks: {tickList.TickList.Count}, s/h: {60 * 60}, s/d: {60 * 60 * 24}");

            }

            return null;
        }

        private OhlcListModel MapToTime(TickListModel tickList)
        {
            var m15 = TimeFrameFactory.Create15Minutes();
            OhlcListModel ohlcList = m15.ConvertTime(tickList, PricePointType.Bid);

            return ohlcList;
        }
    }
}