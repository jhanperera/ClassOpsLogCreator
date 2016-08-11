using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

/// <summary>
/// 
/// Author: Jhan Perera
/// Department: UIT Client Services
/// 
/// 
///
/// Description of class: This class will create the graph for the school so we can quickly 
/// check distances and get shortest paths for the number of 
/// shifts that day. 
/// 
/// The Buildings are held in a Dictionary to map them to
/// an integer for the adjacecny matrix that is the
/// backbone of the graph. 
///
/// Class Version: 0.1.0.0 - BETA - 7182016
/// 
/// System Version: 0.1.0.0 - BETA - 7152016
/// 
/// </summary>
namespace ClassOpsLogCreator
{
    public class Graph
    {
        /// Private variables
        private Boolean[,] adjacecnyMatrix = null;
        private int vertexCount;
        private int edgeCount;
        private Dictionary<String, int> buildingDictionary = null;

        /// <summary>
        /// Constructor for the main graph
        /// </summary>
        /// <param name="VertexCount"></param>
        public Graph()
        {
            //Create the building dictionary 
            this.createBuildingDictionary();

            this.vertexCount = buildingDictionary.Count;
            this.edgeCount = 0;

            adjacecnyMatrix = new Boolean[buildingDictionary.Count, buildingDictionary.Count];
        }

        /// <summary>
        /// This method will create a edge/link between two buildings
        /// </summary>
        /// <param name="Building1"></param>
        /// <param name="Building2"></param>
        public void addEdge(string Building1, string Building2)
        {
            int i, j;
            buildingDictionary.TryGetValue(Building1, out i);
            buildingDictionary.TryGetValue(Building2, out j);

            if (i >= 0 && i < vertexCount && j >= 0 && j < vertexCount)
            {
                adjacecnyMatrix[i, j] = true;
                adjacecnyMatrix[j, i] = true;
                this.edgeCount++;
            }
        }

        /// <summary>
        /// This method will remove a edge/link between two buildings
        /// </summary>
        /// <param name="Building1"></param>
        /// <param name="Building2"></param>
        public void removeEdge(string Building1, string Building2)
        {
            int i, j;
            buildingDictionary.TryGetValue(Building1, out i);
            buildingDictionary.TryGetValue(Building2, out j);

            if (i >= 0 && i < vertexCount && j >= 0 && j < vertexCount)
            {
                adjacecnyMatrix[i, j] = false;
                adjacecnyMatrix[j, i] = false;
                this.edgeCount--;
            }
        }

        /// <summary>
        /// This methos returns the number of Vertex in the graph
        /// </summary>
        /// <returns></returns>
        public int numberOfVerticies()
        {
            return this.vertexCount;
        }

        /// <summary>
        /// This will return the number of edges in the graph
        /// </summary>
        /// <returns></returns>
        public int numberOfEdges()
        {
            return this.edgeCount;
        }

        /// <summary>
        /// This method will return true if there is an edge between builidng 1 
        /// building 2
        /// </summary>
        /// <param name="Building1"></param>
        /// <param name="Building2"></param>
        /// <returns></returns>
        public Boolean hasEdge(string Building1, string Building2)
        {
            return false;
        }

        /// <summary>
        /// This method will return a building name based on the numerical
        /// location in the matrix
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public string getVertexName(int location)
        {
            return buildingDictionary.FirstOrDefault(x => x.Value == location).Key;
        }

        /// <summary>
        /// This method will return all the buildinds adjacent to 
        /// the building in question
        /// </summary>
        /// <param name="building"></param>
        /// <returns></returns>
        public List<string> adjacentTo(string Building)
        {
            //Save the building ID and get the list read
            int buildingID;
            List<string> adjacentto = new List<string>();

            //Get the building ID
            buildingDictionary.TryGetValue(Building, out buildingID);

            //Look through the array to see what buildings are connected
            for (int i = 0; i < this.vertexCount; i++)
            {
                //Connected then add it the list.
                if (this.adjacecnyMatrix[buildingID, i] == true)
                {
                    adjacentto.Add(buildingDictionary.FirstOrDefault(x => x.Value == i).Key);
                }
            }
            //Return the list
            return adjacentto;
        }

        /// <summary>
        /// This method will create our Dictionary of buildinds and 
        /// a corresponding integer which represents a spot in the 
        /// matrix
        /// </summary>
        private void createBuildingDictionary()
        {
            buildingDictionary = new Dictionary<string, int>();
            buildingDictionary.Add("BCS", 0);
            buildingDictionary.Add("ELC", 1);
            buildingDictionary.Add("SSB", 2);
            buildingDictionary.Add("ACE", 3);
            buildingDictionary.Add("CFT", 4);
            buildingDictionary.Add("TEL", 5);
            buildingDictionary.Add("AK", 6);
            buildingDictionary.Add("CFA", 7);
            buildingDictionary.Add("ACW", 8);
            buildingDictionary.Add("VH", 9);
            buildingDictionary.Add("R", 10);
            buildingDictionary.Add("CSQ", 11);
            buildingDictionary.Add("HNE", 12);
            buildingDictionary.Add("OSG", 13);
            buildingDictionary.Add("SCL", 14);
            buildingDictionary.Add("CLH", 15);
            buildingDictionary.Add("BSB", 16);
            buildingDictionary.Add("STC", 17);
            buildingDictionary.Add("YL", 18);
            buildingDictionary.Add("KT", 19);
            buildingDictionary.Add("SLH", 20);
            buildingDictionary.Add("VC", 21);
            buildingDictionary.Add("FC", 22);
            buildingDictionary.Add("MC", 23);
            buildingDictionary.Add("WC", 24);
            buildingDictionary.Add("FRQ", 25);
            buildingDictionary.Add("LAS", 26);
            buildingDictionary.Add("PSE", 27);
            buildingDictionary.Add("CB", 28);
            buildingDictionary.Add("BRG", 29);
            buildingDictionary.Add("SC", 30);
            buildingDictionary.Add("BC", 31);
            buildingDictionary.Add("CC", 32);
            buildingDictionary.Add("LSB", 33);
            buildingDictionary.Add("LUM", 34);
        }
    }
}
