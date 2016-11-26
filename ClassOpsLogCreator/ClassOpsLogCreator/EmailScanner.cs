using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S22.Imap;
using System.Net.Mail;
using Google.Apis.Gmail.v1;
using Google.Apis.Auth.OAuth2;
using System.IO;
using System.Threading;
/*using Google.Apis.Util.Store;
using Google.Apis.Services;
using Google.Apis.Gmail.v1.Data;
using ActiveUp.Net.Mail;*/
using OpenPop.Pop3;
using OpenPop.Mime;

namespace ClassOpsLogCreator
{
    /// <summary>
    /// This class wills can in an email and process it into an Excel file
    /// </summary>
    public class EmailScanner
    {
        //Host connection string
        private static string hostname = "pop.gmail.com";
        private string username = Properties.Settings.Default.gmailUserName;
        private string password;
        private bool isConnectedFlag = false;

        private string msgFrom;
        private string msgBody;

        /// <summary>
        /// The constructor to connect and interface with e-mail.
        /// </summary>
        public EmailScanner(DateTime today)
        {
            string dayOfTheWeek = today.ToString("dddd");

            //We get the password from the JSON file
            JsonParser JP = new JsonParser();
            password = JP.getPassword();

            try
            {
                //use the POP3 method to retrieve our Room report email
                /*
                using (var clientPOP = new Pop3Client())
                {
                    clientPOP.Connect("pop.gmail.com", 995, true);
                    clientPOP.Authenticate(username + "@gmail.com", password, AuthenticationMethod.Auto);
                    var count = clientPOP.GetMessageCount();

                    // We want to download all messages
                    List<Message> allMessages = new List<Message>(count);

                    // Messages are numbered in the interval: [1, messageCount]
                    // Ergo: message numbers are 1-based.
                    // Most servers give the latest message the highest number
                    for (int i = count; i > (count - 5); i--)
                    {
                        allMessages.Add(clientPOP.GetMessage(i));
                    }

                    foreach (Message msg in allMessages)
                    {
                        if (msg.Headers.Subject == "Room Report for " + dayOfTheWeek &&
                            msg.Headers.DateSent.ToString("dd-MM-yy") == today.ToString("dd-MM-yy"))
                        {
                            OpenPop.Mime.MessagePart plainTextPart = msg.FindFirstPlainTextVersion();
                            msgBody = plainTextPart.GetBodyAsText();
                            msgFrom = msg.Headers.From.ToString();
                            break;
                        }
                    }
                }*/
               
                //**********************************TEST CODE*******************************************************/
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
                using (ImapClient client = new ImapClient("mypost.yorku.ca", 993, "pereraj", "pooman12", AuthMethod.Login, true))
                {
                    this.isConnectedFlag = true;


                    IEnumerable<uint> uids = client.Search(SearchCondition.Subject("Room Report for " + dayOfTheWeek).And(SearchCondition.SentOn(today)));
                    IEnumerable<MailMessage> messages = client.GetMessages(uids, FetchOptions.Normal);

                    foreach (MailMessage msg in messages)
                    {
                        msgFrom = msg.From.ToString();
                        msgBody = msg.Body.ToString();
                    }
                }*/
                //**********************************TEST CODE*******************************************************/
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// This contructor will be called to test wheather we can make a connection to the google servers
        /// 
        /// If we have a succesful connection we update the password on central web
        /// </summary>
        /// <param name="test"></param>
        public EmailScanner(bool test)
        {
            //Try and make a connection to email server
            try
            {
                //Lets try it with this password
                password = Properties.Settings.Default.gmailPassword;

                MailRepository mailRepo = new MailRepository(hostname, 993, true, username, password);

                //If we get here we have a connection
                this.isConnectedFlag = true;
                JsonParser JP = new JsonParser();

                //Update the password if we are good. 
                JP.updateJson(password);

            }
            catch (Exception)
            {
                //We hit a problem try again
                this.isConnectedFlag = false;
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
        /// <returns>null if the message was not valid</returns>
        public string messageBody()
        {
            //Try and replace any artifacts that might occur
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
