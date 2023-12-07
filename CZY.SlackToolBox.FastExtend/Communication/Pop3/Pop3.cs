using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZY.SlackToolBox.FastExtend.Communication
{
    /// <summary>
    /// 基于Interop.jmail.dll实现，后续可更新
    /// </summary>
    public class Pop3
    {
        
        /// <summary>
        /// 收取新邮件、不删除老邮件、收取邮件后写入数据库
        /// </summary>
        //public static void GetNewMailIntoDataBase(string UserName, string PassWord, string PopServer, int Port, DateTime MaxDate)
        //{
        //    POP3 NewMail = new POP3();
        //    NewMail.Connect(UserName, PassWord, PopServer, Port);
        //    for (int i = 1; i <= NewMail.Count; i++)
        //    {
        //        //判断是否跟当前最大的时间作比较，大于当前时间就处理
        //        DateTime CurrentEmailDate = DateTime.Now;
        //        try
        //        {
        //            CurrentEmailDate = DateTime.Parse(NewMail.Messages[i].Date.ToString());
        //        }
        //        catch
        //        { }
        //        try
        //        {
        //            if (CurrentEmailDate.CompareTo(MaxDate) > 0)
        //            {
        //                string EmailFuJian = "";
        //                for (int j = 0; j < NewMail.Messages[i].Attachments.Count; j++)
        //                {
        //                    try
        //                    {
        //                        string FileName = DateTime.Now.Ticks.ToString() + NewMail.Messages[i].Attachments[j].Name;
        //                        NewMail.Messages[i].Attachments[j].SaveToFile(System.Web.HttpContext.Current.Request.MapPath("../UploadFile") + "\\MailAttachments\\" + FileName);
        //                        if (EmailFuJian.Trim().Length > 0)
        //                        {
        //                            EmailFuJian = EmailFuJian + "|MailAttachments/" + FileName;
        //                        }
        //                        else
        //                        {
        //                            EmailFuJian = "MailAttachments/" + FileName;
        //                        }
        //                    }
        //                    catch (Exception e)
        //                    {
        //                        System.Web.HttpContext.Current.Response.Write("<script>alert('" + e.Message.ToString() + "');</script>");
        //                    }
        //                }

        //                ZWL.BLL.ERPNetEmail MyModel = new ZWL.BLL.ERPNetEmail();
        //                MyModel.EmailContent = NewMail.Messages[i].Body;
        //                MyModel.EmailState = "未读";
        //                MyModel.EmailTitle = NewMail.Messages[i].Subject;
        //                MyModel.FromUser = NewMail.Messages[i].FromName + "（" + NewMail.Messages[i].From + "）";
        //                MyModel.FuJian = EmailFuJian;
        //                try
        //                {
        //                    MyModel.TimeStr = DateTime.Parse(NewMail.Messages[i].Date.ToString());
        //                }
        //                catch
        //                {
        //                    MyModel.TimeStr = DateTime.Now;
        //                }
        //                MyModel.ToUser = ZWL.Common.PublicMethod.GetSessionValue("UserName");
        //                MyModel.Add();
        //            }
        //        }
        //        catch (Exception ee)
        //        {
        //            System.Web.HttpContext.Current.Response.Write("<script>alert('" + ee.Message.ToString() + "');</script>");
        //        }
        //    }
        //    NewMail.Disconnect();
        //}
    
    
    }
}
