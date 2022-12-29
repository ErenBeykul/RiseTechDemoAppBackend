using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RiseTechDemoApp.Domain.Enums;
using System.Text;

namespace ReportService.Worker
{
    /// <summary>
    /// Rabbitmq ya Mesaj Gönderme ve Alma İşlemlerini İfade Eder
    /// </summary>
    public class QueueActions : IQueueActions
    {
        readonly IWorkers _workers;

        public QueueActions(IWorkers workers)
        {
            _workers = workers;
        }

        /// <summary>
        /// Belli Bir Mesajı Belli Bir Rabbitmq Kuyruğuna (Queue) Gönderir
        /// </summary>
        /// <param name="queue"></param>
        /// <param name="message"></param>
        public static void Send(string queue, string message)
        {
            ConnectionFactory factory = new() { HostName = "localhost" };
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();
            channel.QueueDeclare(queue: queue,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            byte[] body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: queue,
                                 basicProperties: null,
                                 body: body);
        }

        public void GenerateReportIfAsked()
        {
            ConnectionFactory factory = new() { HostName = "localhost" };
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();
            channel.QueueDeclare(queue: QueueName.Reports.ToString(),
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            EventingBasicConsumer consumer = new(channel);

            consumer.Received += async (model, ea) =>
            {
                byte[] body = ea.Body.ToArray();
                string message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] Received {0}", message);

                // Rapor Hazırlanıyor
                Guid reportId = Guid.Parse(message);
                await _workers.GenerateReport(reportId);
            };

            channel.BasicConsume(queue: QueueName.Reports.ToString(),
                                 autoAck: true,
                                 consumer: consumer);

            Console.ReadLine();
        }
    }
}