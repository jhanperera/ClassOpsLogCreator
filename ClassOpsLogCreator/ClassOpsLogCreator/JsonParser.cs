using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ClassOpsLogCreator
{
    /// <summary>
    /// This class will log into the ftp server and retrieve the json file that 
    /// contains the user credentials. 
    /// 
    /// This also allows credentials to be updated and pushed to the ftp server.
    /// </summary>
    class JsonParser
    {
        private string url = "ftp://publish.yorku.ca/MyWebSite/classops/CLog_Deploy/cred.json";
        private dynamic Json = null;

        /// <summary>
        /// The constructor that will open the ftp connection, download and parse the json file
        /// </summary>
        public JsonParser()
        {
            try
            {
                //Open an ftp request from the URL
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url) ;
                //Request the download method
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                //Pass it the credentials
                request.Credentials = new NetworkCredential("csstaff", "dr4g0n12");
                //Get the response
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                //Open a steam to read in the file
                Stream responseStream = response.GetResponseStream();
                //Read in the file from the response steam
                StreamReader reader = new StreamReader(responseStream);
                //Save it as a var
                var json = reader.ReadToEnd();
                //Read in the json file as a dynamic
                Json = JsonConvert.DeserializeObject<dynamic>(json);
               
                //Close the readers and the response steams and the ftp connection
                reader.Close();
                response.Close();                               
            }
            catch (Exception)
            {
                throw;
            }
        } 
        
        /// <summary>
        /// Returns the password saved in the json file
        /// </summary>
        /// <returns></returns>
        public string getPassword()
        {
            string password = Json.cred;
            return password;
        }  

        public void updateJson(string newPass)
        {
            //Open an ftp request from the URL
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
            //Request the download method
            request.Method = WebRequestMethods.Ftp.DownloadFile;
            //Pass it the credentials
            request.Credentials = new NetworkCredential("csstaff", "dr4g0n12");

            Json.cred = newPass;

            //write to a file called cred.json
            string save = JsonConvert.SerializeObject(Json);

            StreamReader sourceStream = new StreamReader(Json);
            byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            sourceStream.Close();
            request.ContentLength = fileContents.Length;

            Stream requestStream = request.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);
            requestStream.Close();

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            response.Close();

        }
    }
}
