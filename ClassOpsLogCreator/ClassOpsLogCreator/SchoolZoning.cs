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
        
        /**All the methods under here are not using BFS yet
         * The BFS algo needs to be modified to work correctly
         * before we use it to be dynamic and "Smart"
         */

        /// <summary>
        /// This will return a List of buildings that are in one of two zones
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public List<string> getZone_2(int i)
        {
            string[] zone1 = new string[]
            {
                "MC", "WC", "VC", "FC", "LUM", "LSB", "CC", "BC", "CB", "PSE",
                "LAS", "FRQ", "SLH", "KT", "YL", "STC", "BSB", "SC"
            };

            string[] zone2 = new string[]
            {
                "CLH", "BRG", "SCL", "CSQ", "R", "VH", "ACW", "CFT", "ACE", "SSB",
                "ELC", "TEL", "CFA", "HNE", "OSG", "ATK", "BCS"
            };

            List <string> buildingList = new List<string>();
            //North Zone
            if(i == 1)
            {
                buildingList.AddRange(zone1);
            }
            //South Zone
            else
            {
                buildingList.AddRange(zone2);
            }

            return buildingList;
        }

        /// <summary>
        /// This will return a List of buildings that are in one of three zones
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public List<string> getZone_3(int i)
        {
            string[] zone1 = new string[]
            {
                "MC", "WC", "VC", "FC", "LUM", "FRQ", "SLH", "BSB", "STC", "YL", "KT"
            };

            string[] zone2 = new string[]
            {
                "LSB", "CC", "BC", "CB", "PSE", "LAS", "SC", "CLH", "BRG", "SCL", "CSQ"
            };

            string[] zone3 = new string[]
            {
                "R", "VH", "ACW", "CFT", "ACE", "SSB", "HNE", "CFA", "TEL", "ELC", "OSG", "ATK", "BCS"
            };

            List<string> buildingList = new List<string>();
            if (i == 1)
            {
                buildingList.AddRange(zone1);
            }
            else if (i == 2)
            {
                buildingList.AddRange(zone2);
            }
            else
            {
                buildingList.AddRange(zone3);
            }

            return buildingList;
        }

        /// <summary>
        /// This will return a List of buildings that are in one of four zones
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public List<string> getZone_4(int i)
        {
            string[] zone1 = new string[]
            {
                "MC", "VC", "WC", "FC", "SLH", "STC", "YL", "KT" 
            };

            string[] zone2 = new string[]
            {
                "LUM", "FRQ", "LAS", "PSE", "CB", "BC", "CC", "LSB", "SC"
            };

            string[] zone3 = new string[]
            {
                "CLH", "BSB", "BRG", "SCL", "CSQ", "R", "VH", "HNE", "OSG"
            };

            string[] zone4 = new string[]
            {
                "ATK", "ACW", "CFT", "ACE", "SSB", "CFA", "TEL", "ELC", "BCS"
            };

            List<string> buildingList = new List<string>();
            if (i == 1)
            {
                buildingList.AddRange(zone1);
            }
            else if(i == 2)
            {
                buildingList.AddRange(zone2);
            }
            else if(i == 3)
            {
                buildingList.AddRange(zone3);
            }
            else
            {
                buildingList.AddRange(zone4);
            }
            return buildingList;
        }

        /// <summary>
        /// This will return a List of buildings that are in one of five zones
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public List<string> getZone_5(int i)
        {
            string[] zone1 = new string[]
             {
                "MC", "VC", "WC", "FC", "SLH", "YL", "KT"
             };

            string[] zone2 = new string[]
            {
                "LAS", "PSE", "CB", "BC", "CC", "LSB", "SC"
            };

            string[] zone3 = new string[]
            {
                "BRG", "SCL", "CLH", "CSQ", "HNE", "OSG", "R"
            };

            string[] zone4 = new string[]
            {
                "LUM", "FRQ", "BSB", "STC", "VH", "ATK", "ACW"
            };

            string[] zone5 = new string[]
            {
                "CFA", "CFT", "ACE", "TEL", "SSB", "ELC", "BCS"
            };

            List<string> buildingList = new List<string>();
            if (i == 1)
            {
                buildingList.AddRange(zone1);
            }
            else if (i == 2)
            {
                buildingList.AddRange(zone2);
            }
            else if (i == 3)
            {
                buildingList.AddRange(zone3);
            }
            else if (i == 4)
            {
                buildingList.AddRange(zone4);
            }
            else
            {
                buildingList.AddRange(zone5);
            }

            return buildingList;
        }

        /// <summary>
        /// This will return a List of buildings that are in one of six zones
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public List<string> getZone_6(int i)
        {
            string[] zone1 = new string[]
             {
                "MC", "VC", "WC", "FC", "LUM", "SLH"
             };

            string[] zone2 = new string[]
            {
                "PSE", "CB", "BC", "CC", "LSB", "SC"
            };

            string[] zone3 = new string[]
            {
                "LAS", "CLH", "BSB", "FRQ", "YL", "STC"
            };

            string[] zone4 = new string[]
            {
                "KT", "ACE", "TEL", "SSB", "ELC", "BCS"
            };

            string[] zone5 = new string[]
            {
                "CFT", "ACW", "CFA", "ATK", "VH", "R"
            };

            string[] zone6= new string[]
            {
                "BRG", "SCL", "CSQ", "HNE", "OSG"
            };

            List<string> buildingList = new List<string>();
            if (i == 1)
            {
                buildingList.AddRange(zone1);
            }
            else if (i == 2)
            {
                buildingList.AddRange(zone2);
            }
            else if (i == 3)
            {
                buildingList.AddRange(zone3);
            }
            else if (i == 4)
            {
                buildingList.AddRange(zone4);
            }
            else if (i == 5)
            {
                buildingList.AddRange(zone5);
            }
            else
            {
                buildingList.AddRange(zone6);
            }
            return buildingList;
        }

        /// <summary>
        /// This will return a List of buildings that are in one of seven zones
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public List<string> getZone_7(int i)
        {
            string[] zone1 = new string[]
             {
                "MC", "VC", "WC", "FC", "SLH"
             };

            string[] zone2 = new string[]
            {
                "CB", "BC", "CC", "LSB", "SC"
            };

            string[] zone3 = new string[]
            {
                "PSE", "LAS", "LUM", "FRQ", "CLH"
            };

            string[] zone4 = new string[]
            {
               "BSB", "STC", "VH", "R", "ATk"
            };

            string[] zone5 = new string[]
            {
                "YL", "KT", "ACW", "CFA", "CFT"
            };

            string[] zone6 = new string[]
            {
                "BRG", "SCL", "CSQ", "HNE", "OSG"
            };

            string[] zone7 = new string[]
            {
                "ACE", "SSB", "TEL", "ELC", "BCS"
            };

            List<string> buildingList = new List<string>();
            if (i == 1)
            {
                buildingList.AddRange(zone1);
            }
            else if (i == 2)
            {
                buildingList.AddRange(zone2);
            }
            else if (i == 3)
            {
                buildingList.AddRange(zone3);
            }
            else if (i == 4)
            {
                buildingList.AddRange(zone4);
            }
            else if (i == 5)
            {
                buildingList.AddRange(zone5);
            }
            else if( i == 6)
            {
                buildingList.AddRange(zone6);
            }
            else
            {
                buildingList.AddRange(zone7);
            }
            return buildingList;
        }

        /// <summary>
        /// This will return a List of buildings that are in one of eight zones
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public List<string> getZone_8(int i)
        {
            string[] zone1 = new string[]
             {
                "MC", "VC", "WC", "FC", "SLH"
             };

            string[] zone2 = new string[]
            {
                "CB", "BC", "CC", "LSB", "SC"
            };

            string[] zone3 = new string[]
            {
                "PSE", "LAS", "LUM", "FRQ", "CLH"
            };

            string[] zone4 = new string[]
            {
               "BSB", "STC", "VH", "R"
            };

            string[] zone5 = new string[]
            {
                "YL", "KT", "ACW", "CFA", "CFT"
            };

            string[] zone6 = new string[]
            {
                "BRG", "SCL", "CSQ"
            };

            string[] zone7 = new string[]
            {
                "ACE", "SSB", "TEL", "ELC", "BCS"
            };
            string[] zone8 = new string[]
            {
                "ATK", "OSG", "HNE"
            };

            List<string> buildingList = new List<string>();
            if (i == 1)
            {
                buildingList.AddRange(zone1);
            }
            else if (i == 2)
            {
                buildingList.AddRange(zone2);
            }
            else if (i == 3)
            {
                buildingList.AddRange(zone3);
            }
            else if (i == 4)
            {
                buildingList.AddRange(zone4);
            }
            else if (i == 5)
            {
                buildingList.AddRange(zone5);
            }
            else if (i == 6)
            {
                buildingList.AddRange(zone6);
            }
            else if (i == 7)
            {
                buildingList.AddRange(zone7);
            }
            else
            {
                buildingList.AddRange(zone8);
            }
            return buildingList;
        }
        /// <summary>
        /// This method will find the hosrtest path from the root
        /// to all other nodes within a given distance
        /// </summary>
        /// <param name="root"></param>
        /// <param name="distance"></param>
        /// <returns></returns>
        public string[] BFS(string root, int distance)
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
            return mark = mark.Where(n => n != null).ToArray();
        }
    }
}
