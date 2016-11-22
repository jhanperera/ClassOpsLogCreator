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
