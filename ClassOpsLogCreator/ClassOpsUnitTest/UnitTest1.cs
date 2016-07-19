using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassOpsLogCreator;

namespace ClassOpsUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Create_and_check_Vertex()
        {
            Graph g = new Graph();

            int check = g.numberOfVerticies();
            //We should get 
            Assert.AreEqual((int)35, (int) check);
        }

        [TestMethod]
        public void Create_and_check_Edges()
        {
            Graph g = new Graph();

            int check = g.numberOfEdges();
            //We should get 
            Assert.AreEqual(0, (int)check);
        }

        [TestMethod]
        public void Create_add_Edge_Check_Edge()
        {
            Graph g = new Graph();

            g.addEdge("BCS", "ELC");
            int check = g.numberOfEdges();
            //We should get 
            Assert.AreEqual(1, (int)check);
        }

        [TestMethod]
        public void Create_remove_Edge_Check_Edge()
        {
            Graph g = new Graph();

            g.addEdge("BCS", "ELC");
            g.removeEdge("BCS", "ELC");
            int check = g.numberOfEdges();
            //We should get 
            Assert.AreEqual(0, (int)check);
        }

        [TestMethod]
        public void Create_get_Building_Name()
        {
            Graph g = new Graph();

            
            string check = g.getVertexName(0);
            //We should get 
            Assert.AreEqual("BCS", check);
        }

    }
}
