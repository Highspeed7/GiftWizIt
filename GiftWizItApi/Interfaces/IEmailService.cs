using GiftWizItApi.Models;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static GiftWizItApi.Services.EmailService;

namespace GiftWizItApi.Interfaces
{
    public interface IEmailService
    {
        Task Send(EmailMessage emailMessage);
        List<EmailMessage> ReceiveEmail(int maxCount = 10);
        string getContentBody<BodyBuilder>(string template, Func<MimeKit.BodyBuilder, string> builder);
    }
}
