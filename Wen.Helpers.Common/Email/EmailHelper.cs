using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace Wen.Helpers.Common.Email
{
    /// <summary>
    /// 邮件助手
    /// </summary>
    public static class EmailHelper
    {
        private static readonly string SenderEmailHost;
        private static readonly int SenderEmailPort;

        static EmailHelper()
        {
            try
            {
                SenderEmailHost = ConfigurationManager.AppSettings["Email.Host"];
                SenderEmailPort = Convert.ToInt32(ConfigurationManager.AppSettings["Email.Port"]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="senderAddress">发送人邮箱地址</param>
        /// <param name="senderPassword">发送人邮箱密码</param>
        /// <param name="receiverAddresses">接收人邮箱地址列表</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容正文</param>
        public static void SendEmail(string senderAddress, string senderPassword, string receiverAddresses, string subject,
            string body)
        {
            //设置发件人,发件人需要与设置的邮件发送服务器的邮箱一致
            var fromAddr = new MailAddress(senderAddress);
            var message = new MailMessage { From = fromAddr };

            //设置收件人,可添加多个,添加方法与下面的一样
            message.To.Add(receiverAddresses);
            //设置抄送人
            //message.CC.Add(senderAddress);
            //设置邮件标题
            message.Subject = subject;
            //设置邮件内容
            message.Body = body;

            //设置邮件发送服务器,服务器根据你使用的邮箱而不同,可以到相应的 邮箱管理后台查看,下面是QQ的
            //new SmtpClient("smtp.qq.com", 25)
            var client = new SmtpClient(SenderEmailHost, SenderEmailPort)
            {
                Credentials = new NetworkCredential(senderAddress, senderPassword),
                EnableSsl = true
            };

            //设置发送人的邮箱账号和密码
            //启用ssl,也就是安全发送
            //发送邮件
            client.Send(message);
        }
    }
}
