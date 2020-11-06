using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.WebJobs;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Logging;

namespace Function
{
    public static class Function2
    {
        [FunctionName("LeandroFunction")]
        [return: SendGrid(ApiKey = "ApiKeySendGrid", To = "{EmailPara}", From = "leandrofla1@gmail.com")]
        public static SendGridMessage Run([QueueTrigger("filamail", Connection = "StorageAccount")] Email email, ILogger log)
        {
            log.LogInformation($"Email assunto: {email.Assunto}");

            SendGridMessage message = new SendGridMessage()
            {
                Subject = email.Assunto
            };

            message.AddContent("text/plain", email.Corpo);
            return message;
        }
    }

    public class Email
    {
        public string Assunto { get; set; }
        public string Corpo { get; set; }
        public string EmailPara { get; set; }
    }
}
