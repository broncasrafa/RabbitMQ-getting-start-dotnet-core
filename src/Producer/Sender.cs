using System.Text;
using RabbitMQ.Client;

namespace Producer
{
    public class Sender
    {
        private static string _QUEUE_NAME = "BasicTestQueue";
        private static string _message = string.Empty;


        static void Main(string[] args)
        {
            Console.WriteLine(@"=====================================================================================================");
            Console.WriteLine(@" ********** I N I C I A N D O     O     P R O C E S S O ******************************************** ");
            Console.WriteLine(@"=====================================================================================================");
            Console.WriteLine("");

            // seta a mensagem
            SetMessageAndPublish();

            ConsoleAppHelpers.ExitApplication();
        }


        private static void PublishMessage()
        {
            // abrir uma conexão com o servidor RabbitMQ (abrir um Channel para publicar e consumir mensagens)
            // e então publicar uma mensagem depois de declarar uma fila.

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: _QUEUE_NAME,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                byte[] body = Encoding.UTF8.GetBytes(_message);

                channel.BasicPublish(
                    exchange: "",
                    routingKey: _QUEUE_NAME,
                    basicProperties: null,
                    body: body);

                ConsoleAppHelpers.DisplayConsoleText($"Published message: {_message}....", ConsoleColor.Cyan);
            }
        }

        private static void SetMessageAndPublish()
        {
            Console.WriteLine("Digite a mensagem para publicar na fila ou pressione 0 para sair:");

            var texto = Console.ReadLine();

            if (texto.All(Char.IsDigit))
            {
                Environment.Exit(0);
            }

            _message = texto;

            PublishMessage();
            _message = string.Empty;

            do
            {
                if (_message == null || _message.Length == 0)
                {
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine("\nDigite a mensagem para publicar na fila ou pressione 0 para sair:");

                    var n_texto = Console.ReadLine();

                    if (n_texto.All(Char.IsDigit))
                    {
                        Environment.Exit(0);
                    }

                    _message = n_texto;

                    PublishMessage();
                    _message = string.Empty;
                }

            } while (_message == null || _message.Length == 0);
        }
    }
}