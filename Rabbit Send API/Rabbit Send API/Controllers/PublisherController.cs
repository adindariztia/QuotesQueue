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

namespace Rabbit_Send_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {

        [HttpGet("SendQueue")]
        public void SendQueue()
        {
        
            Helper helper = new Helper();
            var factory = new ConnectionFactory { HostName= "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: "MsgQueue",
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            Random num = new Random();
            
            int maxQueue = 20;
            int interval = 1000;

            while (true)
            {
                string randomPhrase = helper.GetRandomWord();

                for (int i = 0; i <= maxQueue; i++)
                {
                    
                    Item message = new Item
                    {
                        Message = randomPhrase,
                        Timestamp = DateTime.Now,
                        Priority = num.Next(1, 10)

                    };
                    byte[] body = Encoding.Default.GetBytes(JsonConvert.SerializeObject(message));

                    //var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: string.Empty,
                                         routingKey: "MsgQueue",
                                         basicProperties: null,
                                         body: body);
                }
                Thread.Sleep(interval);
            }
            
            //return true;
        }


    }

    

}
