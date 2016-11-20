using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ClassOpsLogCreator
{
    /// <summary>
    /// This class is used to zone each of logs in a meaningful way
    /// This class also attempt to balance the logs to have a fair
    /// distribution of work across the workers. 
    /// </summary>
    public class SchoolZoning
    {
        private ClassInfo classinfo;
        private int[] numberOfElementsPerZone;
        private List<string> taskList;

        /// <summary>
        /// This will create a the map for the school and 
        /// provide features for school map as well
        /// </summary>
        public SchoolZoning(List<string> buildingNames)
        {

            classinfo = new ClassInfo(buildingNames);

            taskList = new List<string>();
            string[] taskArray = { "AV Shutdown", "Crestron Logout", "Lockup", "Proactive Classroom Check",
                       "SCLD Student Event", "SCLD Student Logout", "Operator", "Pickup Large PA", "Pickup Mic",
                "Pickup PC", "Pickup Projector", "Pickup Skype Kit","Pickup Small PA" };
            taskList.AddRange(taskArray);
        }

        /// <summary>
        /// Return the number of rows
        /// </summary>
        /// <returns></returns>
        public int[] numberOfRows()
        {
            return this.numberOfElementsPerZone;
        }

        /// <summary>
        /// This method is responsible for the zoning of all the elements in the range.
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
                }
                //Remove all empty rows
                zone1Array = ZoneSuperLogImporter.RemoveEmptyRows(zone1Array);
                zone2Array = ZoneSuperLogImporter.RemoveEmptyRows(zone2Array);
                result = new string[zone1Array.GetLength(0) + zone2Array.GetLength(0), 7];

                //AT THIS POINT IS WHERE WE DO THE "SMART" zoning
                this.applyRankAndOrganize(ref zone1Array, ref zone2Array, classinfo.boarderBuildingZone_2(), 5);

                numberOfElementsPerZone = new int[2];
                numberOfElementsPerZone[0] = zone1Array.GetUpperBound(0);
                numberOfElementsPerZone[1] = zone2Array.GetUpperBound(0);

                //Merge the arrays together
                AddToArray(result, zone1Array);
                AddToArray(result, zone2Array, zone1Array.GetLength(0));

            }

            //If we have 3 Shifts
            else if (shiftNumber == 3)
            {
                List<string> zone1 = classinfo.getZone_3(1); //North Central
                List<string> zone2 = classinfo.getZone_3(2); //North East
                List<string> zone3 = classinfo.getZone_3(3); //South
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

                //AT THIS POINT IS WHERE WE DO THE "SMART" zoning
                this.applyRankAndOrganize(ref zone1Array, ref zone2Array, classinfo.boarderBuildingZone_3(1), 2);//North Center and north east
                this.applyRankAndOrganize(ref zone2Array, ref zone3Array, classinfo.boarderBuildingZone_3(2), 4);//North East and South
                this.applyRankAndOrganize(ref zone3Array, ref zone1Array, classinfo.boarderBuildingZone_3(3), 2);//North Central and south

                numberOfElementsPerZone = new int[3];
                numberOfElementsPerZone[0] = zone1Array.GetUpperBound(0);
                numberOfElementsPerZone[1] = zone2Array.GetUpperBound(0);
                numberOfElementsPerZone[2] = zone3Array.GetUpperBound(0);

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
                zone1Array = ZoneSuperLogImporter.RemoveEmptyRows(zone1Array); //North West
                zone2Array = ZoneSuperLogImporter.RemoveEmptyRows(zone2Array); //Central
                zone3Array = ZoneSuperLogImporter.RemoveEmptyRows(zone3Array); //South
                zone4Array = ZoneSuperLogImporter.RemoveEmptyRows(zone4Array); //North East
                result = new string[zone1Array.GetLength(0) + zone2Array.GetLength(0) + zone3Array.GetLength(0) + zone4Array.GetLength(0), 7];

                //AT THIS POINT IS WHERE WE DO THE "SMART" zoning

                this.applyRankAndOrganize(ref zone1Array, ref zone2Array, classinfo.boarderBuildingZone_4(1), 1);//North West and Central
                this.applyRankAndOrganize(ref zone2Array, ref zone3Array, classinfo.boarderBuildingZone_4(2), 3);//Central and South
                this.applyRankAndOrganize(ref zone3Array, ref zone4Array, classinfo.boarderBuildingZone_4(3), 3);//South and North East
                this.applyRankAndOrganize(ref zone4Array, ref zone1Array, classinfo.boarderBuildingZone_4(4), 1);//North West and North East

                numberOfElementsPerZone = new int[4];
                numberOfElementsPerZone[0] = zone1Array.GetUpperBound(0);
                numberOfElementsPerZone[1] = zone2Array.GetUpperBound(0);
                numberOfElementsPerZone[2] = zone3Array.GetUpperBound(0);
                numberOfElementsPerZone[3] = zone4Array.GetUpperBound(0);

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

                //AT THIS POINT IS WHERE WE DO THE "SMART" zoning
                this.applyRankAndOrganize(ref zone1Array, ref zone2Array, classinfo.boarderBuildingZone_5(1), 2);//North east and North West
                this.applyRankAndOrganize(ref zone2Array, ref zone3Array, classinfo.boarderBuildingZone_5(2), 2);//North West and South West
                this.applyRankAndOrganize(ref zone3Array, ref zone4Array, classinfo.boarderBuildingZone_5(3), 2);//South West and South central
                this.applyRankAndOrganize(ref zone4Array, ref zone5Array, classinfo.boarderBuildingZone_5(4), 2);//South Central and South East
                this.applyRankAndOrganize(ref zone3Array, ref zone1Array, classinfo.boarderBuildingZone_5(5), 2);//North East (Central) and South West

                numberOfElementsPerZone = new int[5];
                numberOfElementsPerZone[0] = zone1Array.GetUpperBound(0);
                numberOfElementsPerZone[1] = zone2Array.GetUpperBound(0);
                numberOfElementsPerZone[2] = zone3Array.GetUpperBound(0);
                numberOfElementsPerZone[3] = zone4Array.GetUpperBound(0);
                numberOfElementsPerZone[4] = zone5Array.GetUpperBound(0);

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

                numberOfElementsPerZone = new int[6];
                numberOfElementsPerZone[0] = zone1Array.GetUpperBound(0);
                numberOfElementsPerZone[1] = zone2Array.GetUpperBound(0);
                numberOfElementsPerZone[2] = zone3Array.GetUpperBound(0);
                numberOfElementsPerZone[3] = zone4Array.GetUpperBound(0);
                numberOfElementsPerZone[4] = zone5Array.GetUpperBound(0);
                numberOfElementsPerZone[5] = zone6Array.GetUpperBound(0);

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

                numberOfElementsPerZone = new int[7];
                numberOfElementsPerZone[0] = zone1Array.GetUpperBound(0);
                numberOfElementsPerZone[1] = zone2Array.GetUpperBound(0);
                numberOfElementsPerZone[2] = zone3Array.GetUpperBound(0);
                numberOfElementsPerZone[3] = zone4Array.GetUpperBound(0);
                numberOfElementsPerZone[4] = zone5Array.GetUpperBound(0);
                numberOfElementsPerZone[5] = zone6Array.GetUpperBound(0);
                numberOfElementsPerZone[6] = zone7Array.GetUpperBound(0);

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

                numberOfElementsPerZone = new int[8];
                numberOfElementsPerZone[0] = zone1Array.GetUpperBound(0);
                numberOfElementsPerZone[1] = zone2Array.GetUpperBound(0);
                numberOfElementsPerZone[2] = zone3Array.GetUpperBound(0);
                numberOfElementsPerZone[3] = zone4Array.GetUpperBound(0);
                numberOfElementsPerZone[4] = zone5Array.GetUpperBound(0);
                numberOfElementsPerZone[5] = zone6Array.GetUpperBound(0);
                numberOfElementsPerZone[6] = zone7Array.GetUpperBound(0);
                numberOfElementsPerZone[7] = zone8Array.GetUpperBound(0);

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
        /// A Helper method to quickly convert an array object to a string array
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
        /// This method combines two rectangle arrays together.
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
        /// This method will score each zone and make a comparison
        /// If the comparison is true, then we move items around in zones and 
        /// return the new zones with balanced ranks
        /// </summary>
        /// <param name="zone1"></param>
        /// <param name="zone2"></param>
        /// <param name="borderBuildings"></param>
        private void applyRankAndOrganize(ref string[,] zone1, ref string[,] zone2, List<string> borderBuildings, int discrepancy)
        {
            //Convert each zone to a list
            List<string[]> zone1List = convertToList(zone1);
            List<string[]> zone2List = convertToList(zone2);
            TaskRanks tr = new TaskRanks();
            int zone1Rank = tr.getTotalTaskValue(zone1);
            int zone2Rank = tr.getTotalTaskValue(zone2);

            //If the difference of the two zones ranks in 6
            if (Math.Abs(zone1Rank - zone2Rank) > discrepancy)
            {
                if (zone1Rank > zone2Rank)
                {
                    //move items from zone1 to zone2
                    for (int i = 0; i < zone1List.Count && Math.Abs(zone1Rank - zone2Rank) > discrepancy; i++)
                    {
                        if (borderBuildings.Contains(zone1List[i][4]) && taskList.Contains(zone1List[i][1]))
                        {
                            if(i + 1 < zone1List.Count && zone1List[i][4] != zone1List[i + 1][4] && zone1List[i][5] != zone1List[i + 1][5])
                            {
                                var temp = zone1List[i];
                                zone1List.Remove(zone1List[i]);
                                zone2List.Add(temp);
                                i--;
                                zone2 = CreateRectangularArray<string>(zone2List);
                                zone2Rank = tr.getTotalTaskValue(zone2);
                                zone1 = CreateRectangularArray<string>(zone1List);
                                zone1Rank = tr.getTotalTaskValue(zone1);
                            }           
                        }
                    }
                }
                else if (zone1Rank < zone2Rank)
                {
                    //move items from zone2 to zone1
                    for (int i = 0; i < zone2List.Count && Math.Abs(zone1Rank - zone2Rank) > discrepancy; i++)
                    {
                        if (borderBuildings.Contains(zone2List[i][4]) && taskList.Contains(zone2List[i][1]))
                        {
                            if(i + 1 < zone2List.Count && zone2List[i][4] != zone2List[i + 1][4] && zone2List[i][5] != zone2List[i + 1][5])
                            {
                                var temp = zone2List[i];
                                zone2List.Remove(zone2List[i]);
                                zone1List.Add(temp);
                                i--;
                                zone1 = CreateRectangularArray<string>(zone1List);
                                zone1Rank = tr.getTotalTaskValue(zone1);
                                zone2 = CreateRectangularArray<string>(zone2List);
                                zone2Rank = tr.getTotalTaskValue(zone2);
                            }                           
                        }
                    }
                }
            }
            //Sort
            zone1List = zone1List.OrderBy(arr => arr[3]).ToList<string[]>();
            zone2List = zone2List.OrderBy(arr => arr[3]).ToList<string[]>();

            //Convert back to 2d arrays
            zone1 = CreateRectangularArray<string>(zone1List);
            zone2 = CreateRectangularArray<string>(zone2List);
        }

        /// <summary>
        /// This method converts a 2d array to a list
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        private static List<string[]> convertToList(string[,] arr)
        {
            string[][] jagged = new string[arr.GetLength(0)][];

            for (int i = 0; i < arr.GetLength(0); i++)
            {
                jagged[i] = new string[arr.GetLength(1)];
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    jagged[i][j] = arr[i, j];
                }
            }
            return jagged.ToList();
        }

        /// <summary>
        /// This method converts a List<string[]> into a 2d array
        /// 
        /// Returns an empty array if arrays.count = 0 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arrays"></param>
        /// <returns></returns>
        private static T[,] CreateRectangularArray<T>(IList<T[]> arrays)
        {
            // TODO: Validation and special-casing for arrays.Count == 0
            if (arrays.Count != 0)
            {
                int minorLength = arrays[0].Length;
                T[,] ret = new T[arrays.Count, minorLength];
                for (int i = 0; i < arrays.Count; i++)
                {
                    var array = arrays[i];
                    if (array.Length != minorLength)
                    {
                        throw new ArgumentException
                            ("All arrays must be the same length");
                    }
                    for (int j = 0; j < minorLength; j++)
                    {
                        ret[i, j] = array[j];
                    }
                }
                return ret;
            }
            T[,] retDefault = new T[arrays.Count, 7];
            return retDefault;
        }
    }
}
