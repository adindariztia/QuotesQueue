using Microsoft.AspNetCore.SignalR;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ConsumerWebApp.Models;

namespace ConsumerWebApp.Hubs
{
    public class MessageHub : Hub
    {
        public async Task SendMessage()
        {
            while (true)
            {
                string message;
                //message = GetQueue();
                message = GetRandomWord();
                await Clients.All.SendAsync("ReceiveMessage", message);
                Thread.Sleep(1000);

            }
        }

        public string GetQueue()
        {
            string result = string.Empty;
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "MsgQueue",
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

            var consumer = new EventingBasicConsumer(channel);

            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                //Quotes receivedMessage = JsonConvert.DeserializeObject<Quotes>(Encoding.UTF8.GetString(body));
                var message = Encoding.UTF8.GetString(body);
                if (String.IsNullOrEmpty(message))
                {
                    result = message;
                    //Console.WriteLine($" [x] Received {receivedMessage.Message}");

                }

            };
            channel.BasicConsume(queue: "MsgQueue",
                                 autoAck: true,
                                 consumer: consumer);

            return result;
        }
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
                string jsonString = responseMessage.Content.ReadAsStringAsync().Result;     
                Quote contentRes = JsonConvert.DeserializeObject<Quote>(jsonString);
                res = contentRes.content;
               
            }
           
            return res;
        }
    }
}
