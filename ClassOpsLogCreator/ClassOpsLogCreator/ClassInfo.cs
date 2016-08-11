using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Excel = Microsoft.Office.Interop.Excel;

/// <summary>
/// 
/// Author: Jhan Perera
/// Department: UIT Client Services
/// 
/// 
/// Description of class: This is a utility class
/// of all the classes in the school. This class contains 
/// information about each class and any special instuctions. 
/// 
/// This class also houses and important zone and building info
///
/// Class Version: 0.1.0.2 - BETA - 7272016
/// 
/// System Version: 0.1.0.0 - BETA - 7152016
/// 
/// </summary>

namespace ClassOpsLogCreator
{
    class ClassInfo
    {
        /// <summary>
        /// Private members
        /// </summary>
        private string[] ClassArray = null;
        private string[] ClassArrayWithNoCrestron = null;
        private string[] CLassArrayWithLapelMic = null;

        /// <summary>
        /// Constructor for this class
        /// This will import the master classlist and give access to other helper methods.
        /// </summary>
        public ClassInfo()
        {
            //Initalize the Absent crestron list
            ClassArray = new string[]
            {
                "ACE001","ACE002","ACE003","ACE004","ACE005","ACE006","ACE007","ACE008","ACE009",
                "ACE010","ACE011","ACE012","ACE013","ACE020","ACE021","ACE025","ACE102","ACE122",
                "ACE147","ACE148","ACE201","ACE203","ACE207","ACE209","ACE231","ACE235","ACE237",
                "ACE241","ACE244","ACE245","ACE249","ACE251","ACE253","ACE311","ACE373","ACW002",
                "ACW003","ACW004","ACW005","ACW006","ACW008","ACW009","ACW102","ACW103","ACW104",
                "ACW106","ACW109","ACW203","ACW204","ACW205","ACW206","ACW209","ACW302","ACW303",
                "ACW304","ACW305","ACW306","ACW307","BSB163","BSB164","BSB203","BSB204","BSB207",
                "BSB102A","BSB328A","BC202","BC214","BC215","BC225","BC228","BC230","BC320","BC322",
                "BC323","BC325","BC203A","CC106","CC108","CC109","CC208","CC211","CC318","CC335","CFA130",
                "CFA312","CB115","CB120","CB121","CB122","CB129","CLH110","CLHA","CLHB",
                "CLHC","CLHD","CLHE","CLHF","CLHG","CLHH","CLHI","CLHJ","CLHK","CLHL","CLHM","FC022",
                "FC042","FC044","FC046","FC048","FC049","FC103","FC104","FC105","FC106","FC108","FC109",
                "FC110","FC112","FC113","FC114","FC117","FC118","FC119","FC152","FC201","FC202","FC203",
                "FC303","FC019B","FC019C","FC030C","FC030D","FC047A","FC047B","HNE001","HNE030","HNE031",
                "HNE032","HNE033","HNE034","HNE035","HNE036","HNE037","HNE038","HNE102","HNE103","HNE104",
                "HNE138","HNE140","HNE141","HNE142","HNE143","HNE206","HNE207","HNE230","HNE248","HNE249",
                "HNE253","HNE258","HNE281","HNE303","HNE304","HNE401","HNE402","HNEB015","HNEB017","HNEB02",
                "HNEB10","HNEB11","KT204","KT510","KT519","KT524","KT626","KT749","KT764","KT830","KT857",
                "KT901","KT902","KT921","KT956","KT1001","KT1048","KK145","KK214","KK224","LASA","LASB",
                "LASC","LSB101","LSB103","LSB105","LSB106","LSB107","MC101","MC103","MC109","MC110","MC111",
                "MC112","MC113","MC114","MC211","MC212","MC213","MC214","MC215","MC216","MC050A","MC050B",
                "MC050C","MC157A","MC157B","MC157C","IKB1001","IKB1002","IKB1003","IKB1004","IKB1005",
                "IKB1006","IKB1008","IKB1014","IKB2001","IKB2002","IKB2003","IKB2004","IKB2008","IKB2009","IKB2010","IKB2011","IKB2027","IKB2028","IKB4034",
                "IKB2026C","PSE317","PSE321","RN102","RN119","RN120","RN143","RN145","RN146","RN201","RN203",
                "RN306","RN812","RN814","RN836","RN836A","RS101","RS101A","RS102","RS103","RS104","RS105",
                "RS122","RS123","RS125","RS127","RS128","RS129","RS130","RS133","RS136","RS137","RS156",
                "RS174","RS201","RS202","RS203","RS205","RS421","RS501","RS536","RS537","RS801","RS802",
                "RS803","RS822","RS840","RS841","SSBE111","SSBE112","SSBE115","SSBE118","SSBN105","SSBN106",
                "SSBN107","SSBN108","SSBN109","SSBN201","SSBN300A","SSBS123","SSBS124","SSBS125","SSBS126",
                "SSBS127","SSBS128","SSBS129","SSBS235","SSBS236","SSBS335","SSBW132","SSBW133","SSBW136",
                "SSBW141","SSBW253","SSBW254","SSBW255","SSBW256","SSBW257","SSBW356","SSBW357","SLH107",
                "SLHA","SLHB","SLHC","SLHD","SLHE","SLHF","SC114","SC116","SC203","SC205","SC211","SC212",
                "SC214","SC216","SC218","SC219","SC220","SC221","SC222","SC223","SC224","SC302","SC303",
                "SC304","TEL0001","TEL0004","TEL0005","TEL0006","TEL0007","TEL0009","TEL0010","TEL0011",
                "TEL0013","TEL0014","TEL0015","TEL0016","TEL1004","TEL1005","TEL1015","TEL1016","TEL2003",
                "TEL2027","TEL2032","TEL2114","TEL2116","TEL2118","TEL3001","TEL3069","TEL3072","TEL4023",
                "TEL4028","TEL4031","TEL4034","VC102","VC103","VC104","VC105","VC106","VC107","VC108","VC114",
                "VC115","VC116","VC117","VC118","VC119","VC135","VC107A","VH1005","VH1016","VH1018","VH1020",
                "VH1022","VH1152","VH1152A","VH1154","VH1156","VH1158","VH2000","VH2005","VH2009","VH2016",
                "VH3000","VH3003","VH3004","VH3005","VH3006","VH3009","VH3017","VHA","VHB","VHC","VHD","WC012",
                "WC117","WC118","WC283A","WC283B","YL232","YL234","YL305","YL390","YL246B","YL280A","YL280N"
            };

            ClassArrayWithNoCrestron = new string[]
            {
                "ACE147", "ACE148", "ACE201", "ACE203", "ACE207", "ACE209",
                "ACE249", "ACE251", "ACE253", "ACE311", "ACE373", "BSB163",
                "BSB102A", "BC320", "FC022", "FC042", "FC042","FC044","FC046",
                "FC048","FC049","FC103","FC104","FC105","FC106","FC108","FC109",
                "FC110","FC112","FC113","FC114","FC117","FC118","FC119","FC152",
                "FC303","FC019B","FC019C","FC030C","FC030D","FC047A","FC047B", "HNE001",
                "HNE102","HNE103","HNE104","HNE138","HNE143","HNE206","HNE207","HNE230",
                "HNEB015","HNEB017","HNEB10","HNEB11","KT204","KT510","KT626","KT749",
                "KT764","KT830","KT857","KT901","KT902","KT921","KT956","KT1001","KT1048",
                "IKB2028","IKB4034","IKB2026C","RN145","RN146","RN203","RN306","RS101","RS101A",
                "RS102","RS103","RS104","RS105","RS122","RS123","RS125","RS127","RS128","RS129",
                "RS130","RS133","RS136","RS156","RS174","RS202","RS203","RS421","RS501","RS536",
                "RS537","RS822","RS840","RS841","VC102","VC103","VC104","VC105","VC106","VC108",
                "VC114","VC115","VC116","VC117","VC118","VC119"
            };

            CLassArrayWithLapelMic = new string[]
            {
                "ACE001","ACE002", "ACE003", "ACE004", "ACE005", "ACE007", "ACE009", "ACE011",
                "ACE013", "ACE102", "ACE235", "ACE244", "ACW102", "ACW103", "ACW004", "ACW005",
                "ACW006", "ACW106", "ACW109", "ACW205", "ACW206", "CFA312", "CB115", "C121",
                "CLH110", "CLHA", "CLHB", "CLHC", "CLHD", "CLHE", "CLHF", "CLHG", "CLHH", "CLHI",
                "CLHJ", "CLHK", "CLHL", "CLHM", "FC203", "HNE030", "HNE031", "HNE032", "HNE033",
                "HNE034", "HNE035", "HNE037", "HNE038", "HNE140", "HNE304", "HNE401", "HNEB02",
                "KT519", "KT524", "LASA", "LASB", "LASC", "LSB101", "LSB103", "LSB105", "LSB106",
                "LSB107","MC157A", "MC157B", "MC157C", "IKB1001", "IKB1002",
                "IKB1003", "IKB1005", "IKB1006", "IKB2001", "IKB2002", "IKB2003", "IKB2010",
                "IKB2027", "RS137", "RS201", "RS205", "RS802", "SSBE111", "SSBE112", "SSBE115",
                "SSBE118", "SSBN105", "SSBN106", "SSBN107", "SSBN108", "SSBN109", "SSBS124",
                "SSBS235", "SSBS335", "SSBW132", "SSBW133", "SSBW136", "SSBW141", "SLHA",
                "SLHB", "SLHC", "SLHD", "SLHE", "SLHF", "SC302", "SC303", "TEL0001", "TEL0005",
                "TEL0006", "TEL0007", "TEL0010", "TEL0014", "TEL0016", "TEL1004", "TEL1005",
                "VC135", "VH3006", "VH3009", "VH1152A", "VHA", "VHB", "VHC", "VHD"
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
            //replace any of the sapces in the middle
            classroom = Regex.Replace(classroom, @"\s+", "");

            //find the class with a lapel mic
            return (this.CLassArrayWithLapelMic.Contains(classroom)) && (this.ClassArray.Contains(classroom));
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
                "CLH", "FRQ", "LAS", "LUM", "CC", "LSB", "BC", "PSE", "SC", "BSB", "SLH", "VH",
                "FC", "MC", "VC", "WC", "CB"
            };

            string[] zone2 = new string[] //South East
            {
                "CSQ", "SLH", "R", "ACW", "CFT", "CFA", "TEL", "STC", "ATK", "BCS", "SSB", "ACE", "YL",
                "KT", "HNE", "OSG", "ELC"
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
            string[] zone1 = new string[] // North
            {
                "MC", "WC", "VC", "FC", "LUM", "LSB", "CC", "BC", "CB", "PSE",
                "SC", "LAS", "FRQ", "SLH"
            };

            string[] zone2 = new string[] //Central
            {
                "CLH", "BSB", "STC", "BRG", "SCL", "STC", "CSQ", "R", "VH",
                "ACW", "HNE", "OSG"
            };

            string[] zone3 = new string[] //South
            {
                "CFT", "ACE", "SSB", "CFA", "TEL", "ELC", "BGS", "ATK", "YL",
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
            string[] zone1 = new string[] //North
            {
               "CLH", "FRQ", "LAS", "LUM", "SLH", "BSB", "FC", "MC", "VC", "WC","CC", "CB", "LSB", "BC", "PSE", "SC"
            };

            string[] zone2 = new string[] // Central
            {
                "CSQ", "R", "SCL", "ACW", "HNE", "OSG", "VH"
            };

            string[] zone3 = new string[] //South Central
            {
                "CFT", "CFA", "TEL", "STC", "ATK"
            };

            string[] zone4 = new string[] //South East
            {
               "BCS", "SSB", "ACE", "YL", "KT", "ELC"
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
               "CLH", "FRQ", "LAS", "LUM", "SLH", "BSB", "FC", "MC", "VC", "WC"
            };

            string[] zone2 = new string[] //North West
            {
                "CC", "CB", "LSB", "BC", "PSE", "SC"
            };

            string[] zone3 = new string[] //South West
            {
                "CSQ", "R", "SCL", "ACW", "HNE", "OSG", "VH", "BRG"
            };

            string[] zone4 = new string[] //South central
            {
               "CFT", "CFA", "TEL", "STC", "ATK"
            };

            string[] zone5 = new string[] //South East
            {
                "BCS", "SSB", "ACE", "YL", "KT", "ELC"
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
               "CLH", "FRQ", "LAS", "LUM", "SLH", "BSB"
            };

            string[] zone2 = new string[]
            {
                "CC", "CB", "LSB", "BC", "PSE", "SC"
            };

            string[] zone3 = new string[]
            {
                "CSQ", "R", "SCL", "ACW", "HNE", "OSG", "VH"
            };

            string[] zone4 = new string[]
            {
               "FC", "MC", "VC", "WC"
            };

            string[] zone5 = new string[]
            {
                "CFT", "CFA", "TEL", "STC", "ATK"
            };

            string[] zone6 = new string[]
            {
                "BCS", "SSB", "ACE", "YL", "KT", "ELC"
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
               "CLH", "FRQ", "LAS", "LUM"
            };

            string[] zone2 = new string[]
            {
                "CC", "CB", "LSB", "BC", "PSE", "SC"
            };

            string[] zone3 = new string[]
            {
                "BSB", "SLH", "VH"
            };

            string[] zone4 = new string[]
            {
               "CSQ", "R", "SCL", "ACW", "HNE", "OSG"
            };

            string[] zone5 = new string[]
            {
                "FC", "MC", "VC", "WC"
            };

            string[] zone6 = new string[]
            {
                "CFT", "CFA", "TEL", "STC", "ATK"
            };

            string[] zone7 = new string[]
            {
                "BCS", "SSB", "ACE", "YL", "KT", "ELC"
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
                "CLH", "FRQ", "LAS", "LUM", "WSC"
             };

            string[] zone2 = new string[]
            {
                "CC", "CB", "LSB", "BC", "PSE", "SHR", "TM", "SC"
            };

            string[] zone3 = new string[]
            {
                "BSB", "SLH", "VH"
            };

            string[] zone4 = new string[]
            {
               "CSQ", "R", "SCL", "ACW"
            };

            string[] zone5 = new string[]
            {
                "FC", "MC", "VC", "WC"
            };

            string[] zone6 = new string[]
            {
                "CFT", "CFA", "TEL", "STC", "ATK"
            };

            string[] zone7 = new string[]
            {
                "BCS", "SSB", "ACE", "YL", "KT", "ELC"
            };
            string[] zone8 = new string[]
            {
                "HNE", "OSG"
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
        /// Get the boarder Buildings for the zoneing of 2 areas
        /// </summary>
        /// <returns></returns>
        public List<string> boarderBuildingZone_2()
        {
            string[] zone1Boarder = new string[]
            {
                "CSQ", "R", "STC", "YL", "KT", "SLH", "BSB", "CLH", "SCL"
            };
            List<string> zone1BoarderList = new List<string>();
            zone1BoarderList.AddRange(zone1Boarder);
            return zone1BoarderList;
        }

        /// <summary>
        /// Get the boarder Buildings for the zoneing of 3 areas
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public List<string> boarderBuildingZone_3(int i)
        {

            List<string> zone1BoarderList = new List<string>();

            if (i == 1)
            {
                string[] zone1Boarder = new string[]//North and central
                {
                    "WC", "VC", "SLH", "FRQ", "LAS", "PSE", "CLH", "BSB", "STC", "YL", "KT"
                };
                zone1BoarderList.AddRange(zone1Boarder);
            }
            else if (i == 2)
            {
                string[] zone2Boarder = new string[] //Central and South
                {
                    "YL", "KT", "CFT", "TEL", "CFA", "ACW", "VH", "ATK", "R", "OSG"
                };
                zone1BoarderList.AddRange(zone2Boarder);
            }
            else
            {
                string[] zone3Boarder = new string[] //North and South
                 {
                     "VC", "WC", "YL", "KT"
                 };
                zone1BoarderList.AddRange(zone3Boarder);
            }
            return zone1BoarderList;
        }

        /// <summary>
        /// Get the boarder Buildings for the zoneing of 4 areas
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public List<string> boarderBuildingZone_4(int i)
        {

            List<string> zone1BoarderList = new List<string>();

            if (i == 1)
            {
                string[] zone1Boarder = new string[]//North and central
                {
                    "SCL", "CLH", "BSB", "VH", "R", "CSQ"
                };
                zone1BoarderList.AddRange(zone1Boarder);
            }
            else if (i == 2)
            {
                string[] zone2Boarder = new string[] //Central and South Central
                {
                    "VH", "ACW", "ATK", "CFA", "TEL", "CFT",
                };
                zone1BoarderList.AddRange(zone2Boarder);
            }
            else if (i == 2)
            {
                string[] zone3Boarder = new string[] //Sount Central and South East
                {
                    "KT", "YL", "SSB", "ELC", "ACE", "TEL", "CFT"
                };
                zone1BoarderList.AddRange(zone3Boarder);
            }
            else
            {
                string[] zone4Boarder = new string[] //North and South East
                 {
                     "VC", "WC", "YL", "KT"
                 };
                zone1BoarderList.AddRange(zone4Boarder);
            }
            return zone1BoarderList;
        }

        /// <summary>
        /// Get the boarder Buildings for the zoneing of 5 areas
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
                    "LSB", "PSE", "LAS", "LUM", "CLH"
                };
                zone1BoarderList.AddRange(zone1Boarder);
            }
            else if (i == 2)
            {
                string[] zone2Boarder = new string[] //North West and South West
                {
                    "SC", "BRG"
                };
                zone1BoarderList.AddRange(zone2Boarder);
            }
            else if (i == 2)
            {
                string[] zone3Boarder = new string[] //South West and South central
                {
                    "ACW", "VH", "ATK", "CFA", "CFT", "TEL"
                };
                zone1BoarderList.AddRange(zone3Boarder);
            }
            else if (i == 4)
            {
                string[] zone4Boarder = new string[] //South Central and South East
                 {
                     "YL", "KT", "SSB", "ELC", "CFT", "ACE", "TEL"
                 };
                zone1BoarderList.AddRange(zone4Boarder);
            }
            else
            {
                string[] zone5Boarder = new string[] //North East (Central) and South West
                 {
                     "SLH", "CSQ", "BSB", "R", "VH", "CLH"
                 };
                zone1BoarderList.AddRange(zone5Boarder);
            }
            return zone1BoarderList;
        }
    }
}
