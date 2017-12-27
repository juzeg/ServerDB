using Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic
{
    class Logic
    {
        DB dB = null;
        public static List<string> Commands = new List<string>();

        //global variables using in method Devide()
        private string table_name = "";
        private List<string> arguments = new List<string>();

        public Logic(DB tmp)
        {
            dB = tmp;
            //Create new table, arguments: table name and array of names of columns
            Commands.Add("Create");
            //Send new data to server, arguments: table name and array of data
            Commands.Add("SendUpdate");
            //Send new data to client, arguments: table name and lastest value of time
            Commands.Add("ReciveUpdate");
            //Send lastest image of mission: arguments: table name(+_img)
            Commands.Add("GetLastImage");
            //Check actuall, arguments: table name and lastest values of time
            Commands.Add("CheckTopicality");
            //Send lastest set of data to client, arguments: table name
            Commands.Add("GetLastest");
            //Send one category of data to client, arguments: table name, time, name of column
            Commands.Add("GetOne");
        }

        //Check command then try to do
        public string Do(string main)
        {
            string[] tmp = main.Split(' ');

            //Cut command off arguments
            string command = null;
            List<string> args = new List<string>();

            for (int i = 0; i<tmp.Length; i++)
            {
                if (i == 0) command = tmp[i];
                else args.Add(tmp[i]);
            }

            //In case of first word of command try to do or return error
                if (command == "Create")
                {
                if (!Create(args)) return "Something went wrong";
                else return "Create";
                }
                else if (command == "SendUpdate")
                {
                if (!SendUpdate(args)) return "Something went wrong";
                else return "SendUpdate";
                }
                else if (command == "ReciveUpdate")
                {
                return "ReciveUpdate";
                }
                else if (command == "GetLastImage")
                {
                return "GetLastImage";
                }
                else if (command == "CheckTopicality")
                {
                return "CheckTopicality";
                }
                else if (command == "GetLastest")
                {
                return "GetLastest";
                }
                else if (command == "GetOne")
                {
                return "GetOne";
                }
                else return "Command does not exsist";
        }

        //Cut table name off rest of arguments
        private void Divide(List<string> args)
        {
            for (int i = 0; i < args.Count; i++)
            {
                if (i == 0) table_name = args[i];
                else arguments.Add(args[i]);
            }
        }

        //Create new table, arguments: table name and array of names of columns
        private bool Create(List<string> args)
        {
            System.Console.WriteLine("x");
            Divide(args);
            try
            {
                string sql = "CREATE TABLE " + table_name + " (time int";
                for (int i = 0; i < arguments.Count; i++)
                {
                    sql += ", " + arguments[i] + " real";
                }
                sql += ")";

                //Create table with images
                string sql_img = "CREATE TABLE " + table_name + "_img (time int, image string)";
                dB.Query(sql);
                dB.Query(sql_img);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Send new data to server, arguments: table name and array of data
        private bool SendUpdate(List<string> args)
        {
            Divide(args);
            int time;
            if (!Int32.TryParse(arguments[1].ToString(), out time))
            {
                return false;
            }
            string sql = "INSERT INTO " + table_name + " VALUES (";
            for (int i = 0; i < arguments.Count; i++)
            {
                sql += arguments[i];
                if (i + 1 != arguments.Count) sql += ", ";
            }
            sql += ")";
            dB.Query(sql);
            return true;
        }
    }
}
