#region namespaces

using System.Net;
using System.Net.Mail;

#endregion

namespace Wen.Common.Email
{
    /// <summary>
    /// 邮件发送
    /// </summary>
    public class EmailSender
    {
        /// <summary>
        /// 地址
        /// </summary>
        private readonly string _address;

        /// <summary>
        /// 密码
        /// </summary>
        private readonly string _password;

        /// <summary>
        /// 端口
        /// </summary>
        private readonly int _port;

        /// <summary>
        /// 主机
        /// </summary>
        private readonly string _smtpHost;

        /// <summary>
        /// 邮件发送
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="password">密码</param>
        /// <param name="smtpHost">主机</param>
        /// <param name="port">端口</param>
        public EmailSender(string address, string password, string smtpHost, int port)
        {
            _address = address;
            _password = password;
            _port = port;
            _smtpHost = smtpHost;
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="receiverAddresses">接收人邮箱地址列表</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容正文</param>
        /// <param name="enableSsl"></param>
        public void Send(string receiverAddresses, string subject, string body, bool enableSsl = true)
        {
            //设置发件人,发件人需要与设置的邮件发送服务器的邮箱一致
            var fromAddr = new MailAddress(_address);
            var message = new MailMessage {From = fromAddr};

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
            var client = new SmtpClient(_smtpHost, _port)
            {
                //设置发送人的邮箱账号和密码
                Credentials = new NetworkCredential(_address, _password),
                //启用ssl,也就是安全发送
                EnableSsl = enableSsl
            };

            //发送邮件
            client.Send(message);
        }
    }
}