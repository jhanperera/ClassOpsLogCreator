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

        [TestMethod]
        public void Create_and_Test_AdjacentTo()
        {
            Graph g = new Graph();
            List<string> check1 = new List<string>();
            check1.Add("ELC");
            List<string> check2 = new List<string>();
            check2.Add("BCS");

            g.addEdge("BCS", "ELC");   
            List<string> weGet1 = g.adjacentTo("BCS");
            List<string> weGet2 = g.adjacentTo("ELC");

            //We should get
            CollectionAssert.AreEqual(check1, weGet1);
            CollectionAssert.AreEqual(check2, weGet2);
        }


        [TestMethod]
        public void Create_SchoolZoning_And_Check()
        {
            SchoolZoning sz = new SchoolZoning();

            int check = sz.getNumberOfBuilding();
            //We should get 
            Assert.AreEqual((int)35, (int)check);
        }

        [TestMethod]
        public void Create_SchoolZoning_And_Check_Connections()
        {
            SchoolZoning sz = new SchoolZoning();

            int check = sz.getNumberOfConnections();
            //We should get 
            Assert.AreEqual((int)81, (int)check);
        }

        [TestMethod]
        public void Create_SchoolZoning_Check_BFS()
        {
            SchoolZoning sz = new SchoolZoning();
            Queue<string> actual = sz.BFS("BSB", 2);

            Queue<string> check = new Queue<string>();
            //We should get
            CollectionAssert.AreEqual(check, actual);
        }

    }
}
