using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MSTranslator
{
    class JsonHelper
    {
        public static String ToJson(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
        public static T JsonDeserialize<T>(String jsonStr)
        {
            try
            {
                var t = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(jsonStr);
                return t;
            }
            catch {
                return default(T);
            }
        }
    }
}
