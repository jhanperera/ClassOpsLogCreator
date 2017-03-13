using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Excel = Microsoft.Office.Interop.Excel;

namespace ClassOpsLogCreator
{
    /// <summary>
    /// This class hold the information about class rooms and other
    /// vital information about the school buildings 
    /// </summary>
    class ClassInfo
    {
        /// <summary>
        /// Private members
        /// </summary>
        private string[] ClassArray = null;
        private string[] ClassArrayWithNoCrestron = null;
        private string[] CLassArrayWithLapelMic = null;
        private List<string> BuildingNames = null;
        private string ACE,ACW,ATK,BC,BCS,BRG,BSB,CB,CC,CFA,CFT,CLH,
            CSQ,DB,ELC,FC,FRQ,HNE,K,KT,LAS,LSB,LUM,MC,OSG,PSE,R,SC,SCL,
            SHR,SLH,SSB,STC,TM,VC,VH,WC,WSC,YL;

        /// <summary>
        /// Constructor for this class
        /// This will import the master class list and give access to other helper methods.
        /// </summary>
        public ClassInfo(List<string> buildingNames)
        {
            this.setBuildingList(buildingNames);
            //Initialize the Classes with crestron
            ClassArray = new string[]
            {
                ACE + "001",ACE + "002",ACE + "003",ACE + "004",ACE + "005",ACE + "006",ACE + "007",ACE + "008",ACE + "009",
                ACE + "010",ACE + "011",ACE + "012",ACE + "013",ACE + "020",ACE + "021",ACE + "025",ACE + "102",ACE + "122",
                ACE + "147",ACE + "148",ACE + "201",ACE + "203",ACE + "207",ACE + "209",ACE + "231",ACE + "235",ACE + "237",
                ACE + "241",ACE + "244",ACE + "245",ACE + "249",ACE + "251",ACE + "253",ACE + "311",ACE + "373","ACW002",
                ACW+"003",ACW+"004",ACW+"005",ACW+"006",ACW+"008",ACW+"009",ACW+"102",ACW+"103",ACW+"104",
                ACW+"106",ACW+"109",ACW+"203",ACW+"204",ACW+"205",ACW+"206",ACW+"209",ACW+"302",ACW+"303",
                ACW+"304",ACW+"305",ACW+"306",ACW+"307",BSB+"163",BSB+"164",BSB+"203",BSB+"204",BSB+"207",
                BSB+"102A",BSB+"328A",BC+"202",BC+"214",BC+"215",BC+"225",BC+"228",BC+"230",BC+"320",BC+"322",
                BC+"323",BC+"325",BC+"203A",CC+"106",CC+"108",CC+"109",CC+"208",CC+"211",CC+"318",CC+"335",CFA+"130",
                CFA+"312",CB+"115",CB+"120",CB+"121",CB+"122",CB+"129",CLH+"110",CLH+"A",CLH+"B",
                CLH+"C",CLH+"D",CLH+"E",CLH+"F",CLH+"G",CLH+"H",CLH+"I",CLH+"J",CLH+"K",CLH+"L",CLH+"M",FC+"022",
                FC+"042",FC+"044",FC+"046",FC+"048",FC+"049",FC+"103",FC+"104",FC+"105",FC+"106",FC+"108",FC+"109",
                FC+"110",FC+"112",FC+"113",FC+"114",FC+"117",FC+"118",FC+"119",FC+"152",FC+"201",FC+"202",FC+"203",
                FC+"303",FC+"019B",FC+"019C",FC+"030C",FC+"030D",FC+"047A",FC+"047B",HNE+"001",HNE+"030",HNE+"031",
                HNE+"032",HNE+"033",HNE+"034",HNE+"035",HNE+"036",HNE+"037",HNE+"038",HNE+"102",HNE+"103",HNE+"104",
                HNE+"138",HNE+"140",HNE+"141",HNE+"142",HNE+"143",HNE+"206",HNE+"207",HNE+"230",HNE+"248",HNE+"249",
                HNE+"253",HNE+"258",HNE+"281",HNE+"303",HNE+"304",HNE+"401",HNE+"402",HNE+"B015",HNE+"B017",HNE+"B02",
                HNE+"B10",HNE+"B11",KT+"204",KT+"510",KT+"519",KT+"524",KT+"626",KT+"749",KT+"764",KT+"830",KT+"857",
                KT+"901",KT+"902",KT+"921",KT+"956",KT+"1001",KT+"1048",LAS+"A",LAS+"B",
                LAS+"C",LSB+"101",LSB+"103",LSB+"105",LSB+"106",LSB+"107",MC+"101",MC+"103",MC+"109",MC+"110",MC+"111",
                MC+"112",MC+"113",MC+"114",MC+"211",MC+"212",MC+"213",MC+"214",MC+"215",MC+"216",MC+"050A",MC+"050B",
                MC+"050C",MC+"157A",MC+"157B",MC+"157C","IKB1001","IKB1002","IKB1003","IKB1004","IKB1005",
                "IKB1006","IKB1008","IKB1014","IKB2001","IKB2002","IKB2003","IKB2004","IKB2008","IKB2009","IKB2010","IKB2011","IKB2027","IKB2028","IKB4034",
                "IKB2026C",PSE+"317",PSE+"321",R+"N102",R+"N119",R+"N120",R+"N143",R+"N145",R+"N146",R+"N201",R+"N203",
                R+"N306",R+"N812",R+"N814",R+"N836",R+"N836A",R+"S101",R+"S101A",R+"S102",R+"S103",R+"S104",R+"S105",
                R+"S122",R+"S123",R+"S125",R+"S127",R+"S128",R+"S129",R+"S130",R+"S133",R+"S136",R+"S137",R+"S156",
                R+"S174",R+"S201",R+"S202",R+"S203",R+"S205",R+"S421",R+"S501",R+"S536",R+"S537",R+"S801",R+"S802",
                R+"S803",R+"S822",R+"S840",R+"S841",SSB+"E111",SSB+"E112",SSB+"E115",SSB+"E118",SSB+"N105",SSB+"N106",
                SSB+"N107",SSB+"N108",SSB+"N109",SSB+"N201",SSB+"N300A",SSB+"S123",SSB+"S124",SSB+"S125",SSB+"S126",
                SSB+"S127",SSB+"S128",SSB+"S129",SSB+"S235",SSB+"S236",SSB+"S335",SSB+"W132",SSB+"W133",SSB+"W136",
                SSB+"W141",SSB+"W253",SSB+"W254",SSB+"W255",SSB+"W256",SSB+"W257",SSB+"W356",SSB+"W357",SLH+"107",
                SLH+"A",SLH+"B",SLH+"C",SLH+"D",SLH+"E",SLH+"F",SC+"114",SC+"116",SC+"203",SC+"205",SC+"211",SC+"212",
                SC+"214",SC+"216",SC+"218",SC+"219",SC+"220",SC+"221",SC+"222",SC+"223",SC+"224",SC+"302",SC+"303",
                SC+"304",DB+"0001",DB+"0004",DB+"0005",DB+"0006",DB+"0007",DB+"0009",DB+"0010",DB+"0011",
                DB+"0013",DB+"0014",DB+"0015",DB+"0016",DB+"1004",DB+"1005",DB+"1015",DB+"1016",DB+"2003",
                DB+"2027",DB+"2032",DB+"2114",DB+"2116",DB+"2118",DB+"3001",DB+"3069",DB+"3072",DB+"4023",
                DB+"4028",DB+"4031",DB+"4034",VC+"102",VC+"103",VC+"104",VC+"105",VC+"106",VC+"107",VC+"108",VC+"114",
                VC+"115",VC+"116",VC+"117",VC+"118",VC+"119",VC+"135",VC+"107A",VH+"1005",VH+"1016",VH+"1018",VH+"1020",
                VH+"1022",VH+"1152",VH+"1152A",VH+"1154",VH+"1156",VH+"1158",VH+"2000",VH+"2005",VH+"2009",VH+"2016",
                VH+"3000",VH+"3003",VH+"3004",VH+"3005",VH+"3006",VH+"3009",VH+"3017",VH+"A",VH+"B",VH+"C",VH+"D",WC+"012",
                WC+"117",WC+"118",WC+"283A",YL+"232",YL+"234",YL+"305",YL+"390",YL+"246B",YL+"280A",YL+"280N"
            };
            //Init classes without a crestron
            ClassArrayWithNoCrestron = new string[]
            {
                ACE + "147", ACE + "148", ACE + "201", ACE + "203", ACE + "207", ACE + "209",
                ACE + "249", ACE + "251", ACE + "253", ACE + "311", ACE + "373", BSB+"163",
                BSB+"102A", BC+"320", FC+"022", FC+"042", FC+"042",FC+"044",FC+"046",
                FC+"048",FC+"049",FC+"103",FC+"104",FC+"105",FC+"106",FC+"108",FC+"109",
                FC+"110",FC+"112",FC+"113",FC+"114",FC+"117",FC+"118",FC+"119",FC+"152",
                FC+"303",FC+"019B",FC+"019C",FC+"030C",FC+"030D",FC+"047A",FC+"047B", HNE+"001",
                HNE+"102",HNE+"103",HNE+"104",HNE+"138",HNE+"143",HNE+"206",HNE+"207",HNE+"230",
                HNE+"B015",HNE+"B017",HNE+"B10",HNE+"B11",KT+"204",KT+"510",KT+"626",KT+"749",
                KT+"764",KT+"830",KT+"857",KT+"901",KT+"902",KT+"921",KT+"956",KT+"1001",KT+"1048",K+"K145",
                K+"K214",K+"K224","IKB2028","IKB4034","IKB2026C",R+"N145",R+"N146",R+"N203",R+"N306",R+"S101",R+"S101A",
                R+"S102",R+"S103",R+"S104",R+"S105",R+"S122",R+"S123",R+"S125",R+"S127",R+"S128",R+"S129",
                R+"S130",R+"S133",R+"S136",R+"S156",R+"S174",R+"S202",R+"S203",R+"S421",R+"S501",R+"S536",
                R+"S537",R+"S822",R+"S840",R+"S841",VC+"102",VC+"103",VC+"104",VC+"105",VC+"106",VC+"108",
                VC+"114",VC+"115",VC+"116",VC+"117",VC+"118",VC+"119"
            };
            //Classes with mic
            CLassArrayWithLapelMic = new string[]
            {
                ACE + "001",ACE + "002", ACE + "003", ACE + "004", ACE + "005", ACE + "007", ACE + "009", ACE + "011",
                ACE + "013", ACE + "102", ACE + "235", ACE + "244", ACW+"102", ACW+"103", ACW+"004", ACW+"005",
                ACW+"006", ACW+"106", ACW+"109", ACW+"205", ACW+"206", CFA+"312", CB+"115", "C121",
                CLH+"110", CLH+"A", CLH+"B", CLH+"C", CLH+"D", CLH+"E", CLH+"F", CLH+"G", CLH+"H", CLH+"I",
                CLH+"J", CLH+"K", CLH+"L", CLH+"M", FC+"203", HNE+"030", HNE+"031", HNE+"032", HNE+"033",
                HNE+"034", HNE+"035", HNE+"037", HNE+"038", HNE+"140", HNE+"304", HNE+"401", HNE+"B02",
                KT+"519", KT+"524", LAS+"A", LAS+"B", LAS+"C", LSB+"101", LSB+"103", LSB+"105", LSB+"106",
                LSB+"107",MC+"157A", MC+"157B", MC+"157C", "IKB1001", "IKB1002",
                "IKB1003", "IKB1005", "IKB1006", "IKB2001", "IKB2002", "IKB2003", "IKB2010",
                "IKB2027", R+"S137", R+"S201", R+"S205", R+"S802", SSB+"E111", SSB+"E112", SSB+"E115",
                SSB+"E118", SSB+"N105", SSB+"N106", SSB+"N107", SSB+"N108", SSB+"N109", SSB+"S124",
                SSB+"S235", SSB+"S335", SSB+"W132", SSB+"W133", SSB+"W136", SSB+"W141", SLH+"A",
                SLH+"B", SLH+"C", SLH+"D", SLH+"E", SLH+"F", SC+"302", SC+"303", DB+"0001", DB+"0005",
                DB+"0006", DB+"0007", DB+"0010", DB+"0014", DB+"0016", DB+"1004", DB+"1005",
                VC+"135", VH+"3006", VH+"3009", VH+"1152A", VH+"A", VH+"B", VH+"C", VH+"D"
            };
        }

