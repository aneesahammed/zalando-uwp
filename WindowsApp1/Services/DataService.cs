using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WindowsApp1.Models;

namespace WindowsApp1.Services
{
    public interface IDataService
    {
        Task<RootObject> GetData(string query,bool isResetRequested=false);
    }

    public class DataService : IDataService
    {
        private string _language;
        public DataService()
        {
            _language = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        }

        private static int _pageIndex = 1;
        public async Task<RootObject> GetData(string query, bool isResetRequested=false)
        {   
            if (isResetRequested)
                _pageIndex = 1;

#if DEBUG
            System.Diagnostics.Debug.WriteLine(string.Format("PAGE-INDEX-->{0}", _pageIndex));
#endif

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Accept-Language", _language);

                HttpResponseMessage httpResponseMessage;
                if (string.IsNullOrEmpty(query))
                {
                    httpResponseMessage = await httpClient.GetAsync($"https://api.zalando.com/articles?page={_pageIndex}&pageSize=20");                   
                }
                else
                {
                    httpResponseMessage = await httpClient.GetAsync($"https://api.zalando.com/articles{query}&page={_pageIndex}&pageSize=20");
                }

                _pageIndex++;
                var json = await httpResponseMessage.Content.ReadAsStringAsync();

                var requestInfo = JsonConvert.DeserializeObject<RootObject>(json);
                return requestInfo;
            }
        }
    }    
}
