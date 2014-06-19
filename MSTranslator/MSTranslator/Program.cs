using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSTranslator.Models;
using System.Xml;

namespace MSTranslator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string txtToTranslate = @"Hello World";
            string clientID = "fengye87626";
            string AppScrect = "uPQ1isBzB5sRBZhsyYQ1l3j2pmT5zZzYq6I9UCrHZAo=";
            string strTranslatorAccessURI = @"https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";
            var tokenHelper = new TokenHelper(clientID,AppScrect,strTranslatorAccessURI);
            var token=tokenHelper.GetToken();
            //Console.WriteLine(token.access_token);

            while(true)
            {
            Console.Write("English Sentence: ");
            txtToTranslate = Console.ReadLine();
            string uri = "http://api.microsofttranslator.com/v2/Http.svc/Translate?text=" + System.Web.HttpUtility.UrlEncode(txtToTranslate) + "&from=en&to=zh-CHS";
            HttpItem hi = new HttpItem() { Address = uri, Authorization = "Bearer " + token.access_token, RequestMethod = RequestMethod.Get };
            var mm = new HTTPHelper(hi).HttpHelperMethod().Result;
            XmlDocument xd = new XmlDocument();
            xd.LoadXml(mm);
            Console.WriteLine(xd.InnerText );
            //Console.WriteLine("MS Translater: "+mm);
            }
            Console.ReadKey();
        }
    }
}
