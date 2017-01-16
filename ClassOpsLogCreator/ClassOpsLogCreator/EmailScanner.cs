using System;

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

            //If the password is different from the current saved password we update the current saved password
            if(password != Properties.Settings.Default.gmailPassword)
            {
                Properties.Settings.Default.gmailPassword = password;
                Properties.Settings.Default.Save();
            }

            try
            {   
                //**********************************TEST CODE*******************************************************/
                MailRepository mailRepo = new MailRepository(hostname, 993, true, username, password);

                 var emailList = mailRepo.GetAllMails("inbox");

                 foreach(ActiveUp.Net.Mail.Message ms in emailList)
                 {
                    //We Check if today has the newest schedule
                     if(ms.Subject == "Room Report for " + dayOfTheWeek && 
                         (ms.Date.ToString("dd-MM-yyyy") == today.ToString("dd-MM-yyyy")))
                     {
                         this.msgFrom = ms.From.Email;
                         this.msgBody = ms.BodyText.Text;
                        return;
                     }
                     //Or if yesterday has a schedule we can work with.
                     else if(ms.Subject == "Room Report for " + dayOfTheWeek &&
                         (ms.Date.ToString("dd-MM-yyyy") == today.AddDays(-1).ToString("dd-MM-yyyy")))
                    {
                        this.msgFrom = ms.From.Email;
                        this.msgBody = ms.BodyText.Text;
                        return;
                    }
                 }
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
