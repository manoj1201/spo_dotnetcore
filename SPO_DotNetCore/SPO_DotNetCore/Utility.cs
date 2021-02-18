using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SPO_DotNetCore
{
    public static class Utility
    {
        public static JObject Deserialize(string response)
        {
            dynamic deSerailizedObj;
            byte[] jsonBytes = Encoding.UTF8.GetBytes(response);
            using (var stream = new MemoryStream(jsonBytes))
            {
                using (var sr = new StreamReader(stream))
                {
                    using (var reader = new JsonTextReader(sr))
                    {
                        var serializer = new JsonSerializer
                        {
                            MissingMemberHandling = MissingMemberHandling.Ignore,

                            NullValueHandling = NullValueHandling.Ignore
                        };

                        deSerailizedObj = serializer.Deserialize<dynamic>(reader);
                    }
                }

            }
            return (JObject)deSerailizedObj;
        }
    }
}
