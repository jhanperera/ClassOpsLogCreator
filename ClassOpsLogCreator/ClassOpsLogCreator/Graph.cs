using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

/// <summary>
/// This class will create the graph for the school so we can quickly 
/// check distances and get shortest paths for the number of 
/// shifts that day. 
/// 
/// The Buildings are held in a Dictionary to map them to
/// an integer for the adjacecny matrix that is the
/// backbone of the graph. 
/// </summary>
namespace ClassOpsLogCreator
{
    public class Graph
    {
        /// Private variables
        private Boolean[,] adjacecnyMatrix = null;
        private int vertexCount = 0;
        private Dictionary<String, int> buildingDictionary = null;

        /// <summary>
        /// Constructor for the main graph
        /// </summary>
        /// <param name="VertexCount"></param>
        public Graph(int VertexCount)
        {
            this.vertexCount = VertexCount;
            adjacecnyMatrix = new Boolean[VertexCount, VertexCount];
        }

        /// <summary>
        /// This method will create a edge/link between two buildings
        /// </summary>
        /// <param name="Building1"></param>
        /// <param name="Building2"></param>
        public void addEdge(string Building1, string Building2)
        {
            //Check if building1 and building 2 are in the dictionary
            //if so get the integer value 
            // make matrix[i][j] = true
            // and matrix [j][i] = true

        }

        /// <summary>
        /// This method will remove a edge/link between two buildings
        /// </summary>
        /// <param name="Building1"></param>
        /// <param name="Building2"></param>
        public void removeEdge(string Building1, string Building2)
        {
            //Check if building1 and building 2 are in the dictionary
            //if so get the integer value 
            // make matrix[i][j] = false
            // and matrix [j][i] = false
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
        /// This method will create our Dictionary of buildinds and 
        /// a corresponding integer which represents a spot in the 
        /// matrix
        /// </summary>
        private void createBuildingDictionary()
        {
            buildingDictionary = new Dictionary<string, int>();
            buildingDictionary.Add("ACE", 0);
            s
        }

    }
}
