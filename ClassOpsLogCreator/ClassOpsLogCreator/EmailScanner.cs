using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S22.Imap;
using System.Net.Mail;
using ActiveUp.Net.Mail;

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

            string dayOfTheWeek = DateTime.Now.ToString("dddd");

            try
            {
                var mailRepository = new MailRepository(
                            "imap.gmail.com",
                            993,
                            true,
                            username + "@gmail.com",
                            password
                        );

                this.isConnectedFlag = true;

                //get all the messages from the inboc
                var emailList = mailRepository.GetAllMails("inbox");

                foreach (Message email in emailList)
                {
                    if (email.Subject == "Fwd: Room Report for Wednesday" && email.ReceivedDate == DateTime.Today)
                    {
                        msgBody = email.BodyText.ToString();
                        msgFrom = email.From.ToString();
                    }

                }
            }
            catch (Exception)
            {

                throw;
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

        /// <summary>
        /// This method tests if we can make a connection to the imap server
        /// </summary>
        /// <param name="test"></param>
        public EmailScanner(bool test)
        {
            try
            {
                using (ImapClient client = new ImapClient(hostname, 993, username, password, AuthMethod.Login, true))
                {
                    this.isConnectedFlag = true;
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
