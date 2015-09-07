using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace App.Droid
{
    public class ExpenseTrackerApi
    {

        /// <summary>
        /// Testing...
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<List<string>> GetGitHubReposAsync(string user)
        {
            string url = string.Format("https://api.github.com/users/{0}/repos");

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Other");
            var response = await httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            var json = JArray.Parse(content);

            var repos = new List<string>();

            foreach (var item in json)
            {
                var repository = item.Value<string>("full_name");
                repos.Add(repository);
            }

            return repos;
        }
    }
}