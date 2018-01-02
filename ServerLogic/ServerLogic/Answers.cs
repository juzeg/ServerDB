using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic
{
    class Answers
    {
        public static string Check = "\nYou can check list of missions by command 'List'";
        public static string NotExist = "Mission does not exist or was deleted" + Check;
        public static string Exist = "Mission is already exist" + Check;
        public static string Create_Succesful = "New mission created";
        public static string Succesful = "Whatever have you done - it works";
    }
}
