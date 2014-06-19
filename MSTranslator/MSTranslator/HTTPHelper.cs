using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.IO;

namespace MSTranslator
{
    public class HTTPHelper : IDisposable
    {
        HttpItem httpItem;

        public HTTPHelper(HttpItem item)
        {
            httpItem = item;
        }

        public async Task<string> HttpHelperMethod()
        {
            string responseresult = "";
            Uri target = new Uri(httpItem.Address);
            HttpWebRequest webRequest = WebRequest.Create(target) as HttpWebRequest;
            webRequest.Method = httpItem.RequestMethod.ToString();
            webRequest.ContentType = httpItem.Content_Type;
            webRequest.Headers["Authorization"] = httpItem.Authorization;
            webRequest.AllowAutoRedirect = true;
            if (httpItem.RequestMethod == RequestMethod.Post | httpItem.RequestMethod==RequestMethod.Put)
            {
                using (StreamWriter requestWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    requestWriter.Write(httpItem.HttpMsgBodyContent);
                    requestWriter.Flush();
                }
            }

            var result = await webRequest.GetResponseAsync().ContinueWith(GetResponseTask =>
            {
                try
                {
                    if (!GetResponseTask.IsFaulted)
                    {
                        HttpWebResponse httpWebResp = GetResponseTask.Result as HttpWebResponse;
                        //Console.WriteLine((int)httpWebResp.StatusCode + httpWebResp.StatusDescription);
                        StreamReader reader = new StreamReader(GetResponseTask.Result.GetResponseStream());
                        if (reader != null)
                        {
                            responseresult = reader.ReadToEnd();
                            //TODO: analysis the result and put the href into the list 
                            //Console.WriteLine(responseresult);
                        }
                    }
                    else
                    {
                        try
                        {
                            var tmp = GetResponseTask.Result as HttpWebResponse;
                        }
                        catch (Exception ex)
                        {
                            var t = ((WebException)ex.InnerException).Response as HttpWebResponse;
                            Console.WriteLine(t.StatusDescription);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
                return responseresult;
            });
            return result;

            /*
            try
            {
                var hello = webRequest.GetResponse();
                StreamReader reader = new StreamReader(hello.GetResponseStream());
                if (reader != null)
                {
                    responseresult = reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                var twe = ex.Response as HttpWebResponse;
                Console.WriteLine(twe.StatusDescription.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return responseresult;
            */
        }

        public void Dispose()
        {
            //TODO:
        }



    }
}
