using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace LoonieTrader.Library.Models
{
    public class ObservableStream
    {
        public ObservableStream(Stream stream)
        {
            _stream = stream;
        }

        private readonly Stream _stream;

        public IObservable<string> GetObservable()
        {
            return ReadLines(_stream).ToObservable(Scheduler.Default);
        }

        private IEnumerable<string> ReadLines(Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                while (!reader.EndOfStream)
                {
                    yield return reader.ReadLine();
                }
            }
        }
    }
}