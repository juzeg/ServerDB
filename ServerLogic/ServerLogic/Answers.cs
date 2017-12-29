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
    }
}
