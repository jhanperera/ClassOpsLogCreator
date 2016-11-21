using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassOpsLogCreator;

namespace ClassOpsUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Create_TaskRank_Check_Ranks()
        {
            TaskRanks tr = new TaskRanks();

            int taskRank = tr.getTaskValue("Demo");
            //We should get 
            Assert.AreEqual(3, taskRank);
        }

        [TestMethod]
        public void Create_TaskRank_Check_TotalTaskRanks()
        {
            TaskRanks tr = new TaskRanks();

            string[,] input = new string[,]
            {
                { "","Crestron Logout","7/27/16", "1400","R","N102", "" },
                { "","Crestron Logout","7/27/16", "1400","R","N102", "" },
                { "","Crestron Logout","7/27/16", "1400","R","N102", "" },
                { "","Crestron Logout","7/27/16", "1400","R","N102", "" },
                { "","CLOSE ACE017","7/27/16", "1400","R","N102", "" },
                { "","CLOSE ACE017","7/27/16", "1400","R","N102", "" },

            };
            int taskRank = tr.getTotalTaskValue(input);
            //We should get 
            Assert.AreEqual(6, taskRank);
        }

        [TestMethod]
        public void Create_Email_Scanner()
        {
            EmailScanner es = new EmailScanner();
            Assert.IsTrue(es.isConnected());
        }

        [TestMethod]
        public void Create_Email_Scanner_Check_MsgFrom()
        {
            EmailScanner es = new EmailScanner();

            string msgFrom = es.messageFrom();

            Assert.AreEqual("pereraj@yorku.ca", msgFrom);
        }
    }
}
