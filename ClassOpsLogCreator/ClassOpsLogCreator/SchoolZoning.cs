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
        private ClassInfo classinfo;
        private int numberOfBuildings;
        private int numberOfConnections;
        private int[] sumWeight;

        /// <summary>
        /// This will create a the map for the school and 
        /// provide features for school map as well
        /// </summary>
        public SchoolZoning()
        {
            //Create the graph
            schoolGraph = new Graph();
            classinfo = new ClassInfo();
            
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
            TaskRanks tr = new TaskRanks();

            //If we have 2 shifts
            if (shiftNumber == 2)
            {
                List<string> zone1 = classinfo.getZone_2(1);
                List<string> zone2 = classinfo.getZone_2(2);
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
                //Calculate sum of th weights
                sumWeight = new int[2];
                sumWeight[0] = tr.getTotalTaskValue(zone1Array);
                sumWeight[1] = tr.getTotalTaskValue(zone2Array);

                //Merge the arrays together
                AddToArray(result, zone1Array);
                AddToArray(result, zone2Array, zone1Array.GetLength(0));
            }

            //If we have 3 Shifts
            else if (shiftNumber == 3)
            {
                List<string> zone1 = classinfo.getZone_3(1);
                List<string> zone2 = classinfo.getZone_3(2);
                List<string> zone3 = classinfo.getZone_3(3);
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
                List<string> zone1 = classinfo.getZone_4(1);
                List<string> zone2 = classinfo.getZone_4(2);
                List<string> zone3 = classinfo.getZone_4(3);
                List<string> zone4 = classinfo.getZone_4(4);
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
                List<string> zone1 = classinfo.getZone_5(1);
                List<string> zone2 = classinfo.getZone_5(2);
                List<string> zone3 = classinfo.getZone_5(3);
                List<string> zone4 = classinfo.getZone_5(4);
                List<string> zone5 = classinfo.getZone_5(5);
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
                List<string> zone1 = classinfo.getZone_6(1);
                List<string> zone2 = classinfo.getZone_6(2);
                List<string> zone3 = classinfo.getZone_6(3);
                List<string> zone4 = classinfo.getZone_6(4);
                List<string> zone5 = classinfo.getZone_6(5);
                List<string> zone6 = classinfo.getZone_6(6);
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
                List<string> zone1 = classinfo.getZone_7(1);
                List<string> zone2 = classinfo.getZone_7(2);
                List<string> zone3 = classinfo.getZone_7(3);
                List<string> zone4 = classinfo.getZone_7(4);
                List<string> zone5 = classinfo.getZone_7(5);
                List<string> zone6 = classinfo.getZone_7(6);
                List<string> zone7 = classinfo.getZone_7(7);
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
                List<string> zone1 = classinfo.getZone_8(1);
                List<string> zone2 = classinfo.getZone_8(2);
                List<string> zone3 = classinfo.getZone_8(3);
                List<string> zone4 = classinfo.getZone_8(4);
                List<string> zone5 = classinfo.getZone_8(5);
                List<string> zone6 = classinfo.getZone_8(6);
                List<string> zone7 = classinfo.getZone_8(7);
                List<string> zone8 = classinfo.getZone_8(8);
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

        public int[] getWeighted()
        {
            return this.sumWeight;
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
