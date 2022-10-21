using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Consumer
{
    public class Receiver
    {
        private static string _QUEUE_NAME = "BasicTestQueue";

        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: _QUEUE_NAME,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body.ToArray());
                    Console.WriteLine($"Received message {message} ....");
                };

                channel.BasicConsume(
                    queue: _QUEUE_NAME,
                    autoAck: true,
                    consumer: consumer);

                Console.WriteLine("Press [enter] to exit .....");
                Console.ReadLine();
            }
        }
    }
}