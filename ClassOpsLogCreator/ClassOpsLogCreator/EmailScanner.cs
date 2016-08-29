using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S22.Imap;
using System.Net.Mail;

namespace ClassOpsLogCreator
{
    public class EmailScanner
    {
        private static string hostname = "mypost.yorku.ca";
        //private static string username = Properties.Settings.Default.UserName;
        //private static string password = Properties.Settings.Default.Password;
        private static string username = "pereraj";
        private static string password = "pooman12";
        private bool isConnectedFlag = false;

        private string msgFrom;
        private string msgBody;

        /// <summary>
        /// The constructor to connect and interface with e-mail.
        /// </summary>
        public EmailScanner()
        {
            using (ImapClient client = new ImapClient(hostname, 993, username, password, AuthMethod.Login, true))
            {
                this.isConnectedFlag = true;

                IEnumerable<uint> uids = client.Search(SearchCondition.From("pereraj@yorku.ca").And(SearchCondition.Subject("Room Report")));
                IEnumerable <MailMessage> messages = client.GetMessages(uids, FetchOptions.Normal);

                foreach(MailMessage msg in messages)
                {
                    msgFrom = msg.From.ToString();
                    msgBody = msg.Body.ToString();
                }

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
            return "";
        }
    }
}