        /// <summary>
        /// Return the list of crestron absent classrooms
        /// </summary>
        /// <returns></returns>
        public string[] CrestronAbsentClassList()
        {
            return this.ClassArrayWithNoCrestron;
        }

        /// <summary>
        /// Is this class room in the absent list?
        /// </summary>
        /// <param name="classroom"></param>
        /// <returns></returns>
        public Boolean hasCrestron(string classroom)
        {
            //trim the string
            classroom = classroom.Trim();
            //replace any of the spaces in the middle
            classroom = Regex.Replace(classroom, @"\s+", "");

            //find the class. 
            return !(this.ClassArrayWithNoCrestron.Contains(classroom)) && (this.ClassArray.Contains(classroom));
        }

        /// <summary>
        /// Does this class have a lapel mic?
        /// </summary>
        /// <param name="classroom"></param>
        /// <returns></returns>
        public Boolean hasLapelMic(string classroom)
        {
            //trim the string
            classroom = classroom.Trim();
            //replace any of the spaces in the middle
            classroom = Regex.Replace(classroom, @"\s+", "");

            //find the class with a lapel mic
            return (this.CLassArrayWithLapelMic.Contains(classroom)) && (this.ClassArray.Contains(classroom));
        }

        /// <summary>
        /// This method must be called first before any zoning is done
        /// </summary>
        /// <param name="buildingList"></param>
        public void setBuildingList(List<string> buildingList)
        {
            this.BuildingNames = buildingList;
            ACE = BuildingNames[0]; ACW = BuildingNames[1]; ATK = BuildingNames[2];
            BC = BuildingNames[3]; BCS = BuildingNames[4]; BRG = BuildingNames[5];
            BSB = BuildingNames[6]; CB = BuildingNames[7]; CC = BuildingNames[8];
            CFA = BuildingNames[9]; CFT = BuildingNames[10]; CLH = BuildingNames[11];
            CSQ = BuildingNames[12]; DB = BuildingNames[13]; ELC = BuildingNames[14];
            FC = BuildingNames[15]; FRQ = BuildingNames[16]; HNE = BuildingNames[17];
            K = BuildingNames[18]; KT = BuildingNames[19]; LAS = BuildingNames[20];
            LSB = BuildingNames[21]; LUM = BuildingNames[22]; MC = BuildingNames[23];
            OSG = BuildingNames[24]; PSE = BuildingNames[25]; R = BuildingNames[26];
            SC = BuildingNames[27]; SCL = BuildingNames[28]; SHR = BuildingNames[29];
            SLH = BuildingNames[30]; SSB = BuildingNames[31]; STC = BuildingNames[32];
            TM = BuildingNames[33]; VC = BuildingNames[34]; VH = BuildingNames[35];
            WC = BuildingNames[36]; WSC = BuildingNames[37]; YL = BuildingNames[38];
        }

