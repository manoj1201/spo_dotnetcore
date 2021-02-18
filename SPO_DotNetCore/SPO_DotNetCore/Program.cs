using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SPO_DotNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Data> getdata = GetListData();
            foreach (Data data in getdata)
            {
                Console.WriteLine("Employee Name " + data.Title);
            }
            Console.ReadLine();
        }
        public static List<Data> GetListData()
        {
            const string DataColumn = "ID,Title";
            const string DataAPIAllData = "{0}/_api/lists/getbytitle('{1}')/items?$top=10&$select=" + DataColumn + "&$orderby=Modified desc";

            try
            {
                var results = new List<Data>();

                string sharepointSiteUrl = Convert.ToString("https://mittal1201.sharepoint.com/sites/CommSiteHub");
                if (!string.IsNullOrEmpty(sharepointSiteUrl))
                {
                    string listname = "Employee";
                    string api = string.Format(DataAPIAllData, sharepointSiteUrl, listname);
                    if (!string.IsNullOrEmpty(listname))
                    {
                        //Invoke REST Call
                        string response = TokenHelper.GetAPIResponse(api);
                        if (!String.IsNullOrEmpty(response))
                        {
                            JObject jobj = Utility.Deserialize(response);
                            JArray jarr = (JArray)jobj["d"]["results"];

                            //Write Response to Output
                            foreach (JObject j in jarr)
                            {
                                Data data = new Data();
                                data.Title = Convert.ToString(j["Title"]);

                                results.Add(data);
                            }
                        }
                        return results;
                    }
                    else
                    {
                        throw new Exception("Custom Message");
                    }
                }
                else
                {
                    throw new Exception("Custom Message");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Custom Message");
            }
        }
    }

    public class Data
    {
        public string Title { get; set; }
    }
}
