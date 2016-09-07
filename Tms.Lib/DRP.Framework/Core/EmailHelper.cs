using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using jmail;

namespace DRP.Framework.Core
{
    public class EmailEntity
    {
        public string MailServerSmtp { get; set; }

        public string MailServerAcct { get; set; }

        public string MailServerPwd { get; set; }

        public string MailContent { get; set; }

        public string MailTo { get; set; }

        public string MailFrom { get; set; }

        public string MailFromName { get; set; }

        public string Subject { get; set; }

        public List<string> Attachment { get; set; }
    }

    public class EmailHelper
    {
        public bool Send(EmailEntity entity)
        {
            try
            {
                Message jmailobj = new Message();
                jmailobj.Logging = false;
                jmailobj.Silent = true;
                jmailobj.Charset = "gb2312";
                jmailobj.Priority = 1;
                jmailobj.Subject = entity.Subject;
                jmailobj.HTMLBody = entity.MailContent;
                jmailobj.ContentType = "text/html";

                //foreach (string f in entity.Attachment)
                //{
                //    if (f.Contains("."))
                //        jmailobj.AddAttachment(f, true, null);
                //}

                jmailobj.MailServerUserName = entity.MailServerAcct; //发信邮件服务器的帐号 
                jmailobj.MailServerPassWord = entity.MailServerPwd; //密码           
                jmailobj.From = entity.MailFrom;
                jmailobj.FromName = entity.MailFromName;
                jmailobj.AddRecipient(entity.MailTo);
                bool res = jmailobj.Send(entity.MailServerSmtp, false);
                jmailobj.Close();
                return res;
            }
            catch {
                return false;
            }
        }

    }
}
