using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using RabbitMQ.Client;
using Newtonsoft.Json;
using Rabbit_Send_API.Models;
using System.Threading;
using System.Runtime.CompilerServices;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using Rabbit_Send_API.Models;



namespace Rabbit_Send_API.Models
{
    public class Helper
    {
        public string GetRandomWord()
        {
            const string URL = "https://api.quotable.io/random";
            string res = string.Empty;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(URL);

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage responseMessage = client.GetAsync("").Result;
            if (responseMessage.IsSuccessStatusCode)
            {
                //var dataObj = responseMessage.Content.ReadAsAsync<List<JsonContent>>().Result;
                //Quote dataObj = JsonConvert.DeserializeObject<Quote>(responseMessage.Content.ReadAsAsync);
                string jsonString = responseMessage.Content.ReadAsStringAsync().Result;
                    //.Replace("\\", "")
                                               //.Trim(new char[1] { '"' });

                //var dataObj = JsonConvert.DeserializeObject(jsonString);
                Quote contentRes = JsonConvert.DeserializeObject<Quote>(jsonString);
                res = contentRes.content;
                //if (dataObj) {
                //    var test = dataObj[1].ToString();

                //}


                //Console.Write(dataObj.ToString());
                //res = dataObj.ToString();
            }
            return res;
        }
    }
}
