﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using S22.Imap;
using System.Net.Mail;
using Google.Apis.Gmail.v1;
using Google.Apis.Auth.OAuth2;
using System.IO;
using System.Threading;
using Google.Apis.Util.Store;
using Google.Apis.Services;
using Google.Apis.Gmail.v1.Data;
using ActiveUp.Net.Mail;
using System.Security.Cryptography.X509Certificates;

namespace ClassOpsLogCreator
{
    /// <summary>
    /// This class wills can in an email and process it into an Excel file
    /// </summary>
    public class EmailScanner
    {
        //Host connection string
        private static string hostname = "imap.gmail.com";
        private string username = "uitclientservices";
        private string password = "ca1mn3ss";
        private bool isConnectedFlag = false;

        private string msgFrom;
        private string msgBody;

        /// <summary>
        /// The constructor to connect and interface with e-mail.
        /// </summary>
        public EmailScanner(DateTime today)
        {
            string dayOfTheWeek = today.ToString("dddd");
            try
            {    

                MailRepository mailRepo = new MailRepository(hostname, 993, true, username, password);

                var emailList = mailRepo.GetAllMails("inbox");

                foreach(ActiveUp.Net.Mail.Message ms in emailList)
                {
                    if(ms.Subject == "Room Report for " + dayOfTheWeek && 
                        (ms.Date.ToString("dd-MM-yyyy") == today.ToString("dd-MM-yyyy")))
                    {
                        this.msgFrom = ms.From.Email;
                        this.msgBody = ms.BodyText.Text;
                    }
                }

                /*
                using (ImapClient client = new ImapClient(hostname, 993, username, password, AuthMethod.Login, true))
                {
                    this.isConnectedFlag = true;

                    string dayOfTheWeek = DateTime.Now.ToString("dddd");

                    IEnumerable<uint> uids = client.Search(SearchCondition.Subject("Room Report for " + dayOfTheWeek).And(SearchCondition.SentOn(today)));
                    IEnumerable<MailMessage> messages = client.GetMessages(uids, FetchOptions.Normal);

                    foreach (MailMessage msg in messages)
                    {
                        msgFrom = msg.From.ToString();
                        msgBody = msg.Body.ToString();
                    }
                }*/
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Return true if we are able to 
        /// </summary>
        /// <returns></returns>
        public bool isConnected()
        {
            return this.isConnectedFlag;
        }

        /// <summary>
        /// Return the Message sender email address
        /// </summary>
        /// <returns></returns>
        public string messageFrom()
        {
            string resault = msgFrom;
            return resault;
        }

        /// <summary>
        /// This returns the message body of the message
        /// </summary>
        /// <returns></returns>
        public string messageBody()
        {
            try
            {
                msgBody = msgBody.Replace("?", " ");
            }
            catch(Exception)
            {
                msgBody = null;
            }          
            return msgBody;
        }
    }
}
