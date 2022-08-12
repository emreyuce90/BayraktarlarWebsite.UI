using BayraktarlarWebsite.BLL.Interfaces;
using BayraktarlarWebsite.Entities.Dtos;
using BayraktarlarWebsite.Entities.Entities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BayraktarlarWebsite.BLL.Concrete
{
    public class MailManager : IMailService
    {
        private readonly SmtpSettings _smtpSettings;
        public MailManager(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }
        public void SendMail(EmailSendDto emailSendDto)
        {
            //MailMessage mailMessage = new MailMessage
            //{
            //    From= new MailAddress(_smtpSettings.SenderName), 
            //    To = { new MailAddress(emailSendDto.UserEmailAddress) },      
            //    Subject =emailSendDto.Subject,
            //    IsBodyHtml=true,
            //    Body=emailSendDto.Description,

            //};
            MailMessage mailMessage = new(_smtpSettings.SenderEmail,emailSendDto.UserEmailAddress,emailSendDto.Subject,emailSendDto.Description);
            mailMessage.IsBodyHtml = true;
            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpSettings.Server,
                Port = _smtpSettings.Port,
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            smtpClient.Send(mailMessage);
        }
    }
}
