using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Windows.Input;
using System;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Text;
using System.Net;
using OfflineAnalyst.App.Models;
using SciChart.Data.Model;
using SciChart.Charting.Model.ChartSeries;
using SciChart.Charting.Model.DataSeries;
using SevenZipExtractor;

namespace OfflineAnalyst.App.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            NewChartCommand = new RelayCommand(OpenChartWindow);

            //  MovingAverage sma50 = new MovingAverage(3);
            var ds1 = new XyDataSeries<DateTime, double> { SeriesName = "3-Period SMA" };

            //ds1.Append(PriceData.XValues.Select(x => x), PriceData.CloseValues.Select(y => sma50.Push(y).Current));


            RenderableSeriesViewModels = new ObservableCollection<BaseRenderableSeriesViewModel>();

            //  RenderableSeriesViewModels.Add( new OhlcRenderableSeriesViewModel() {DataSeries = PriceData });
            RenderableSeriesViewModels.Add(new CandlestickRenderableSeriesViewModel() { DataSeries = PriceData });
            // RenderableSeriesViewModels.Add(new LineRenderableSeriesViewModel() { DataSeries = ds1 });
        }

        public ICommand NewChartCommand { get; set; }

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

        public string StatusBarLeft
        {
            get { return "Left part"; }
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

        private void OpenChartWindow()
        {
            OpenFileDialog dlg = new OpenFileDialog();


            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".bi5";
            dlg.Filter = "bi5 Files (*.bi5)|*.bi5|CSV Files (*.csv)|*.csv";


            // Display OpenFileDialog by calling ShowDialog method 
            var result = dlg.ShowDialog();


            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                string filename = dlg.FileName; // Files\EURUSD\2017\11\01
                                                // 2016.08.11 00:00:00.020,  1.1185,  1.11855,  4.42,  2.70 
                                                // 32 - bit integer: milliseconds since epoch, 
                                                // 32 - bit float: Ask price, 
                                                // 32 - bit float: Bid price,
                                                // 32 - bit float: Ask volume,
                                                // 32 - bit float: Bid volume. 
                                                // bigEndian

                var li = filename.LastIndexOf(@"\") + 1;
                var len = filename.Length;
                var hour = filename.Substring(li, 2);
                var day = filename.Substring(li - 3, 2);
                var mon = filename.Substring(li - 6, 2);
                var yr = filename.Substring(li - 11, 4);

                var baseDate = DateTime.ParseExact($"{yr}-{mon}-{day} {hour}:00:00", "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture);
            //    var baseDate = new  DateTime(yr, mon, day, );

                using (ArchiveFile archiveFile = new ArchiveFile(dlg.OpenFile(), SevenZipFormat.Lzma))
                {
                    foreach (Entry entry in archiveFile.Entries)
                    {
                        Console.WriteLine($"size {entry.Size}");

                        // extract to file
                        //  entry.Extract(entry.FileName);

                        // extract to stream
                        var memoryStream = new MemoryStream();
                        entry.Extract(memoryStream);
                        //  var buff = memoryStream.ToArray();
                        //  var buff = memoryStream.ToArray();

                        //    BitConverter.

                        BinaryReader reader = new BinaryReader(memoryStream);

                        // var len = memoryStream.Length;
                        memoryStream.Seek(0, SeekOrigin.Begin);

                        while (reader.BaseStream.Position < reader.BaseStream.Length)
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



                            //  float askv = reader.ReadSingle();
                            //                            int avv = IPAddress.HostToNetworkOrder(askv);
                            //    float x = BitConverter.ToSingle(askv, 0);

                            //     float bidv = reader.ReadSingle();
                            //                 var sec = buff[i+5];
                            //                          int bvv = IPAddress.HostToNetworkOrder(bidv);

                            var tm = new TickModel();
                            Console.WriteLine($"{milli}: ask:{aaa}, bid:{bbb}, av:{askv}, bv:{bidv}");

                        }
                    }
                }

            }
        }
    }
}