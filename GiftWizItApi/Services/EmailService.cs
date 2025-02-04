﻿using GiftWizItApi.Constants;
using GiftWizItApi.Interfaces;
using GiftWizItApi.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using MimeKit;
using MimeKit.Cryptography;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GiftWizItApi.Services
{
    public class EmailService: IEmailService
    {
        private readonly IEmailConfiguration emailConfiguration;
        private readonly IHostingEnvironment env;

        public EmailService(IEmailConfiguration emailConfiguration, IHostingEnvironment env)
        {
            this.emailConfiguration = emailConfiguration;
            this.env = env;
        }

        public List<EmailMessage> ReceiveEmail(int maxCount = 10)
        {
            throw new NotImplementedException();
        }

        public async Task Send(EmailMessage emailMessage)
        {
            var message = new MimeMessage();
            message.To.AddRange(emailMessage.ToAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));
            message.From.AddRange(emailMessage.FromAddresses.Select(x => new MailboxAddress(x.Name, x.Address)));

            message.Subject = emailMessage.Subject;
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = emailMessage.Content
            };

            using(var emailClient = new SmtpClient())
            {
                emailClient.Connect(emailConfiguration.SmtpServer, emailConfiguration.SmtpPort, true);

                emailClient.AuthenticationMechanisms.Remove("XOAUTH2");

                emailClient.Authenticate(emailConfiguration.SmtpUsername, emailConfiguration.SmtpPassword);

                DkimSign(message);

                await emailClient.SendAsync(message);
                emailClient.Disconnect(true);
            }
        }

        public string getContentBody<BodyBuilder>(string template, Func<MimeKit.BodyBuilder, string> getTemplateParams)
        {
            var pathToTemplate = env.ContentRootPath
                + Path.DirectorySeparatorChar.ToString()
                + EmailTemplateConstants.TemplateParentDirectory
                + Path.DirectorySeparatorChar.ToString()
                + template;

            var builder = new MimeKit.BodyBuilder();

            using (StreamReader SourceReader = System.IO.File.OpenText(pathToTemplate))
            {
                builder.HtmlBody = SourceReader.ReadToEnd();
            }

            // TODO: Supply template args
            // TODO: Execute a delegate to get appropriate template values.
            string messageBody = getTemplateParams(builder);

            return messageBody;
        }

        private void DkimSign(MimeMessage message)
        {
            var headers = new HeaderId[] { HeaderId.From, HeaderId.Subject, HeaderId.Date };
            var headerAlgorithm = DkimCanonicalizationAlgorithm.Simple;
            var bodyAlgorithm = DkimCanonicalizationAlgorithm.Simple;
            var keyPath = "";

            if (env.IsDevelopment())
            {
                keyPath = "C:/Users/Brian/Desktop/key1.giftwizit.com.pem.txt";
            }else
            {
                keyPath = "key1.giftwizit.com.pem.txt";
            }

            var signer = new DkimSigner(keyPath, "giftwizit.com", "key1", DkimSignatureAlgorithm.RsaSha256);

            message.Prepare(EncodingConstraint.SevenBit);

            message.Sign(signer, headers, headerAlgorithm, bodyAlgorithm);
        }
    }
}
