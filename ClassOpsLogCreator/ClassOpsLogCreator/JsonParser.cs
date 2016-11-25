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

        /// <summary>
        /// This method will update the Json file with a new password 
        /// 
        /// and write it to the ftp server for future updates. 
        /// </summary>
        /// <param name="newPass"></param>
        public void updateJson(string newPass)
        {
            try
            {
                //Open an ftp request from the URL
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
                //Request the download method
                request.Method = WebRequestMethods.Ftp.UploadFile;
                //Pass it the credentials
                request.Credentials = new NetworkCredential("csstaff", "dr4g0n12");

                //Upadate the password
                Json.cred = newPass;

                //write to a file called cred.json
                string save = JsonConvert.SerializeObject(Json);
                File.WriteAllText("cred.json", save);

                using (StreamWriter file = File.CreateText("cred.json"))
                {
                    //Serialize the data to JSON
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, Json);
                }

                //Upload the file the ftp server
                StreamReader sourceStream = new StreamReader("cred.json");
                byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                sourceStream.Close();
                request.ContentLength = fileContents.Length;

                Stream requestStream = request.GetRequestStream();
                requestStream.Write(fileContents, 0, fileContents.Length);
                requestStream.Close();

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                response.Close();

                //Delete the file so we don't have a trace of it. 
                File.Delete("cred.json");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
