using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassOpsLogCreator
{
    class ClassInfo
    {
        private string[] ClassListWithNoCrestron = null;
        private string[] ClassAcronym = null;

        /// <summary>
        /// Constructor for this class
        /// This will import the master classlist and give access to other helper methods.
        /// </summary>
        public ClassInfo()
        {
            //Initalize the Absent crestron list
            ClassListWithNoCrestron = new string[]
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
                "OSG2028","OSG4034","OSG2026C","RN145","RN146","RN203","RN306","RS101","RS101A",
                "RS102","RS103","RS104","RS105","RS122","RS123","RS125","RS127","RS128","RS129",
                "RS130","RS133","RS136","RS156","RS174","RS202","RS203","RS421","RS501","RS536",
                "RS537","RS822","RS840","RS841","VC102","VC103","VC104","VC105","VC106","VC108",
                "VC114","VC115","VC116","VC117","VC118","VC119"
            };

            ClassAcronym = new string[]
            {
                "ACE","ACW", "BSB","BC","CC","CFA","CFA","CB","CLH","FC","HNE","KT","LAS","LSB",
                "MC","OSG","PSE","R","SSB","SLH","SC","TEL","VC","VH","WC","YL"
            };
        }

        //Return the list of crestron absent classrooms
        public string[] CrestronAbsentClassList()
        {
            return this.ClassListWithNoCrestron;
        }

        //Return the list of crestron absent classrooms
        public string[] ClassAcronymList()
        {
            return this.ClassAcronym;
        }


        //Is this class room in the absent list?
        public Boolean hasCrestron(string classroom)
        {
            //trim the string
            classroom = classroom.Trim();
            //replace any of the spaces in the middle
            classroom = classroom.Replace(" ", "");

            bool isClass = false;
            foreach(string s in ClassAcronym)
            {
                if (classroom.Contains(s))
                {
                    isClass = true;
                }
            }

            //find the class. 
            return !(this.ClassListWithNoCrestron.Contains(classroom)) && (isClass);
            

        }
    }
}
