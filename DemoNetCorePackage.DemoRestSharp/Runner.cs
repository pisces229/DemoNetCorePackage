using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoNetCorePackage.DemoRestSharp
{
    internal class Runner
    {
        public Runner() 
        {
            
        }
        public async Task Run() 
        {
            //{
            //    var restClientOptions = new RestClientOptions 
            //    { 
            //        BaseUrl= new Uri("https://10.10.95.30"),
            //        RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
            //        {
            //            Console.WriteLine(sslPolicyErrors.ToString());
            //            return true;
            //        }
            //    };
            //    var restClient = new RestClient(restClientOptions);
            //    var restRequest = new RestRequest("/api/test/success", Method.Get);
            //    //restRequest.AddHeader("Header", "Header");
            //    //restRequest.AddFile();
            //    var restResponse = await restClient.ExecuteAsync<string>(restRequest);
            //    Console.WriteLine($"StatusCode:{restResponse.StatusCode}");
            //    Console.WriteLine($"ContentLength:{restResponse.ContentLength}");
            //    Console.WriteLine($"Content:{restResponse.Content}");
            //    if (restResponse.IsSuccessful)
            //    {
            //        Console.WriteLine($"Data:{restResponse.Data}");
            //    }
            //}
            //{
            //    var restClientOptions = new RestClientOptions
            //    {
            //        BaseUrl = new Uri("https://10.10.95.30"),
            //        RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
            //        {
            //            Console.WriteLine(sslPolicyErrors.ToString());
            //            return true;
            //        }
            //    };
            //    var restClient = new RestClient(restClientOptions);
            //    var restRequest = new RestRequest("/api/test/success", Method.Get);
            //    var restResponse = await restClient.ExecuteAsync(restRequest);
            //    Console.WriteLine($"StatusCode:{restResponse.StatusCode}");
            //    Console.WriteLine($"ContentLength:{restResponse.ContentLength}");
            //    Console.WriteLine($"Content:{restResponse.Content}");
            //    if (restResponse.IsSuccessful)
            //    {
            //        Console.WriteLine($"RawBytes.Length:{restResponse.RawBytes?.Length}");
            //    }
            //    restClient.DownloadData(restRequest);
            //}
            {
                var restClientOptions = new RestClientOptions
                {
                    BaseUrl = new Uri("https://www.google.com.tw/"),
                    RemoteCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
                    {
                        Console.WriteLine(sslPolicyErrors.ToString());
                        return true;
                    }
                };
                var restClient = new RestClient(restClientOptions);
                var restRequest = new RestRequest("", Method.Get);
                var restResponse = await restClient.ExecuteAsync(restRequest);
                Console.WriteLine($"StatusCode:{restResponse.StatusCode}");
                Console.WriteLine($"Content:{restResponse.Content}");
                //Console.WriteLine($"Data:{ response.Data }");
            }
        }
    }
}
