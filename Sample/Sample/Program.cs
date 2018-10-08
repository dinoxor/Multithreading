using System.Diagnostics;
using System.Collections.Generic;
using System.Threading;

namespace Sample
{
    class Program
    {
        public static List<string> _lowList;
        public static List<string> _highList;

        static void Main(string[] args)
        {
            _lowList = new List<string>();
            _highList = new List<string>();

            Thread lowThread = new Thread(new ThreadStart(callLow));
            Thread highThread = new Thread(new ThreadStart(callHigh));
            Thread printThread = new Thread(new ThreadStart(PrintNumber));

            lowThread.Start();
            printThread.Start();

            Thread.Sleep(100);
            highThread.Start();            
        }

        public static void PrintNumber()
        {
            while (_lowList.Count > 0 || _highList.Count > 0)
            {
                var value = string.Empty;

                if (_highList.Count > 0)
                {
                    value = _highList[0];
                    _highList.RemoveAt(0);
                }
                else
                {
                    value = _lowList[0];
                    _lowList.RemoveAt(0);
                }               

                Debug.WriteLine(value);
            }
        }

        public static void callLow ()
        {
            for (int i = 0; i < 500; i++)
            {
                _lowList.Add(i.ToString());
            }
        }

        public static void callHigh()
        {
            for (int i = 0; i < 50; i++)
            {
                _highList.Add($"HIGH{i}");
            }

            Thread.Sleep(100);

            for (int i = 51; i < 100; i++)
            {
                _highList.Add($"HIGH{i}");
            }

        }

        public class SomeObj
        {
            public string name { get; set; }
            public string value { get; set; }

        }


    }
}
