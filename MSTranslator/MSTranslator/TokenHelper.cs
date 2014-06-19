using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using MSTranslator.Models;
using System.Web;


namespace MSTranslator
{
    public class TokenHelper
    {
        string clientID = "";
        string AppScrect="";
        string strTranslatorAccessURI="";

        public TokenHelper(string clientID, string AppScrect, string strTranslatorAccessURI)
        {
            this.clientID = clientID;
            this.AppScrect = AppScrect;
            this.strTranslatorAccessURI = strTranslatorAccessURI;
        }

        public AuthToken GetToken()
        {
            string postcontent = string.Format("grant_type=client_credentials&client_id={0}&client_secret={1} &scope=http://api.microsofttranslator.com", HttpUtility.UrlEncode(clientID), HttpUtility.UrlEncode( AppScrect));
            HttpItem hi = new HttpItem() { Address = strTranslatorAccessURI, RequestMethod = RequestMethod.Post, HttpMsgBodyContent = postcontent, Content_Type = "application/x-www-form-urlencoded" };
            var result = new HTTPHelper(hi).HttpHelperMethod().Result;
            var token = JsonHelper.JsonDeserialize<AuthToken>(result);
            return token;
        }

        private static string GetBase64String(string code)
        {
            string encode;

            byte[] bytes = Encoding.Default.GetBytes(code);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = code;
            }
            return encode;
        }
    }


}
