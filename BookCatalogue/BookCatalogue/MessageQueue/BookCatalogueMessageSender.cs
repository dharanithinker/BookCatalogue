using BookCatalogue.Models;
using Microsoft.Extensions.Options;
using Nancy.Json;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookCatalogue.MessageQueue
{

    public class BookCatalogueMessageSender : IBookCatalogueMessageSender
    {
        private readonly string _hostname;
        private readonly string _password;
        private readonly string _queueName;
        private readonly string _username;
        private readonly int _port;

        public BookCatalogueMessageSender(IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _queueName = rabbitMqOptions.Value.QueueName;
            _hostname = rabbitMqOptions.Value.Hostname;
            _username = rabbitMqOptions.Value.UserName;
            _password = rabbitMqOptions.Value.Password;
            _port = rabbitMqOptions.Value.Port;
        }

        public void SendNewBookNotification(Book newBook)
        {
            var factory = new ConnectionFactory() { HostName = _hostname };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                EventMessage message = new EventMessage()
                {
                    Type = EventAction.BOOK_ADDED,
                    Message = "Book Added",
                    Data = newBook
                };
                var json = new JavaScriptSerializer().Serialize(message);
                var body = Encoding.UTF8.GetBytes(json);
                channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
            }
        }

        public void UpdateBookNotification(Book modifiedBook)
        {
            var factory = new ConnectionFactory() { HostName = _hostname };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                EventMessage message = new EventMessage()
                {
                    Type = EventAction.BOOK_UPDATED,
                    Message = "Book Updated",
                    Data = modifiedBook
                };
                var json = new JavaScriptSerializer().Serialize(message);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
            }
        }

        public void DeleteBookNotification()
        {
            var factory = new ConnectionFactory() { HostName = _hostname };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                EventMessage message = new EventMessage()
                {
                    Type = EventAction.BOOK_DELETED,
                    Message = "Book Deleted"
                };
                var json = new JavaScriptSerializer().Serialize(message);
                var body = Encoding.UTF8.GetBytes(json);

                channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
            }
        }
    }
}
