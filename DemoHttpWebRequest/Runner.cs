using System.Net;
using System.Text;
using System.Text.Json;

namespace DemoHttpWebRequest
{
    internal class Runner
    {
        public Runner() 
        {
        }
        public async Task Run()
        {
            await Get();
            await PostJson();
            await PostFile();
            await GetDownload();
            await PostDownload();
        }
        private async Task Get()
        {
            //var requestUriString = "https://localhost:44359/api/test/GetQueryString?A=AAA&B=BBB";
            var requestUriString = "https://localhost:44359/api/test/GetQueryStringModel?A=AAA&B=BBB";

            var httpWebRequest = WebRequest.Create(requestUriString) as HttpWebRequest;
            httpWebRequest.Method = WebRequestMethods.Http.Get;
            httpWebRequest.KeepAlive = true;
            httpWebRequest.Timeout = 900000;

            //using (var requestStream = httpWebRequest.GetRequestStream())
            //{
            //    requestStream.Write(jsonBytes, 0, jsonBytes.Length);
            //    requestStream.Flush();
            //}

            using (var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                if (httpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    using (var stream = httpWebResponse.GetResponseStream())
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            var result = await reader.ReadToEndAsync();
                            Console.WriteLine(result);
                        }
                    }
                }
                else
                {
                    Console.WriteLine(httpWebResponse.StatusCode);
                }
            }
        }
        private async Task PostJson()
        {
            //var requestUriString = "https://localhost:44359/api/test/ReadPostBody";
            //var requestUriString = "https://localhost:44359/api/test/PostJson";
            var requestUriString = "https://localhost:44359/api/test/PostJsonModel";

            var jsonModel = new
            {
                A = "AAAA",
                B = "BBBB"
            };
            var jsonString = JsonSerializer.Serialize(jsonModel);
            var jsonByteArray = Encoding.UTF8.GetBytes(jsonString);

            var httpWebRequest = WebRequest.Create(requestUriString) as HttpWebRequest;
            httpWebRequest.Headers.Add("token", "1qaz2wsx");
            httpWebRequest.Method = WebRequestMethods.Http.Post;
            httpWebRequest.KeepAlive = true;
            httpWebRequest.Timeout = 900000;
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.ContentLength = jsonByteArray.Length;

            using (var requestStream = httpWebRequest.GetRequestStream())
            {
                requestStream.Write(jsonByteArray, 0, jsonByteArray.Length);
                requestStream.Flush();
            }

            using (var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                if (httpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    using (var stream = httpWebResponse.GetResponseStream())
                    {
                        using (var reader = new StreamReader(stream))
                        {
                            var result = await reader.ReadToEndAsync();
                            Console.WriteLine(result);
                        }
                    }
                }
                else
                {
                    Console.WriteLine(httpWebResponse.StatusCode);
                }
            }
        }
        private async Task PostFile()
        {
            //var requestUriString = "https://localhost:44359/api/test/ReadPostForm";
            //var requestUriString = "https://localhost:44359/api/test/PostFile";
            var requestUriString = "https://localhost:44359/api/test/PostFileModel";

            //var directoryInfo = new DirectoryInfo(Path.Combine("temp", Guid.Empty.ToString() + ".txt"));
            var directoryInfo = new DirectoryInfo(Path.Combine("temp", "largeFile.txt"));

            var filename = Path.GetFileName(directoryInfo.FullName);

            var postParameters = new Dictionary<string, object>();
            postParameters.Add("A", "AAA");
            postParameters.Add("B", "BBB");

            using (var fileStream = new FileStream(directoryInfo.FullName, FileMode.Open, FileAccess.Read))
            {
                var data = new byte[fileStream.Length];
                fileStream.Read(data, 0, data.Length);
                postParameters.Add("FILE", new FormUpload.FileParameter(data, filename, "application/octet-stream"));
            }
            //using (var fileStream = new FileStream(directoryInfo.FullName, FileMode.Open, FileAccess.Read))
            //{
            //    var data = new byte[fileStream.Length];
            //    fileStream.Read(data, 0, data.Length);
            //    postParameters.Add("FILE_A", new FormUpload.FileParameter(data, filename, "application/octet-stream"));
            //}
            //using (var fileStream = new FileStream(directoryInfo.FullName, FileMode.Open, FileAccess.Read))
            //{
            //    var data = new byte[fileStream.Length];
            //    fileStream.Read(data, 0, data.Length);
            //    postParameters.Add("FILE_B", new FormUpload.FileParameter(data, filename, "application/octet-stream"));
            //}

            var postURL = requestUriString;
            var userAgent = "Someone";
            var httpWebResponse = FormUpload.MultipartFormDataPost(
                postURL,
                userAgent,
                postParameters);

            if (httpWebResponse.StatusCode == HttpStatusCode.OK)
            {
                using (var responseReader = new StreamReader(httpWebResponse.GetResponseStream()))
                {
                    var responseString = await responseReader.ReadToEndAsync();
                    Console.WriteLine(responseString);
                }
            }
            else
            {
                Console.WriteLine(httpWebResponse.StatusCode);
            }
        }
        private async Task GetDownload()
        {
            var requestUriString = "https://localhost:44359/api/test/GetDownload?EXISTS=N";

            var httpWebRequest = WebRequest.Create(requestUriString) as HttpWebRequest;
            httpWebRequest.Headers.Add("token", "1qaz2wsx");
            httpWebRequest.Method = WebRequestMethods.Http.Get;
            httpWebRequest.KeepAlive = true;
            httpWebRequest.Timeout = 900000;
            httpWebRequest.ContentType = "application/json";

            //using (var requestStream = request.GetRequestStream())
            //{
            //    requestStream.Write(jsonByteArray, 0, jsonByteArray.Length);
            //    requestStream.Flush();
            //}

            using (var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                if (httpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    //foreach (string key in response.Headers.Keys)
                    //{
                    //    Console.WriteLine(key + ":" + response.Headers[key]);
                    //}

                    var contentType = httpWebResponse.ContentType;

                    if ("application/download".Equals(contentType))
                    {
                        var savepath = Path.Combine("save", httpWebResponse.Headers["filename"]);

                        // copy stream
                        using (var fileStream = File.Create(savepath))
                        using (var responseStream = httpWebResponse.GetResponseStream())
                        {
                            responseStream.CopyTo(fileStream);
                        }

                        //// write buffer
                        //using (var fileStream = File.Create(savepath))
                        //using (var responseStream = response.GetResponseStream())
                        //{
                        //    var buffer = new byte[8192];
                        //    var readlength = 0;
                        //    while ((readlength = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                        //    {
                        //        fileStream.Write(buffer, 0, readlength);
                        //    }
                        //}
                    }
                    else if ("application/json".Equals(contentType))
                    {
                        using (var responseReader = new StreamReader(httpWebResponse.GetResponseStream()))
                        {
                            var responseString = await responseReader.ReadToEndAsync();
                            Console.WriteLine(responseString);
                        }
                    }
                    else
                    {
                        Console.WriteLine(contentType);
                    }
                }
                else
                {
                    Console.WriteLine(httpWebResponse.StatusCode);
                }
            }
        }
        private async Task PostDownload()
        {
            var requestUriString = "https://localhost:44359/api/test/PostDownload";
            //var requestUriString = "https://172.31.93.23/api/test/PostDownload";

            var jsonModel = new
            {
                EXISTS = "N"
            };
            var jsonString = JsonSerializer.Serialize(jsonModel);
            var jsonByteArray = Encoding.UTF8.GetBytes(jsonString);

            var httpWebRequest = WebRequest.Create(requestUriString) as HttpWebRequest;
            httpWebRequest.Headers.Add("token", "1qaz2wsx");
            httpWebRequest.Method = WebRequestMethods.Http.Post;
            httpWebRequest.KeepAlive = true;
            httpWebRequest.Timeout = 900000;
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.ContentLength = jsonByteArray.Length;

            using (var requestStream = httpWebRequest.GetRequestStream())
            {
                requestStream.Write(jsonByteArray, 0, jsonByteArray.Length);
                requestStream.Flush();
            }

            using (var httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
            {
                if (httpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    //foreach (string key in response.Headers.Keys)
                    //{
                    //    Console.WriteLine(key + ":" + response.Headers[key]);
                    //}

                    var contentType = httpWebResponse.ContentType;

                    if ("application/download".Equals(contentType))
                    {
                        var savepath = Path.Combine("save", httpWebResponse.Headers["filename"]);

                        // copy stream
                        using (var fileStream = File.Create(savepath))
                        using (var responseStream = httpWebResponse.GetResponseStream())
                        {
                            responseStream.CopyTo(fileStream);
                        }

                        //// write buffer
                        //using (var fileStream = File.Create(savepath))
                        //using (var responseStream = response.GetResponseStream())
                        //{
                        //    var buffer = new byte[8192];
                        //    var readlength = 0;
                        //    while ((readlength = responseStream.Read(buffer, 0, buffer.Length)) > 0)
                        //    {
                        //        fileStream.Write(buffer, 0, readlength);
                        //    }
                        //}
                    }
                    else if ("application/json".Equals(contentType))
                    {
                        using (var responseReader = new StreamReader(httpWebResponse.GetResponseStream()))
                        {
                            var responseString = await responseReader.ReadToEndAsync();
                            Console.WriteLine(responseString);
                        }
                    }
                    else
                    {
                        Console.WriteLine(contentType);
                    }
                }
                else
                {
                    Console.WriteLine(httpWebResponse.StatusCode);
                }
            }
        }
        #region FormUpload
        public static class FormUpload
        {
            private static readonly Encoding encoding = Encoding.UTF8;
            public static HttpWebResponse MultipartFormDataPost(string postUrl, string userAgent,
                Dictionary<string, object> postParameters)
            {
                var formDataBoundary = string.Format("----------{0:N}", Guid.NewGuid());
                var contentType = "multipart/form-data; boundary=" + formDataBoundary;
                var formData = GetMultipartFormData(postParameters, formDataBoundary);
                return PostForm(postUrl, userAgent, contentType, formData);
            }
            private static HttpWebResponse PostForm(string postUrl, string userAgent, string contentType, byte[] formData)
            {
                var httpWebRequest = WebRequest.Create(postUrl) as HttpWebRequest;
                if (httpWebRequest == null)
                {
                    throw new NullReferenceException("request is not a http request");
                }
                // Set up the request properties.
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentType = contentType;
                httpWebRequest.UserAgent = userAgent;
                httpWebRequest.CookieContainer = new CookieContainer();
                httpWebRequest.ContentLength = formData.Length;
                // You could add authentication here as well if needed:
                //request.PreAuthenticate = true;
                //request.AuthenticationLevel = AuthenticationLevel.MutualAuthRequested;
                //request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes("USER" + ":" + "PASSWORD")));
                // Send the form data to the request.
                using (var requestStream = httpWebRequest.GetRequestStream())
                {
                    requestStream.Write(formData, 0, formData.Length);
                }
                return httpWebRequest.GetResponse() as HttpWebResponse;
            }

            private static byte[] GetMultipartFormData(Dictionary<string, object> postParameters, string boundary)
            {
                byte[] formData;
                using (var formDataStream = new MemoryStream())
                {
                    var needsCLRF = false;
                    foreach (var param in postParameters)
                    {
                        // Thanks to feedback from commenters, add a CRLF to allow multiple parameters to be added.
                        // Skip it on the first parameter, add it to subsequent parameters.
                        if (needsCLRF)
                        {
                            formDataStream.Write(encoding.GetBytes("\r\n"), 0, encoding.GetByteCount("\r\n"));
                        }
                        needsCLRF = true;
                        if (param.Value is FileParameter)
                        {
                            var fileToUpload = (FileParameter)param.Value;

                            // Add just the first part of this param, since we will write the file data directly to the Stream
                            var header = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"; filename=\"{2}\"\r\nContent-Type: {3}\r\n\r\n",
                                boundary,
                                param.Key,
                                fileToUpload.FileName ?? param.Key,
                                fileToUpload.ContentType ?? "application/octet-stream");

                            formDataStream.Write(encoding.GetBytes(header), 0, encoding.GetByteCount(header));

                            // Write the file data directly to the Stream, rather than serializing it to a string.
                            formDataStream.Write(fileToUpload.File, 0, fileToUpload.File.Length);
                        }
                        else
                        {
                            var postData = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"{1}\"\r\n\r\n{2}",
                                boundary,
                                param.Key,
                                param.Value);
                            formDataStream.Write(encoding.GetBytes(postData), 0, encoding.GetByteCount(postData));
                        }
                    }
                    // Add the end of the request.  Start with a newline
                    var footer = "\r\n--" + boundary + "--\r\n";
                    formDataStream.Write(encoding.GetBytes(footer), 0, encoding.GetByteCount(footer));
                    // Dump the Stream into a byte[]
                    formDataStream.Position = 0;
                    formData = new byte[formDataStream.Length];
                    formDataStream.Read(formData, 0, formData.Length);
                }

                return formData;
            }
            public class FileParameter
            {
                public byte[] File { get; set; }
                public string FileName { get; set; }
                public string ContentType { get; set; }
                public FileParameter(byte[] file) : this(file, null)
                {
                }
                public FileParameter(byte[] file, string filename) : this(file, filename, null)
                {
                }
                public FileParameter(byte[] file, string filename, string contenttype)
                {
                    File = file;
                    FileName = filename;
                    ContentType = contenttype;
                }
            }
        }
        #endregion
    }
}
