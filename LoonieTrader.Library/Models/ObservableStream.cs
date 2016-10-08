using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using Jil;
using LoonieTrader.Library.RestApi.Responses;

namespace LoonieTrader.Library.Models
{
    public class ObservableStream<T>
    {
        public ObservableStream(Stream stream)
        {
            _stream = stream;
        }

        private readonly Stream _stream;

        public IObservable<T> GetObservable()
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

                    yield return obj;
                }
            }
        }
    }
}