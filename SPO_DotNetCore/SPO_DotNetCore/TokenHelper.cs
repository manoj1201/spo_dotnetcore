using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace SPO_DotNetCore
{
    class TokenHelper
    {
        public static string GetAPIResponse(string url)
        {
            string response = String.Empty;
            try
            {
                //Call to get AccessToken
                string accessToken = GetSharePointAccessToken();
                //Call to get the REST API response from Sharepoint
                System.Net.HttpWebRequest endpointRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
                endpointRequest.Method = "GET";
                endpointRequest.Accept = "application/json;odata=verbose";
                endpointRequest.Headers.Add("Authorization", "Bearer " + accessToken);
                System.Net.WebResponse webResponse = endpointRequest.GetResponse();
                Stream webStream = webResponse.GetResponseStream();
                StreamReader responseReader = new StreamReader(webStream);
                response = responseReader.ReadToEnd();
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public static string GetSharePointAccessToken()
        {

            Uri site = new Uri("https://mittal1201.sharepoint.com/sites/CommSiteHub");
            string user = "sayon@mittal1201.onmicrosoft.com";
            string pwd = "mindtree@1234";
            string result;
            using (var authenticationManager = new AuthManager())
            {
                string accessTokenSP = authenticationManager.AcquireTokenAsync(site, user, pwd).Result;
                result = accessTokenSP;
            }
            return result;
        }

    }
}
