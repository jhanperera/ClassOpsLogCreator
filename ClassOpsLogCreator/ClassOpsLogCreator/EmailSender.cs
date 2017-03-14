using System;
using System.Net.Mail;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;


namespace ClassOpsLogCreator
{
    /// <summary>
    /// This calls will generate a email and send it to csmanagers for statitcs reasons.
    /// </summary>
    public class EmailSender
    {
        private DateTime now = DateTime.Now;
        private bool connectionMade = false;

        /// <summary>
        /// This is the main constructor to make the connection and send the statistics email
        /// </summary>
        public EmailSender(string attachmentPath, string emailSubject)
        {
            //Connect to the smtp server
            using (SmtpClient smtpClient = new SmtpClient())
            {
                //Using the credentials provided in the settings panel.
                NetworkCredential basicCredential =
                                    new NetworkCredential(Properties.Settings.Default.UserName, Properties.Settings.Default.Password);

                //Create a message
                MailMessage message = new MailMessage();

                //Set the sending address
                MailAddress fromAddress = new MailAddress(Properties.Settings.Default.UserName + "@yorku.ca");

                //Set the smtp host name
                smtpClient.Host = "mailrelay.yorku.ca";

                //Set the port to 587 SATALITE
                smtpClient.Port = 587;

                //Do not use the default credential
                smtpClient.UseDefaultCredentials = false;

                //Use SSL to send the email security.
                smtpClient.EnableSsl = true;

                //Set the credentials to the user name and password provided.
                smtpClient.Credentials = basicCredential;

                //Set the from sender of the message
                message.From = fromAddress;

                //Set the subject
                message.Subject = emailSubject;

                //Set IsBodyHtml to true means you can send HTML email.
                message.IsBodyHtml = true;
                message.Body = "<h3>Please see attached a pdf of the auto generated statistics.</h3>";

                //Have to add the description of each task

                message.Body += "This message was auto generated at " + now.ToString() + 
                                " by the CLog.exe application from: "+ this.getIPofMachine();

                //Set the attachment
                message.Attachments.Add(new Attachment(attachmentPath));

                //DEBUG CODE
                //message.To.Add("pereraj@yorku.ca");

                //Send the email to masyb@yorku.ca
                message.To.Add("masyb@yorku.ca");

                //Try to send the email
                try
                {
                    smtpClient.Send(message);
                    connectionMade = true;
                }
                catch (Exception)
                {
                    throw new Exception("Unable to send statistics email. Please check login credentials");
                }
            }
        }

        /// <summary>
        /// This is an email tester method if the email can be sent from the account
        /// </summary>
        /// <param name="tester"></param>
        public EmailSender(bool tester)
        {
            //Using the smtp client we try to make a connection
            using (SmtpClient smtpClient = new SmtpClient())
            {
                //Set the credentials as the ppy user name and the email password
                NetworkCredential basicCredential =
                                    new NetworkCredential(Properties.Settings.Default.UserName, Properties.Settings.Default.Password);
                //Create a new message
                MailMessage message = new MailMessage();

                //Declare a from address - who is sending the email
                MailAddress fromAddress = new MailAddress("no-reply@yorku.ca");

                //Declare the host
                smtpClient.Host = "mailrelay.yorku.ca";

                //THe Port 587 = SATALITE 
                smtpClient.Port = 587;

                //Don't use the default credentials
                smtpClient.UseDefaultCredentials = false;

                //Use SSL for secure connections
                smtpClient.EnableSsl = true;

                //Use the basic credentials declared above to make the connection.
                smtpClient.Credentials = basicCredential;

                //Set the from
                message.From = fromAddress;

                //Add a subject
                message.Subject = "This is a test message.";

                //Set IsBodyHtml to true means you can send HTML email.
                message.IsBodyHtml = true;
                message.Body = "<h3>This message was sent to test the connection to the smtp server from the CLog.exe application.</h3>";
                message.Body += "<p>This request was made from " + this.getIPofMachine() + 
                                ", if you did not make this request please consider chaning your passwords.</p>";
                message.Body += "This message was auto generated at " + now.ToString();

                //Add a To address to send the email to.
                message.To.Add(Properties.Settings.Default.UserName + "@yorku.ca");

                try
                {
                    //Send the email!
                    smtpClient.Send(message);
                    //If all is okay we are connected and good to go.
                    connectionMade = true;
                }
                catch (Exception)
                {
                    throw new Exception("Unable to send test email. Please check login credentials");
                }
            }
        }

        /// <summary>
        /// Return whether a connection was made
        /// </summary>
        /// <returns></returns>
        public bool isConnectionMade()
        {
            return connectionMade;
        }

        /// <summary>
        /// Returns the current IPaddress of this machine
        /// </summary>
        /// <returns></returns>
        private string getIPofMachine()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("Local IP Address Not Found!");
        }
    }
}
