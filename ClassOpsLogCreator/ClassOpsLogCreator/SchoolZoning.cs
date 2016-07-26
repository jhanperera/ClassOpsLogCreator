using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

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
            string[] zone1 = new string[] // North
            {
                "MC", "WC", "VC", "FC", "LUM", "LSB", "CC", "BC", "CB", "PSE",
                "SC", "LAS", "FRQ", "SLH"
            };

            string[] zone2 = new string[] //Central
            {
                "CLH", "BSB", "STC", "BRG", "SCL", "STC", "CSQ", "R", "VH",
                "ACW", "HNE"
            };

            string[] zone3 = new string[] //South
            {
                "CFT", "ACE", "SSB", "CFA", "TEL", "ELC", "BGS", "ATK", "OSG", "YL",
                "KT"
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
        /// This method is resposible for the zoning of all the elements in the range.
        /// This will return an array with all the elements in it and with zones.
        /// </summary>
        /// <param name="range"></param>
        /// <param name="shiftNumber"></param>
        /// <returns></returns>
        public string[,] generateZonedLog(Excel.Range range, int shiftNumber)
        {
            //Setting up the ranges and the variables
            System.Array rangeArray = (System.Array)range.Cells.Value2;
            string[,] zonedArray = covertToArray(rangeArray);
            string[,] result = null;

            //If we have 2 shifts
            if (shiftNumber == 2)
            {
                List<string> zone1 = getZone_2(1);
                List<string> zone2 = getZone_2(2);
                string[,] zone1Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone2Array = new string[zonedArray.GetUpperBound(0), 7];
                int zone1Index = 0;
                int zone2Index = 0;

                for (int i = 0; i <= zonedArray.GetUpperBound(0); i++)
                {
                    DateTime temp;
                    if ((!DateTime.TryParse(zonedArray[i, 2], out temp)))
                    {
                        zonedArray[i, 2] = DateTime.FromOADate(double.Parse((string)zonedArray[i, 2].ToString())).ToString("M/dd/yy");
                    }

                    if (zone1.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone1Array, zone1Index * zone1Array.GetLength(1), zonedArray.GetLength(1));
                        zone1Index++;
                    }
                    else if (zone2.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone2Array, zone2Index * zone2Array.GetLength(1), zonedArray.GetLength(1));
                        zone2Index++;
                    }
                }
                //Remove all empty rows
                zone1Array = ZoneSuperLogImporter.RemoveEmptyRows(zone1Array);
                zone2Array = ZoneSuperLogImporter.RemoveEmptyRows(zone2Array);
                result = new string[zone1Array.GetLength(0) + zone2Array.GetLength(0), 7];
                //Merge the arrays together
                AddToArray(result, zone1Array);
                AddToArray(result, zone2Array, zone1Array.GetLength(0));
            }

            //If we have 3 Shifts
            else if (shiftNumber == 3)
            {
                List<string> zone1 = getZone_3(1);
                List<string> zone2 = getZone_3(2);
                List<string> zone3 = getZone_3(3);
                string[,] zone1Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone2Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone3Array = new string[zonedArray.GetUpperBound(0), 7];
                int zone1Index = 0;
                int zone2Index = 0;
                int zone3Index = 0;

                for (int i = 0; i <= zonedArray.GetUpperBound(0); i++)
                {
                    DateTime temp;
                    if ((!DateTime.TryParse(zonedArray[i, 2], out temp)))
                    {
                        zonedArray[i, 2] = DateTime.FromOADate(double.Parse(zonedArray[i, 2].ToString())).ToString("M/dd/yy");
                    }
                    if (zone1.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone1Array, zone1Index * zone1Array.GetLength(1), zonedArray.GetLength(1));
                        zone1Index++;
                    }
                    else if (zone2.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone2Array, zone2Index * zone2Array.GetLength(1), zonedArray.GetLength(1));
                        zone2Index++;
                    }
                    else if (zone3.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone3Array, zone3Index * zone3Array.GetLength(1), zonedArray.GetLength(1));
                        zone3Index++;
                    }
                }
                //Remove all empty rows
                zone1Array = ZoneSuperLogImporter.RemoveEmptyRows(zone1Array);
                zone2Array = ZoneSuperLogImporter.RemoveEmptyRows(zone2Array);
                zone3Array = ZoneSuperLogImporter.RemoveEmptyRows(zone3Array);
                result = new string[zone1Array.GetLength(0) + zone2Array.GetLength(0) + zone3Array.GetLength(0), 7];
                //Merge the arrays together
                AddToArray(result, zone1Array);
                AddToArray(result, zone2Array, zone1Array.GetLength(0));
                AddToArray(result, zone3Array, zone1Array.GetLength(0) + zone2Array.GetLength(0));
            }

            //If we have 4 Shifts
            else if (shiftNumber == 4)
            {
                List<string> zone1 = getZone_4(1);
                List<string> zone2 = getZone_4(2);
                List<string> zone3 = getZone_4(3);
                List<string> zone4 = getZone_4(4);
                string[,] zone1Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone2Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone3Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone4Array = new string[zonedArray.GetUpperBound(0), 7];
                int zone1Index = 0;
                int zone2Index = 0;
                int zone3Index = 0;
                int zone4Index = 0;

                for (int i = 0; i <= zonedArray.GetUpperBound(0); i++)
                {
                    DateTime temp;
                    if ((!DateTime.TryParse(zonedArray[i, 2], out temp)))
                    {
                        zonedArray[i, 2] = DateTime.FromOADate(double.Parse((string)zonedArray[i, 2].ToString())).ToString("M/dd/yy");
                    }
                    if (zone1.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone1Array, zone1Index * zone1Array.GetLength(1), zonedArray.GetLength(1));
                        zone1Index++;
                    }
                    else if (zone2.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone2Array, zone2Index * zone2Array.GetLength(1), zonedArray.GetLength(1));
                        zone2Index++;
                    }
                    else if (zone3.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone3Array, zone3Index * zone3Array.GetLength(1), zonedArray.GetLength(1));
                        zone3Index++;
                    }
                    else if (zone4.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone4Array, zone4Index * zone4Array.GetLength(1), zonedArray.GetLength(1));
                        zone4Index++;
                    }
                }
                //Remove all empty rows
                zone1Array = ZoneSuperLogImporter.RemoveEmptyRows(zone1Array);
                zone2Array = ZoneSuperLogImporter.RemoveEmptyRows(zone2Array);
                zone3Array = ZoneSuperLogImporter.RemoveEmptyRows(zone3Array);
                zone4Array = ZoneSuperLogImporter.RemoveEmptyRows(zone4Array);
                result = new string[zone1Array.GetLength(0) + zone2Array.GetLength(0) + zone3Array.GetLength(0) + zone4Array.GetLength(0), 7];
                //Merge the arrays together
                AddToArray(result, zone1Array);
                AddToArray(result, zone2Array, zone1Array.GetLength(0));
                AddToArray(result, zone3Array, zone1Array.GetLength(0) + zone2Array.GetLength(0));
                AddToArray(result, zone4Array, zone1Array.GetLength(0) + zone2Array.GetLength(0) + zone3Array.GetLength(0));
            }

            //If we have 5 Shifts
            else if (shiftNumber == 5)
            {
                List<string> zone1 = getZone_5(1);
                List<string> zone2 = getZone_5(2);
                List<string> zone3 = getZone_5(3);
                List<string> zone4 = getZone_5(4);
                List<string> zone5 = getZone_5(5);
                string[,] zone1Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone2Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone3Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone4Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone5Array = new string[zonedArray.GetUpperBound(0), 7];
                int zone1Index = 0;
                int zone2Index = 0;
                int zone3Index = 0;
                int zone4Index = 0;
                int zone5Index = 0;

                for (int i = 0; i <= zonedArray.GetUpperBound(0); i++)
                {
                    DateTime temp;
                    if ((!DateTime.TryParse(zonedArray[i, 2], out temp)))
                    {
                        zonedArray[i, 2] = DateTime.FromOADate(double.Parse((string)zonedArray[i, 2].ToString())).ToString("M/dd/yy");
                    }
                    if (zone1.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone1Array, zone1Index * zone1Array.GetLength(1), zonedArray.GetLength(1));
                        zone1Index++;
                    }
                    else if (zone2.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone2Array, zone2Index * zone2Array.GetLength(1), zonedArray.GetLength(1));
                        zone2Index++;
                    }
                    else if (zone3.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone3Array, zone3Index * zone3Array.GetLength(1), zonedArray.GetLength(1));
                        zone3Index++;
                    }
                    else if (zone4.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone4Array, zone4Index * zone4Array.GetLength(1), zonedArray.GetLength(1));
                        zone4Index++;
                    }
                    else if (zone5.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone5Array, zone5Index * zone5Array.GetLength(1), zonedArray.GetLength(1));
                        zone5Index++;
                    }
                }
                //Remove all empty rows
                zone1Array = ZoneSuperLogImporter.RemoveEmptyRows(zone1Array);
                zone2Array = ZoneSuperLogImporter.RemoveEmptyRows(zone2Array);
                zone3Array = ZoneSuperLogImporter.RemoveEmptyRows(zone3Array);
                zone4Array = ZoneSuperLogImporter.RemoveEmptyRows(zone4Array);
                zone5Array = ZoneSuperLogImporter.RemoveEmptyRows(zone5Array);

                result = new string[zone1Array.GetLength(0) + zone2Array.GetLength(0) +
                                        zone3Array.GetLength(0) + zone4Array.GetLength(0) +
                                        zone5Array.GetLength(0), 7];
                //Merge the arrays together
                AddToArray(result, zone1Array);
                AddToArray(result, zone2Array, zone1Array.GetLength(0));
                AddToArray(result, zone3Array, zone1Array.GetLength(0) + zone2Array.GetLength(0));
                AddToArray(result, zone4Array, zone1Array.GetLength(0) + zone2Array.GetLength(0) + zone3Array.GetLength(0));
                AddToArray(result, zone5Array, zone1Array.GetLength(0) + zone2Array.GetLength(0) +
                                    zone3Array.GetLength(0) + zone4Array.GetLength(0));
            }

            //If we have 6 Shifts
            else if (shiftNumber == 6)
            {
                List<string> zone1 = getZone_6(1);
                List<string> zone2 = getZone_6(2);
                List<string> zone3 = getZone_6(3);
                List<string> zone4 = getZone_6(4);
                List<string> zone5 = getZone_6(5);
                List<string> zone6 = getZone_6(6);
                string[,] zone1Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone2Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone3Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone4Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone5Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone6Array = new string[zonedArray.GetUpperBound(0), 7];
                int zone1Index = 0;
                int zone2Index = 0;
                int zone3Index = 0;
                int zone4Index = 0;
                int zone5Index = 0;
                int zone6Index = 0;

                for (int i = 0; i <= zonedArray.GetUpperBound(0); i++)
                {
                    DateTime temp;
                    if ((!DateTime.TryParse(zonedArray[i, 2], out temp)))
                    {
                        zonedArray[i, 2] = DateTime.FromOADate(double.Parse((string)zonedArray[i, 2].ToString())).ToString("M/dd/yy");
                    }
                    if (zone1.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone1Array, zone1Index * zone1Array.GetLength(1), zonedArray.GetLength(1));
                        zone1Index++;
                    }
                    else if (zone2.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone2Array, zone2Index * zone2Array.GetLength(1), zonedArray.GetLength(1));
                        zone2Index++;
                    }
                    else if (zone3.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone3Array, zone3Index * zone3Array.GetLength(1), zonedArray.GetLength(1));
                        zone3Index++;
                    }
                    else if (zone4.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone4Array, zone4Index * zone4Array.GetLength(1), zonedArray.GetLength(1));
                        zone4Index++;
                    }
                    else if (zone5.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone5Array, zone5Index * zone5Array.GetLength(1), zonedArray.GetLength(1));
                        zone5Index++;
                    }
                    else if (zone6.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone6Array, zone6Index * zone6Array.GetLength(1), zonedArray.GetLength(1));
                        zone6Index++;
                    }
                }
                //Remove all empty rows
                zone1Array = ZoneSuperLogImporter.RemoveEmptyRows(zone1Array);
                zone2Array = ZoneSuperLogImporter.RemoveEmptyRows(zone2Array);
                zone3Array = ZoneSuperLogImporter.RemoveEmptyRows(zone3Array);
                zone4Array = ZoneSuperLogImporter.RemoveEmptyRows(zone4Array);
                zone5Array = ZoneSuperLogImporter.RemoveEmptyRows(zone5Array);
                zone6Array = ZoneSuperLogImporter.RemoveEmptyRows(zone6Array);

                result = new string[zone1Array.GetLength(0) + zone2Array.GetLength(0) +
                                        zone3Array.GetLength(0) + zone4Array.GetLength(0) +
                                        zone5Array.GetLength(0) + zone6Array.GetLength(0), 7];
                //Merge the arrays together
                AddToArray(result, zone1Array);
                AddToArray(result, zone2Array, zone1Array.GetLength(0));
                AddToArray(result, zone3Array, zone1Array.GetLength(0) + zone2Array.GetLength(0));
                AddToArray(result, zone4Array, zone1Array.GetLength(0) + zone2Array.GetLength(0) + zone3Array.GetLength(0));
                AddToArray(result, zone5Array, zone1Array.GetLength(0) + zone2Array.GetLength(0) +
                                    zone3Array.GetLength(0) + zone4Array.GetLength(0));
                AddToArray(result, zone6Array, zone1Array.GetLength(0) + zone2Array.GetLength(0) +
                                    zone3Array.GetLength(0) + zone4Array.GetLength(0) + zone5Array.GetLength(0));
            }

            //If we have 7 shifts
            else if (shiftNumber == 7)
            {
                List<string> zone1 = getZone_7(1);
                List<string> zone2 = getZone_7(2);
                List<string> zone3 = getZone_7(3);
                List<string> zone4 = getZone_7(4);
                List<string> zone5 = getZone_7(5);
                List<string> zone6 = getZone_7(6);
                List<string> zone7 = getZone_7(7);
                string[,] zone1Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone2Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone3Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone4Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone5Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone6Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone7Array = new string[zonedArray.GetUpperBound(0), 7];
                int zone1Index = 0;
                int zone2Index = 0;
                int zone3Index = 0;
                int zone4Index = 0;
                int zone5Index = 0;
                int zone6Index = 0;
                int zone7Index = 0;

                for (int i = 0; i <= zonedArray.GetUpperBound(0); i++)
                {
                    DateTime temp;
                    if ((!DateTime.TryParse(zonedArray[i, 2], out temp)))
                    {
                        zonedArray[i, 2] = DateTime.FromOADate(double.Parse((string)zonedArray[i, 2].ToString())).ToString("M/dd/yy");
                    }
                    if (zone1.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone1Array, zone1Index * zone1Array.GetLength(1), zonedArray.GetLength(1));
                        zone1Index++;
                    }
                    else if (zone2.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone2Array, zone2Index * zone2Array.GetLength(1), zonedArray.GetLength(1));
                        zone2Index++;
                    }
                    else if (zone3.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone3Array, zone3Index * zone3Array.GetLength(1), zonedArray.GetLength(1));
                        zone3Index++;
                    }
                    else if (zone4.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone4Array, zone4Index * zone4Array.GetLength(1), zonedArray.GetLength(1));
                        zone4Index++;
                    }
                    else if (zone5.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone5Array, zone5Index * zone5Array.GetLength(1), zonedArray.GetLength(1));
                        zone5Index++;
                    }
                    else if (zone6.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone6Array, zone6Index * zone6Array.GetLength(1), zonedArray.GetLength(1));
                        zone6Index++;
                    }
                    else if (zone7.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone7Array, zone7Index * zone7Array.GetLength(1), zonedArray.GetLength(1));
                        zone7Index++;
                    }
                }
                //Remove all empty rows
                zone1Array = ZoneSuperLogImporter.RemoveEmptyRows(zone1Array);
                zone2Array = ZoneSuperLogImporter.RemoveEmptyRows(zone2Array);
                zone3Array = ZoneSuperLogImporter.RemoveEmptyRows(zone3Array);
                zone4Array = ZoneSuperLogImporter.RemoveEmptyRows(zone4Array);
                zone5Array = ZoneSuperLogImporter.RemoveEmptyRows(zone5Array);
                zone6Array = ZoneSuperLogImporter.RemoveEmptyRows(zone6Array);
                zone7Array = ZoneSuperLogImporter.RemoveEmptyRows(zone7Array);

                result = new string[zone1Array.GetLength(0) + zone2Array.GetLength(0) +
                                        zone3Array.GetLength(0) + zone4Array.GetLength(0) +
                                        zone5Array.GetLength(0) + zone6Array.GetLength(0) +
                                        zone7Array.GetLength(0), 7];
                //Merge the arrays together
                AddToArray(result, zone1Array);
                AddToArray(result, zone2Array, zone1Array.GetLength(0));
                AddToArray(result, zone3Array, zone1Array.GetLength(0) + zone2Array.GetLength(0));
                AddToArray(result, zone4Array, zone1Array.GetLength(0) + zone2Array.GetLength(0) + zone3Array.GetLength(0));
                AddToArray(result, zone5Array, zone1Array.GetLength(0) + zone2Array.GetLength(0) +
                                    zone3Array.GetLength(0) + zone4Array.GetLength(0));
                AddToArray(result, zone6Array, zone1Array.GetLength(0) + zone2Array.GetLength(0) +
                                    zone3Array.GetLength(0) + zone4Array.GetLength(0) + zone5Array.GetLength(0));
                AddToArray(result, zone7Array, zone1Array.GetLength(0) + zone2Array.GetLength(0) +
                                    zone3Array.GetLength(0) + zone4Array.GetLength(0) + zone5Array.GetLength(0) +
                                    zone6Array.GetLength(0));
            }

            //If we have 8 shifts
            else if (shiftNumber == 8)
            {
                List<string> zone1 = getZone_8(1);
                List<string> zone2 = getZone_8(2);
                List<string> zone3 = getZone_8(3);
                List<string> zone4 = getZone_8(4);
                List<string> zone5 = getZone_8(5);
                List<string> zone6 = getZone_8(6);
                List<string> zone7 = getZone_8(7);
                List<string> zone8 = getZone_8(8);
                string[,] zone1Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone2Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone3Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone4Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone5Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone6Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone7Array = new string[zonedArray.GetUpperBound(0), 7];
                string[,] zone8Array = new string[zonedArray.GetUpperBound(0), 7];
                int zone1Index = 0;
                int zone2Index = 0;
                int zone3Index = 0;
                int zone4Index = 0;
                int zone5Index = 0;
                int zone6Index = 0;
                int zone7Index = 0;
                int zone8Index = 0;

                for (int i = 0; i <= zonedArray.GetUpperBound(0); i++)
                {
                    DateTime temp;
                    if ((!DateTime.TryParse(zonedArray[i, 2], out temp)))
                    {
                        zonedArray[i, 2] = DateTime.FromOADate(double.Parse((string)zonedArray[i, 2].ToString())).ToString("M/dd/yy");
                    }
                    if (zone1.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone1Array, zone1Index * zone1Array.GetLength(1), zonedArray.GetLength(1));
                        zone1Index++;
                    }
                    else if (zone2.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone2Array, zone2Index * zone2Array.GetLength(1), zonedArray.GetLength(1));
                        zone2Index++;
                    }
                    else if (zone3.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone3Array, zone3Index * zone3Array.GetLength(1), zonedArray.GetLength(1));
                        zone3Index++;
                    }
                    else if (zone4.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone4Array, zone4Index * zone4Array.GetLength(1), zonedArray.GetLength(1));
                        zone4Index++;
                    }
                    else if (zone5.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone5Array, zone5Index * zone5Array.GetLength(1), zonedArray.GetLength(1));
                        zone5Index++;
                    }
                    else if (zone6.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone6Array, zone6Index * zone6Array.GetLength(1), zonedArray.GetLength(1));
                        zone6Index++;
                    }
                    else if (zone7.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone7Array, zone7Index * zone7Array.GetLength(1), zonedArray.GetLength(1));
                        zone7Index++;
                    }
                    else if (zone8.Contains(zonedArray[i, 4]))
                    {
                        Array.Copy(zonedArray, i * zonedArray.GetLength(1), zone8Array, zone8Index * zone8Array.GetLength(1), zonedArray.GetLength(1));
                        zone8Index++;
                    }
                }
                //Remove all empty rows
                zone1Array = ZoneSuperLogImporter.RemoveEmptyRows(zone1Array);
                zone2Array = ZoneSuperLogImporter.RemoveEmptyRows(zone2Array);
                zone3Array = ZoneSuperLogImporter.RemoveEmptyRows(zone3Array);
                zone4Array = ZoneSuperLogImporter.RemoveEmptyRows(zone4Array);
                zone5Array = ZoneSuperLogImporter.RemoveEmptyRows(zone5Array);
                zone6Array = ZoneSuperLogImporter.RemoveEmptyRows(zone6Array);
                zone7Array = ZoneSuperLogImporter.RemoveEmptyRows(zone7Array);
                zone8Array = ZoneSuperLogImporter.RemoveEmptyRows(zone8Array);

                result = new string[zone1Array.GetLength(0) + zone2Array.GetLength(0) +
                                        zone3Array.GetLength(0) + zone4Array.GetLength(0) +
                                        zone5Array.GetLength(0) + zone6Array.GetLength(0) +
                                        zone7Array.GetLength(0) + zone8Array.GetLength(0), 7];
                //Merge the arrays together
                AddToArray(result, zone1Array);
                AddToArray(result, zone2Array, zone1Array.GetLength(0));
                AddToArray(result, zone3Array, zone1Array.GetLength(0) + zone2Array.GetLength(0));
                AddToArray(result, zone4Array, zone1Array.GetLength(0) + zone2Array.GetLength(0) + zone3Array.GetLength(0));
                AddToArray(result, zone5Array, zone1Array.GetLength(0) + zone2Array.GetLength(0) +
                                    zone3Array.GetLength(0) + zone4Array.GetLength(0));
                AddToArray(result, zone6Array, zone1Array.GetLength(0) + zone2Array.GetLength(0) +
                                    zone3Array.GetLength(0) + zone4Array.GetLength(0) + zone5Array.GetLength(0));
                AddToArray(result, zone7Array, zone1Array.GetLength(0) + zone2Array.GetLength(0) +
                                    zone3Array.GetLength(0) + zone4Array.GetLength(0) + zone5Array.GetLength(0) +
                                    zone6Array.GetLength(0));
                AddToArray(result, zone8Array, zone1Array.GetLength(0) + zone2Array.GetLength(0) +
                                    zone3Array.GetLength(0) + zone4Array.GetLength(0) + zone5Array.GetLength(0) +
                                    zone6Array.GetLength(0) + zone7Array.GetLength(0));
            }


            //Return the merged array with the zones. 
            return result;
        }

        /// <summary>
        /// A Helper method to quickly convery an array object to a string array
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        private string[,] covertToArray(System.Array array)
        {
            string[,] values = new string[array.GetUpperBound(0), array.GetUpperBound(1)];
            for (int i = 0; i < array.GetUpperBound(0); i++)
            {
                for (int j = 0; j < array.GetUpperBound(1); j++)
                {
                    if (array.GetValue(i + 1, j + 1) == null)
                    {
                        values[i, j] = "";
                    }
                    else
                    {
                        values[i, j] = array.GetValue(i + 1, j + 1).ToString();

                    }
                }
            }
            return values;
        }

        /// <summary>
        /// This method combines two rectange arrays together.
        /// This writes it element by element. (Might have to switch this to BlockCopy for optimization)
        /// </summary>
        /// <param name="result"></param>
        /// <param name="array"></param>
        /// <param name="start"></param>
        static void AddToArray(string[,] result, string[,] array, int start = 0)
        {
            for (int i = 0; i < array.GetLength(0); ++i)
            {
                for (int j = 0; j < array.GetLength(1); ++j)
                {
                    result[i + start, j] = array[i, j];
                }
            }
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
