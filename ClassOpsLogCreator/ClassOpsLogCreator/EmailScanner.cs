using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S22.Imap;
using System.Net.Mail;

namespace ClassOpsLogCreator
{
    /// <summary>
    /// This class wills can in an email and process it into an Excel file
    /// </summary>
    public class EmailScanner
    {
        //Host connection string
        private static string hostname = "mypost.yorku.ca";
        private static string lotusHostName = "postoffice.notes.yorku.ca";
        private string username = Properties.Settings.Default.UserName;
        private string password = Properties.Settings.Default.Password;
        private string lotusPass = Properties.Settings.Default.lotusPassword;
        private bool isLotus = Properties.Settings.Default.isLotusAccount;
        private bool isConnectedFlag = false;

        private string msgFrom;
        private string msgBody;

        /// <summary>
        /// The constructor to connect and interface with e-mail.
        /// </summary>
        public EmailScanner(DateTime today)
        {
            if (isLotus)
            {
                username = username + "@yorku.ca";
                using (ImapClient client = new ImapClient(lotusHostName, 993, username, lotusPass, AuthMethod.Login, true))
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
                }
            }
            else
            {
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
                }
            }
        }

        /// <summary>
        /// This method tests if we can make a connection to the imap server
        /// </summary>
        /// <param name="test"></param>
        public EmailScanner(bool test)
        {
            try
            {
                if (isLotus)
                {
                    using (ImapClient client = new ImapClient(lotusHostName, 993, username, lotusPass, AuthMethod.Login, true))
                    {
                        this.isConnectedFlag = true;
                    }
                }
                else
                {
                    using (ImapClient client = new ImapClient(hostname, 993, username, password, AuthMethod.Login, true))
                    {
                        this.isConnectedFlag = true;
                    }
                }
            }
            catch (Exception)
            {
                this.isConnectedFlag = false;
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
            string resault = msgFrom.Split('<', '>')[1];
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