        /// <summary>
        /// This will return a List of buildings that are in one of two zones
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public List<string> getZone_2(int i)
        {
            string[] zone1 = new string[] //North West
            {
                CLH, FRQ, LAS, LUM, CC, LSB, BC, PSE, SC, BSB, SLH, VH,
                FC, MC, VC, WC, CB,BRG, K
            };

            string[] zone2 = new string[] //South East
            {
                CSQ, R, ACW, CFT, CFA, DB, STC, ATK, BCS, SSB, ACE, YL,
                KT, HNE, OSG, ELC, SCL
            };

            List<string> buildingList = new List<string>();
            //North Zone
            if (i == 1)
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
            string[] zone1 = new string[] // North Central
            {
                LSB, CB, BC, CC, SC, FRQ, R, CSQ, SCL, BRG,VH, OSG, HNE, CLH
            };

            string[] zone2 = new string[] //North East
            {
               MC, FC, VC, WC, SLH, YL, LUM, KT, BSB, LAS, PSE, K
            };

            string[] zone3 = new string[] //South
            {
                SSB, ELC, BCS, DB, ACE, CFT, ACW, CFA, STC, ATK
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

            string[] zone1 = new string[] //North West
            {
               CLH, LAS, PSE, CB, LSB, BC, CC, SC, FRQ
            };

            string[] zone2 = new string[] // Central
            {
                BSB, R, CSQ, SCL, BRG, HNE, OSG, VH, ATK, ACW
            };

            string[] zone3 = new string[] //South
            {
               SSB, ELC, BCS, DB, ACE, CFT,KT, YL, CFA
            };

            string[] zone4 = new string[] //North East
            {
               MC, FC, VC, WC, SLH, LUM, STC, K
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
            string[] zone1 = new string[] //North East (Central)
            {
               FRQ, LAS, SLH, BSB, FC, MC, VC, WC, K
            };

            string[] zone2 = new string[] //North West
            {
                CC, CB, LSB, BC, PSE, SC, CLH, LUM
            };

            string[] zone3 = new string[] //South West
            {
                CSQ, R, SCL, ACW, HNE, OSG, VH, BRG
            };

            string[] zone4 = new string[] //South central
            {
               CFT, CFA, DB, STC, ATK
            };

            string[] zone5 = new string[] //South East
            {
                BCS, SSB, ACE, YL, KT, ELC
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
               CLH, FRQ, LAS, LUM, SLH, BSB
            };

            string[] zone2 = new string[]
            {
                CC, CB, LSB, BC, PSE, SC
            };

            string[] zone3 = new string[]
            {
                CSQ, R, SCL, ACW, HNE, OSG, VH,BRG
            };

            string[] zone4 = new string[]
            {
               FC, MC, VC, WC, K
            };

            string[] zone5 = new string[]
            {
                CFT, CFA, DB, STC, ATK
            };

            string[] zone6 = new string[]
            {
                BCS, SSB, ACE, YL, KT, ELC
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
               CLH, FRQ, LAS, LUM
            };

            string[] zone2 = new string[]
            {
                CC, CB, LSB, BC, PSE, SC
            };

            string[] zone3 = new string[]
            {
                BSB, SLH, VH, 
            };

            string[] zone4 = new string[]
            {
               CSQ, R, SCL, ACW, HNE, OSG, BRG
            };

            string[] zone5 = new string[]
            {
                FC, MC, VC, WC, K
            };

            string[] zone6 = new string[]
            {
                CFT, CFA, DB, STC, ATK
            };

            string[] zone7 = new string[]
            {
                BCS, SSB, ACE, YL, KT, ELC
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
                CLH, FRQ, LAS, LUM, WSC
             };

            string[] zone2 = new string[]
            {
                CC, CB, LSB, BC, PSE, SHR, TM, SC
            };

            string[] zone3 = new string[]
            {
                BSB, SLH, VH
            };

            string[] zone4 = new string[]
            {
               CSQ, R, SCL, ACW, BRG
            };

            string[] zone5 = new string[]
            {
                FC, MC, VC, WC, K
            };

            string[] zone6 = new string[]
            {
                CFT, CFA, DB, STC, ATK
            };

            string[] zone7 = new string[]
            {
                BCS, SSB, ACE, YL, KT, ELC
            };
            string[] zone8 = new string[]
            {
                HNE, OSG
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
        /// Get the boarder Buildings for the zoning of 2 areas
        /// </summary>
        /// <returns></returns>
        public List<string> boarderBuildingZone_2()
        {
            string[] zone1Boarder = new string[]
            {
                CSQ, R, STC, YL, KT, SLH, BSB, CLH, SCL
            };
            List<string> zone1BoarderList = new List<string>();
            zone1BoarderList.AddRange(zone1Boarder);
            return zone1BoarderList;
        }

        /// <summary>
        /// Get the boarder Buildings for the zoning of 3 areas
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public List<string> boarderBuildingZone_3(int i)
        {
            List<string> zone1BoarderList = new List<string>();

            if (i == 1)
            {
                string[] zone1Boarder = new string[] // North Central and North East
                {
                    LUM, LAS, VH, BSB, VC, SLH, FRQ, R, LSB
                };
                zone1BoarderList.AddRange(zone1Boarder);
            }
            else if (i == 2)
            {
                string[] zone2Boarder = new string[] //North East and South
                {
                    YL, KT , ACE, STC
                };
                zone1BoarderList.AddRange(zone2Boarder);
            }
            else
            {
                string[] zone3Boarder = new string[] //North Central and south
                 {
                     ACW, R, VH, HNE, OSG, ATK, CFT, CFA,
                 };
                zone1BoarderList.AddRange(zone3Boarder);
            }
            return zone1BoarderList;
        }

        /// <summary>
        /// Get the boarder Buildings for the zoning of 4 areas
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public List<string> boarderBuildingZone_4(int i)
        {

            List<string> zone1BoarderList = new List<string>();

            if (i == 1)
            {
                string[] zone1Boarder = new string[]//North West and central
                {
                    CLH, R, CSQ, LAS, FRQ, BSB, HNE, BRG
                };
                zone1BoarderList.AddRange(zone1Boarder);
            }
            else if (i == 2)
            {
                string[] zone2Boarder = new string[] //Central and South
                {
                    ACW, CFA, ATK, DB, VH
                };
                zone1BoarderList.AddRange(zone2Boarder);
            }
            else if (i == 3)
            {
                string[] zone3Boarder = new string[] //South North East
                {
                    YL, KT, STC
                };
                zone1BoarderList.AddRange(zone3Boarder);
            }
            else
            {
                string[] zone4Boarder = new string[] //North East and North West
                 {
                     FC, LUM, SLH, FRQ
                 };
                zone1BoarderList.AddRange(zone4Boarder);
            }
            return zone1BoarderList;
        }

        /// <summary>
        /// Get the boarder Buildings for the zoning of 5 areas
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public List<string> boarderBuildingZone_5(int i)
        {

            List<string> zone1BoarderList = new List<string>();

            if (i == 1)
            {
                string[] zone1Boarder = new string[]//North east and North West
                {
                    LSB, PSE, LAS, LUM, CLH
                };
                zone1BoarderList.AddRange(zone1Boarder);
            }
            else if (i == 2)
            {
                string[] zone2Boarder = new string[] //North West and South West
                {
                    SC, BRG
                };
                zone1BoarderList.AddRange(zone2Boarder);
            }
            else if (i == 3)
            {
                string[] zone3Boarder = new string[] //South West and South central
                {
                    ACW, VH, ATK, CFA, CFT, DB
                };
                zone1BoarderList.AddRange(zone3Boarder);
            }
            else if (i == 4)
            {
                string[] zone4Boarder = new string[] //South Central and South East
                 {
                     YL, KT, SSB, ELC, CFT, ACE, DB
                 };
                zone1BoarderList.AddRange(zone4Boarder);
            }
            else
            {
                string[] zone5Boarder = new string[] //North East (Central) and South West
                 {
                     SLH, CSQ, BSB, R, VH, CLH
                 };
                zone1BoarderList.AddRange(zone5Boarder);
            }
            return zone1BoarderList;
        }
    }
}
