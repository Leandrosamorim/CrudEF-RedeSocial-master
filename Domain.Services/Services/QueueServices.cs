using Azure.Storage.Queues;
using Domain.Models.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services.Services
{
    public class QueueServices : IQueueMessage
    {
       
            private readonly QueueServiceClient _queueServiceClient;
            private const string _queueName = "filamail";

            public QueueServices(string storageAccount)
            {
                _queueServiceClient = new QueueServiceClient(storageAccount);
            }

            public async Task SendAsync(string messageText)
            {
                var queueClient = _queueServiceClient.GetQueueClient(_queueName);

                await queueClient.CreateIfNotExistsAsync();

                await queueClient.SendMessageAsync(messageText);
            }
        }
    }
