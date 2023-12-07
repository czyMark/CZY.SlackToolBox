using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace RoteBridge.Core
{
    public static class SendEmail
    {
        /// <summary>
        /// 发送邮件，注意需要 用户在邮件服务器上开启Smtp
        /// </summary>
        /// <param name="host">邮件服务器</param>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="touserName">收件人邮箱地址</param>
        /// <param name="tocopyname">抄送人邮箱地址</param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public static bool SendMail(this string host, string userName, string password, 
            List<string> touserName, List<string> tocopyname, string subject, string body)
        { 
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式    
            client.Host = host;//邮件服务器
            client.UseDefaultCredentials = true;
            client.Credentials = new System.Net.NetworkCredential(userName, password);//用户名、密码

            //////////////////////////////////////
            string strfrom = userName; 

            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            msg.From = new MailAddress(strfrom, "xyf");
            for (int i = 0; i < touserName.Count; i++)
            {
                msg.To.Add(touserName[i]);
            }
            if (touserName != null)
            {
                for (int i = 0; i < tocopyname.Count; i++)
                {
                    msg.CC.Add(tocopyname[i]);
                }
            }  
            msg.Subject = subject;//邮件标题   
            msg.Body = body;//邮件内容   
            msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码   
            msg.IsBodyHtml = true;//是否是HTML邮件   
            msg.Priority = MailPriority.High;//邮件优先级   
            try
            {
                client.Send(msg);
                return true;
            }
            catch (System.Net.Mail.SmtpException ex)
            {
               return false;
            }
        }
    
        
    
    }


}
