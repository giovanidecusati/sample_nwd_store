using BuildingBlock.EventBus.Abstractions;
using BuildingBlock.EventBus.Events;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace BuildingBlock.EventBus.Rabbitmq
{
    public class RabbitMqManager : IEventBus, IDisposable
    {
        private readonly RabbitMqConnection _connection;
        private readonly ILogger<RabbitMqManager> _logger;
        private readonly Dictionary<string, List<IIntegrationEventHandler>> _handlers = new Dictionary<string, List<IIntegrationEventHandler>>();
        private readonly List<Type> _eventTypes = new List<Type>();        
        private readonly string _queueExchange;

        private string _queueName;
        private IModel _channel;

        public RabbitMqManager(
            RabbitMqConnection connection,
            ILogger<RabbitMqManager> logger)
        {
            _queueExchange = connection.Settings.Exchange;
            _connection = connection;
            _logger = logger;
            _channel = CreateConsumerChannel();            
        }


        public void Publish(IntegrationEvent @event)
        {

            if (!_connection.IsConnected)
            {
                _connection.TryConnect();
            }

            var policy = RetryPolicy.Handle<BrokerUnreachableException>()
                .Or<SocketException>()
                .WaitAndRetry(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                {
                    _logger.LogWarning(ex.ToString());
                });

            using (var channel = _connection.CreateModel())
            {
                var eventName = @event.GetType().Name;

                channel.ExchangeDeclare(
                    exchange: _queueExchange,
                    type: ExchangeType.Direct);

                var message = JsonConvert.SerializeObject(@event);
                var body = Encoding.UTF8.GetBytes(message);
                var messageProperties = channel.CreateBasicProperties();
                messageProperties.CorrelationId = Guid.NewGuid().ToString();

                policy.Execute(() =>
                {
                    channel.BasicPublish(
                        exchange: _queueExchange,
                        routingKey: eventName,
                        basicProperties: messageProperties,
                        body: body);
                });
            }
        }

        public void Subscribe<T>(IIntegrationEventHandler<T> handler) where T : IntegrationEvent
        {
            var eventName = typeof(T).Name;

            if (_handlers.ContainsKey(eventName))
            {
                _handlers[eventName].Add(handler);
            }
            else
            {
                if (!_connection.IsConnected)
                {
                    _connection.TryConnect();
                }

                using (var channel = _connection.CreateModel())
                {
                    channel.QueueBind(queue: _queueName,
                                      exchange: _queueExchange,
                                      routingKey: eventName);

                    _handlers.Add(eventName, new List<IIntegrationEventHandler>());
                    _handlers[eventName].Add(handler);
                    _eventTypes.Add(typeof(T));
                }

            }
        }

        public void Unsubscribe<T>(IIntegrationEventHandler<T> handler) where T : IntegrationEvent
        {
            var eventName = typeof(T).Name;

            if (_handlers.ContainsKey(eventName) && _handlers[eventName].Contains(handler))
            {
                _handlers[eventName].Remove(handler);

                if (_handlers[eventName].Count == 0)
                {
                    _handlers.Remove(eventName);

                    var eventType = _eventTypes.SingleOrDefault(e => e.Name == eventName);

                    if (eventType != null)
                    {
                        _eventTypes.Remove(eventType);

                        if (!_connection.IsConnected)
                        {
                            _connection.TryConnect();
                        }

                        using (var channel = _connection.CreateModel())
                        {
                            channel.QueueUnbind(queue: _queueName,
                                exchange: _queueExchange,
                                routingKey: eventName);

                            if (_handlers.Keys.Count == 0)
                            {
                                _queueName = string.Empty;

                                _channel.Close();
                            }
                        }
                    }
                }
            }
        }

        public void Dispose()
        {
            if (_channel != null)
            {
                _channel.Dispose();
            }

            _handlers.Clear();
        }

        private IModel CreateConsumerChannel()
        {
            if (!_connection.IsConnected)
            {
                _connection.TryConnect();
            }

            var channel = _connection.CreateModel();

            channel.ExchangeDeclare(
                exchange: _queueExchange,
                type: ExchangeType.Direct);

            _queueName = channel.QueueDeclare().QueueName;

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                var eventName = ea.RoutingKey;
                var message = Encoding.UTF8.GetString(ea.Body);

                await ProcessEvent(eventName, message);
            };

            channel.BasicConsume(
                queue: _queueName,
                consumer: consumer);

            channel.CallbackException += (sender, ea) =>
            {
                _channel.Dispose();
                _channel = CreateConsumerChannel();
            };

            return channel;
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            if (_handlers.ContainsKey(eventName))
            {
                Type eventType = _eventTypes.Single(t => t.Name == eventName);
                var integrationEvent = JsonConvert.DeserializeObject(message, eventType);
                var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                var handlers = _handlers[eventName];

                foreach (var handler in handlers)
                {
                    await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { integrationEvent });
                }
            }
        }
    }
}
