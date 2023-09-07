//using System;
//using System.IO;
//using System.Net;

//namespace LoonieTrader.Library.RestApi.Requesters
//{
//    public class LoonieWebClient : IDisposable
//    {
//        public LoonieWebClient()
//        {
//            _wc = new WebClient();
//        }

//        private readonly WebClient _wc;
//        public WebHeaderCollection Headers { get { return _wc.Headers; } }

//        public void Dispose()
//        {
//            // Currently want to keep WebCLient so dont dispose them
//           // Console.WriteLine("Disposing LoonieWebClient");
//        }

//        public byte[] UploadData(string url, string method, byte[] dataBytes)
//        {
//            return _wc.UploadData(url, method, dataBytes);
//        }

//        public byte[] DownloadData(string url)
//        {
//            return _wc.DownloadData(url);
//        }

//        public Stream OpenRead(Uri uri)
//        {
//            return _wc.OpenRead(uri);
//        }
//    }
//}