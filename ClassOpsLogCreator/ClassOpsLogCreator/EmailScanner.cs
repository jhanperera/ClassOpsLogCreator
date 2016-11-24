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
using Google.Apis.Util.Store;
using Google.Apis.Services;
using Google.Apis.Gmail.v1.Data;

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

            try
            {
                {
                    // If modifying these scopes, delete your previously saved credentials
                    // at ~/.credentials/gmail-dotnet-quickstart.json
                     string[] Scopes = { GmailService.Scope.GmailReadonly };
                     string ApplicationName = "CLog";

                        UserCredential credential;

                        using (var stream =
                            new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
                        {
                            string credPath = System.Environment.GetFolderPath(
                                System.Environment.SpecialFolder.Personal);
                            credPath = Path.Combine(credPath, ".credentials/gmail-dotnet-quickstart.json");

                            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                                GoogleClientSecrets.Load(stream).Secrets,
                                Scopes,
                                "user",
                                CancellationToken.None,
                                new FileDataStore(credPath, true)).Result;
                            Console.WriteLine("Credential file saved to: " + credPath);
                        }

                        // Create Gmail API service.
                        var service = new GmailService(new BaseClientService.Initializer()
                        {
                            HttpClientInitializer = credential,
                            ApplicationName = ApplicationName,
                        });

                        // Define parameters of request.
                        UsersResource.LabelsResource.ListRequest request = service.Users.Labels.List("me");

                        // List labels.
                        IList<Label> labels = request.Execute().Labels;
                        Console.WriteLine("Labels:");
                        if (labels != null && labels.Count > 0)
                        {
                            foreach (var labelItem in labels)
                            {
                                Console.WriteLine("{0}", labelItem.Name);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No labels found.");
                        }
                        Console.Read();

                    }
               

                /*using (ImapClient client = new ImapClient(hostname, 993, username, password, AuthMethod.Login, true))
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
