
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace EnterTheMatrix
{
    public class GithubServices
    {
       // private GitHubClient Client;

        public GithubServices()
        {
           //Client = new GitHubClient(new ProductHeaderValue("EnterTheMatrix"));
        }

        public List<GithubRepo> GetRepositories(string term)
        {
            string jsonString = string.Empty;
            string url = @"https://api.github.com/search/repositories?q="+term;

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.UserAgent = "EnterTheMatrix";
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream());
                jsonString = reader.ReadToEnd();
                JavaScriptSerializer js = new JavaScriptSerializer();
                GithubResponse gitResponse = js.Deserialize<GithubResponse>(jsonString);
                return gitResponse.items;
            }
            
         
        }
        
    }
}