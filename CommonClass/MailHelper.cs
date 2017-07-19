using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net.Mail;

namespace ZCZJ_DPF
{
    public class MailHelper
    {
        /// <summary>
        /// Sends an mail message
        /// </summary>
        /// <param name="from">Sender address</param>
        /// <param name="to">Recepient address</param>
        /// <param name="bcc">Bcc recepient</param>
        /// <param name="cc">Cc recepient</param>
        /// <param name="subject">Subject of mail message</param>
        /// <param name="body">Body of mail message</param>
        public static void SendMailMessage(string from, string to, string bcc, string cc, string subject, string body)
        {
            // 新建一个实例
            MailMessage mMailMessage = new MailMessage();

            // 设置发送者的邮件地址
            mMailMessage.From = new MailAddress(from);

            //// 设置接受者的邮件地址
            mMailMessage.To.Add(new MailAddress(to));

            /***************传给多个人*******************/

            //for (int i = 0; i < strToList.Length; i++)
            //{
            //    mMailMessage.To.Add(new MailAddress(strToList[i]));
            //    mail.To.Add(strToList[i]);
            //}

            // Check if the bcc value is null or an empty string
            // 密件抄送 (BCC) 收件人的地址集合。
            if ((bcc != null) && (bcc != string.Empty))
            {
                // Set the Bcc address of the mail message
                mMailMessage.Bcc.Add(new MailAddress(bcc));
            }

            // Check if the cc value is null or an empty value
            //抄送 (CC) 收件人的地址集合。
            if ((cc != null) && (cc != string.Empty))
            {
                // Set the CC address of the mail message
                mMailMessage.CC.Add(new MailAddress(cc));
            }

            // 设置邮件主题
            mMailMessage.Subject = subject;

            // 设置邮件的内容
            mMailMessage.Body = body;

            // 设置邮件的内容是否为HTML
            mMailMessage.IsBodyHtml = true;

            // 设置邮件的优先权
            //mMailMessage.Priority = MailPriority.High;


            // 新建邮件客户端实例
            //SmtpClient mSmtpClient = new SmtpClient();

            ////获取或设置用于 SMTP 事务的主机的名称或 IP 地址。
            //mSmtpClient.Host = "10.202.0.56";

            //用163的邮箱服务器
            SmtpClient mSmtpClient = new SmtpClient("smtp.163.com");

            ////获取或设置用于 SMTP 事务的主机的名称或 IP 地址。
            mSmtpClient.Credentials = new System.Net.NetworkCredential("zthb2011", "zthb123456");

            // 发送邮件
            mSmtpClient.Send(mMailMessage);
        }
    }
}
