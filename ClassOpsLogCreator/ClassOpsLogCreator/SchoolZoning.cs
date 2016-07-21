using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 
/// Author: Jhan Perera
/// Department: UIT Client Services
/// 
/// 
/// Description of class: this class will mapp the whole school
/// Simple distance calculations are made to determine disatnce 
/// and zone accordingly.
///
/// Class Version: 0.1.0.0 - BETA - 7152016
/// 
/// System Version: 0.1.0.0 - BETA - 7152016
/// 
/// </summary>
namespace ClassOpsLogCreator
{
    public class SchoolZoning
    {
        private Graph schoolGraph;
        private int numberOfBuildings;
        private int numberOfConnections;

        /// <summary>
        /// This will create a the map for the school and 
        /// provide features for school map as well
        /// </summary>
        public SchoolZoning()
        {
            //Create the graph and add he edges acroding to the 
            schoolGraph = new Graph();
            this.numberOfBuildings = schoolGraph.numberOfVerticies();
            //Add all the connections here 
            //ACE
            schoolGraph.addEdge("ACE", "KT");
            //ACW
            schoolGraph.addEdge("ACW", "YL");
            schoolGraph.addEdge("ACW", "VH");
            schoolGraph.addEdge("ACW", "STC");
            //BC
            schoolGraph.addEdge("BC", "LSB");
            schoolGraph.addEdge("BC", "CB");
            schoolGraph.addEdge("BC", "SC");
            schoolGraph.addEdge("BC", "CC");
            //BSC
            schoolGraph.addEdge("BCS", "ELC");
            schoolGraph.addEdge("BCS", "TEL");
            //BSB
            schoolGraph.addEdge("BSB", "STC");
            schoolGraph.addEdge("BSB", "R");
            schoolGraph.addEdge("BSB", "VH");
            schoolGraph.addEdge("BSB", "CSQ");
            schoolGraph.addEdge("BSB", "CLH");
            schoolGraph.addEdge("BSB", "LAS");
            schoolGraph.addEdge("BSB", "FRQ");
            schoolGraph.addEdge("BSB", "SLH");
            //BRG
            schoolGraph.addEdge("BRG", "SCL");
            schoolGraph.addEdge("BRG", "SC");
            schoolGraph.addEdge("BRG", "CB");
            schoolGraph.addEdge("BRG", "PSE");
            //CS
            schoolGraph.addEdge("CB", "PSE");
            schoolGraph.addEdge("CB", "LSB");
            //CFA
            schoolGraph.addEdge("CFA", "ATK");
            schoolGraph.addEdge("CFA", "ACW");
            //CFT
            schoolGraph.addEdge("CFT", "ACW");
            schoolGraph.addEdge("CFT", "KT");
            schoolGraph.addEdge("CFT", "YL");
            //CSQ
            schoolGraph.addEdge("CSQ", "CLH");
            schoolGraph.addEdge("CSQ", "BSB");
            schoolGraph.addEdge("CSQ", "R");
            schoolGraph.addEdge("CSQ", "HNE");
            schoolGraph.addEdge("CSQ", "SCL");
            //CLH
            schoolGraph.addEdge("CLH", "LAS");
            schoolGraph.addEdge("CLH", "FRQ");
            schoolGraph.addEdge("CLH", "BSB");
            schoolGraph.addEdge("CLH", "R");
            schoolGraph.addEdge("CLH", "CSQ");
            schoolGraph.addEdge("CLH", "SCL");
            schoolGraph.addEdge("CLH", "PSE");
            //ELC 
            schoolGraph.addEdge("ELC", "TEL");
            schoolGraph.addEdge("ELC", "SSB");
            schoolGraph.addEdge("ELC", "ACE");
            //LAS
            schoolGraph.addEdge("LAS", "PSE");
            schoolGraph.addEdge("LAS", "LUM");
            schoolGraph.addEdge("LAS", "FRQ");
            schoolGraph.addEdge("LAS", "BSB");
            schoolGraph.addEdge("LAS", "CLH");
            //R
            schoolGraph.addEdge("R", "HNE");
            schoolGraph.addEdge("R", "CSQ");
            schoolGraph.addEdge("R", "CLH");
            schoolGraph.addEdge("R", "BSB");
            schoolGraph.addEdge("R", "STC");
            schoolGraph.addEdge("R", "VH");
            schoolGraph.addEdge("R", "ATK");
            //SLH
            schoolGraph.addEdge("SLH", "FC");
            schoolGraph.addEdge("SLH", "VC");
            schoolGraph.addEdge("SLH", "YL");
            schoolGraph.addEdge("SLH", "STC");
            schoolGraph.addEdge("SLH", "BSB");
            schoolGraph.addEdge("SLH", "FRQ");
            schoolGraph.addEdge("SLH", "LUM");
            //SSB
            schoolGraph.addEdge("SSB", "ACE");
            schoolGraph.addEdge("SSB", "TEL");
            //TEL
            schoolGraph.addEdge("TEL", "ACE");
            schoolGraph.addEdge("TEL", "CFT");
            schoolGraph.addEdge("TEL", "ACW");
            schoolGraph.addEdge("TEL", "CFA");
            //VC
            schoolGraph.addEdge("VC", "WC");
            schoolGraph.addEdge("VC", "MC");
            schoolGraph.addEdge("VC", "FC");
            schoolGraph.addEdge("VC", "SLH");
            schoolGraph.addEdge("VC", "YL");
            schoolGraph.addEdge("VC", "KT");
            //YL
            schoolGraph.addEdge("YL", "KT");
            schoolGraph.addEdge("YL", "STC");
            schoolGraph.addEdge("YL", "CFT");
            schoolGraph.addEdge("YL", "ACW");
            schoolGraph.addEdge("YL", "VH");
            schoolGraph.addEdge("YL", "SLH");
            this.numberOfConnections = schoolGraph.numberOfEdges();
        }

        /// <summary>
        /// This reuturn how many buildings we are working with
        /// </summary>
        /// <returns></returns>
        public int getNumberOfBuilding()
        {
            return this.numberOfBuildings;
        }

        /// <summary>
        /// This returns how many connections we have in the graph
        /// </summary>
        /// <returns></returns>
        public int getNumberOfConnections()
        {
            return this.numberOfConnections;
        }

        /// <summary>
        /// This method will find the hosrtest path from the root
        /// to all other nodes within a given distance
        /// </summary>
        /// <param name="root"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public Queue<string> BFS(string root, int distance)
        {
            //Create the queue and the mark array
            Queue<string> reachable = new Queue<string>();
            string[] mark = new string[numberOfBuildings];
            int index = 0;
            int distanceCount = 0;

            //Add the root to the visited list
            mark[index] = root;
            reachable.Enqueue(root);

            //While our queue is not empty and while we are within the distance
            while(reachable.Count > 0 && distanceCount < distance)
            {
                //Pop the top of the queue
                string current = reachable.Dequeue();
                //Look at all the adjacent 
                foreach(string v in schoolGraph.adjacentTo(current))
                {
                    if(!(mark.Contains(v)))
                    {
                        index++;
                        mark[index] = v;
                        reachable.Enqueue(v);
                    }
                }
                distanceCount++;
            }
            return reachable;
        }
    }
}
