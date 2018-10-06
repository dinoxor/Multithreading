using System;
using System.Net.Http;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;

namespace RestWebMatrix
{
    public class Program
    {
        static void Main(string[] args)
        {
            //http://ec2-54-241-228-71.us-west-1.compute.amazonaws.com:3000/low_priority?app_id=496036468

            const string appId1 = "496036468";
            const string appId2 = "745006435";
            const string lowPriorityRoute = "low_priority";
            const string highPriorityRoute = "high_priority";
            const string resetRoute = "reset";
            const string baseUrl = @"http://ec2-54-241-228-71.us-west-1.compute.amazonaws.com:3000/";
            const string urlParameter = "?app_id=";

            var lowPriorityUrl = $"{baseUrl}{lowPriorityRoute}";
            var highPriorityUrl = $"{baseUrl}{highPriorityRoute}";
            var resetUrl = $"{baseUrl}{resetRoute}";

            var lowPriorityClient = getClient(lowPriorityUrl);
            var highPriorityClient = getClient(highPriorityUrl);
            var resetClient = getClient(resetUrl);

            //reset            
            resetClient.GetAsync($"{urlParameter}{appId1}");
            resetClient.GetAsync($"{urlParameter}{appId2}");            

            var useDefaultAppId = true;
            var shouldSwapDefaultAppId = true;
            

            for (int apiCallCounter = 0; apiCallCounter < 850; apiCallCounter++)
            {
                HttpResponseMessage response;
                var currentAppId = useDefaultAppId ? appId1 : appId2;
                var priority = string.Empty;

                if (apiCallCounter >= 325 && apiCallCounter <=425)
                {
                    priority = "high";
                    response = executeApiCall(highPriorityClient, urlParameter, currentAppId);
                }
                else
                {
                    priority = "low";
                    response = executeApiCall(lowPriorityClient, urlParameter, currentAppId);
                }               
                
                var responseUsageHeader = getUsageHeader(response); 
                if (responseUsageHeader != null)
                {
                    if (shouldSwapDefaultAppId || responseUsageHeader.call_count > 50 || responseUsageHeader.total_cputime > 50 || responseUsageHeader.total_time > 50)
                    {
                        //swap app Id 
                        useDefaultAppId = !useDefaultAppId;
                    }
                }
                else
                {
                    //app id reached its limit
                    shouldSwapDefaultAppId = false;
                    useDefaultAppId = !useDefaultAppId;
                }


                if (response.IsSuccessStatusCode)
                {               
                    Debug.WriteLine($"{apiCallCounter} \t[{currentAppId}] \t[{priority}]: {response.StatusCode}\t call_count: {responseUsageHeader.call_count}");
                }                       

            }            
        }

        private static void callLowPriority()
        {
            for (int i = 0; i < 750; i++)
            {

            }

        }

        private static void callHighPriority()
        {
            //sleep for 15 seconds

            for (int i =0; i< 100; i++)
            {

            }


        }

        private static UsageHeader getUsageHeader (HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                var xAppUsageResponseJson = response.Headers.GetValues("X-App-Usage").FirstOrDefault();
                return JsonConvert.DeserializeObject<UsageHeader>(xAppUsageResponseJson);
            }

            return null;
            
        }

        private static HttpResponseMessage executeApiCall(HttpClient client, string url, string appId)
        {
            return client.GetAsync($"{url}{appId}").Result;
        }

        private static HttpClient getClient(string baseAddress)
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

    }
}
