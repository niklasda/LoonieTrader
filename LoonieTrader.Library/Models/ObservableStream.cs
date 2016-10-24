using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Jil;
using LoonieTrader.Library.Interfaces;
using LoonieTrader.Library.RestApi.Interfaces;

namespace LoonieTrader.Library.Models
{
    public class StreamEventArgs<TP> : EventArgs where TP : IHeartbeatStreamable
    {
        public StreamEventArgs(TP obj)
        {
            Obj = obj;
        }

        public TP Obj { get; private set; }
    }

    public delegate void NewValueEventHandler<TS>(object sender, StreamEventArgs<TS> e) where TS : IHeartbeatStreamable;

    public class ObservableStream<T> where T : IHeartbeatStreamable
    {
        public ObservableStream(Stream stream, IExtendedLogger logger)
        {
            _stream = stream;
            _logger = logger;

            var obs = GetObservable();
            obs.Subscribe(x => OnChanged(new StreamEventArgs<T>(x)));
        }

       // private IObservable<T> _obs;
        private readonly Stream _stream;
        private readonly IExtendedLogger _logger;

        public event NewValueEventHandler<T> NewValue;

        private void OnChanged(StreamEventArgs<T> e)
        {
            NewValue?.Invoke(this, e);
        }

        private IObservable<T> GetObservable()
        {
            return ReadLines(_stream).ToObservable(Scheduler.Default);
        }

        private IEnumerable<T> ReadLines(Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    T obj = JSON.Deserialize<T>(line);

                    _logger.Information("Stream observation: {0}", line);


                    yield return obj;
                }
            }
        }
    }
}