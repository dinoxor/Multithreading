using System;
using System.Net.Http;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;
using System.Threading;
using System.Collections.Generic;

namespace RestWebMatrix
{
    public class Program
    {
        private static List<string> _appIds = new List<string> { "496036468", "745006435" };
        private static int _currentAppIdIndex = 0;
        private const string _urlParameter = "?app_id=";        
        private static string _currentAppId = string.Empty;

        private static HttpClient _lowPriorityClient;
        private static HttpClient _highPriorityClient;
        private static HttpClient _resetClient;

        private static List<Request> _lowPriorityRequests = new List<Request>();
        private static List<Request> _highPriorityRequests = new List<Request>();

        static void Main(string[] args)
        {
            //Low priority requests will execute in order it was received
            //After 20 seconds, 100 high priority requests will execute 
            //Remaining low priority request will run for an additional 20 seconds before another 20 high priority requests will execute
            
            Initialize();                     

            //reset both App Ids 
            foreach (var appIds in _appIds)
            {
                _resetClient.GetAsync($"{_urlParameter}{appIds}");
            }            

            Thread lowPriorityThread = new Thread(new ThreadStart(QueueLowPriority));
            Thread highPriorityThread = new Thread(new ThreadStart(QueueHighPriority));
            Thread executeThread = new Thread(new ThreadStart(ExecuteRequests));

            lowPriorityThread.Start();
            executeThread.Start();

            Thread.Sleep(20000);
            highPriorityThread.Start();
        }

        private static void Initialize()
        {          
            const string lowPriorityRoute = "low_priority";
            const string highPriorityRoute = "high_priority";
            const string resetRoute = "reset";
            const string baseUrl = @"http://ec2-54-241-228-71.us-west-1.compute.amazonaws.com:3000/";
            
            var lowPriorityUrl = $"{baseUrl}{lowPriorityRoute}";
            var highPriorityUrl = $"{baseUrl}{highPriorityRoute}";
            var resetUrl = $"{baseUrl}{resetRoute}";

            _lowPriorityClient = GetClient(lowPriorityUrl);
            _highPriorityClient = GetClient(highPriorityUrl);
            _resetClient = GetClient(resetUrl);

            _currentAppId = _appIds[_currentAppIdIndex];
        }

        private static void QueueLowPriority()
        {
            for (int i = 0; i < 750; i++)
            {
                var request = new Request
                {
                    Client = _lowPriorityClient,
                    Url = _urlParameter
                };

                _lowPriorityRequests.Add(request);
            }
        }

        private static void QueueHighPriority()
        {
            for (int i = 0; i < 100; i++)
            {
                var request = new Request
                {
                    Client = _highPriorityClient,
                    Url = _urlParameter
                };

                _highPriorityRequests.Add(request);
            }

            Thread.Sleep(20000);

            for (int i = 0; i < 10; i++)
            {
                var request = new Request
                {
                    Client = _highPriorityClient,
                    Url = _urlParameter
                };

                _highPriorityRequests.Add(request);
            }
        }

        private static void ExecuteRequests()
        {
            //Executes in FIFO queue
            var counter = 0;
            var highCounter = 0;

            while (_lowPriorityRequests.Count > 0 || _highPriorityRequests.Count > 0)
            {                
                var priority = string.Empty;
                //Request request = null;

                List<Request> request = new List<Request>();

                //Higher priority will have execution precedences 
                if (_highPriorityRequests.Count > 0)
                {
                    //TO DO: may need to lock due to race condition
                    priority = $"high:{++highCounter}";

                    request = _highPriorityRequests;//pass by reference
                }
                else
                {
                    priority = "low";

                    request = _lowPriorityRequests;
                }

                var firstRequest = request[0];

                var response = firstRequest.Client.GetAsync($"{firstRequest.Url}{_currentAppId}").Result;
                var responseUsageHeader = GetUsageHeader(response);
                LoadBalanceAppId(responseUsageHeader);

                if (responseUsageHeader != null)//if successful
                {
                    counter++;
                    Debug.WriteLine($"{counter}\t[{_currentAppId}] \t[{priority}]: {response.StatusCode}\t call_count: {responseUsageHeader.call_count}");
                    request.RemoveAt(0);
                }                                  
            }
        }

        private static void LoadBalanceAppId(UsageHeader responseUsageHeader)
        {
            if (responseUsageHeader != null)
            {
                if (responseUsageHeader.call_count > 50 || responseUsageHeader.total_cputime > 50 || responseUsageHeader.total_time > 50)
                {
                    _currentAppId = GetNextAvailableAppId(_currentAppIdIndex);
                }
            }
            else
            {
                //app id reached its limit and throws an error
                _currentAppId = GetNextAvailableAppId(_currentAppIdIndex);
            }
        }

        private static string GetNextAvailableAppId(int currentAppIdIndex)
        {
            var maxIndex = _appIds.Count-1;
            currentAppIdIndex++;

            //if last index, get the first index
            if (currentAppIdIndex > maxIndex)
            {
                _currentAppIdIndex = 0;
                return _appIds[_currentAppIdIndex];
            }
            else
            {
                //get the next index
                _currentAppIdIndex = currentAppIdIndex;
                return _appIds[_currentAppIdIndex];
            }
        }

        private static UsageHeader GetUsageHeader(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                //extract and deserialize Usage header
                var xAppUsageResponseJson = response.Headers.GetValues("X-App-Usage").FirstOrDefault();
                return JsonConvert.DeserializeObject<UsageHeader>(xAppUsageResponseJson);
            }

            return null;
        }       

        private static HttpClient GetClient(string baseAddress)
        {
            var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(
               new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            client.BaseAddress = new Uri(baseAddress);

            return client;
        }

        public class UsageHeader
        {
            public int call_count { get; set; }
            public int total_time { get; set; }
            public int total_cputime { get; set; }
        }

        public class Request
        {
            public HttpClient Client { get; set; }
            public string Url { get; set; }
        }
    }
}
