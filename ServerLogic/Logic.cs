using System;
using System.Collections.Generic;
using Login;

namespace Server
{
    internal class Logic
    {
        public static List<string> Commands = new List<string>();

        //global variable using to send answer to client
        public string answer = "";
        private Answers Answers;
        private List<string> arguments;
        private string column_name;
        private readonly DataBase dB;
        private string IMG;

        //global variables using in method Devide(), GetTime() or GetIMG()
        private string table_name = "";
        private uint time;

        public Logic(DataBase tmp)
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
            //Send set of columns in table, arguments: table name
            Commands.Add("GetColumns");
            //Send list of missions
            Commands.Add("List");
            //TODO Dodać Check Sume 
            //TODO SendOne
            //TODO MemoryCheck
            //TODO Flagowanie
            //TODO Baza
        }

        //Check command then try to do

        public string ToAnswer(string ans)
        {
            answer = "";
            return ans;
        }

        private bool Exist(string table)
        {
            var sql = "SELECT * FROM TABLES WHERE Table_Name = '" + table_name + "'";
            if (dB.Query(sql) == "") return false;
            return true;
        }

        //Cut table name off rest of arguments
        private void Divide(List<string> args)
        {
            table_name = "";
            arguments = new List<string>();

            for (var i = 0; i < args.Count; i++)
                if (i == 0)
                    table_name = args[i];
                else
                    arguments.Add(args[i]);
        }

        //Cut table name off image (in string)
        private void GetImg(List<string> args)
        {
            table_name = "";
            arguments = new List<string>();

            for (var i = 0; i < args.Count; i++)
                if (i == 0)
                    table_name = args[i];
                else if (i + 1 == args.Count)
                    IMG = args[i];
                else
                    arguments.Add(args[i]);
        }

        //Cut table name off column name (using in GetOne)
        private void GetColumnName(List<string> args)
        {
            table_name = args[0];
            column_name = args[1];
        }

        //Cut table name off time (using in ReciveUpdate)
        private void GetTime(List<string> args)
        {
            int time_0;
            table_name = args[0];
            int.TryParse(args[1], out time_0);
            time = Convert.ToUInt32(time_0);
        }

        //Create new table, arguments: table name and array of names of columns
        public bool Create(List<string> args)
        {
            Divide(args);
            try
            {
                if (!Exist(table_name))
                {
                    var structure = "";
                    var sql = "CREATE TABLE " + table_name + " (time int";
                    for (var i = 0; i < arguments.Count; i++)
                    {
                        if (i + 1 == arguments.Count) structure += arguments[i];
                        else structure += arguments[i] + " ";
                        sql += ", " + arguments[i] + " real";
                    }

                    sql += ")";

                    //Create record in table of structures of tables
                    var sql_structure = "INSERT INTO TABLES VALUES ('" + table_name + "', '" + structure + "')";
                    dB.Query(sql_structure);

                    //Create record in images table where images from satelite will be stored
                    var sql_img = "INSERT INTO IMGS VALUES ('" + table_name + "', '')";

                    answer = Answers.Create_Succesful;

                    dB.Query(sql);
                    dB.Query(sql_img);
                    return true;
                }

                answer = Answers.Exist;
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Send new data to server, arguments: table name and array of data
        public bool SendUpdate(List<string> args)
        {
            GetImg(args);
            try
            {
                //Updating data in main table of mission
                if (Exist(table_name))
                {
                    int time_0;
                    if (!int.TryParse(arguments[0], out time_0)) return false;
                    var sql = "INSERT INTO " + table_name + " VALUES (";
                    for (var i = 0; i < arguments.Count; i++)
                    {
                        sql += arguments[i];
                        if (i + 1 != arguments.Count) sql += ", ";
                    }

                    sql += ")";
                    dB.Query(sql);

                    //Updating last image in table of images
                    var sql_img = "UPDATE IMGS SET IMG = '" + IMG + "' WHERE Mission_Name = '" + table_name + "'";
                    dB.Query(sql_img);
                    answer = Answers.Succesful;
                    return true;
                }

                answer = Answers.NotExist;
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Send new data to client, arguments: table name and lastest value of time
        public bool ReciveUpdate(List<string> args)
        {
            GetTime(args);
            //try
            {
                if (Exist(table_name))
                {
                    var sql = "SELECT * FROM " + table_name + " WHERE time > " + time;
                    answer = dB.Query(sql);
                    return true;
                }

                answer = Answers.NotExist;
                return true;
            }
        }

        //Send lastest image of mission: arguments: table name
        public bool GetLastImage(List<string> args)
        {
            try
            {
                if (Exist(table_name))
                {
                    var sql = "SELECT IMG FROM IMGS WHERE Mission_Name = '" + table_name + "'";
                    answer = dB.Query(sql);
                    return true;
                }

                answer = Answers.NotExist;
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Check actuall, arguments: table name and lastest values of time
        public bool CheckTopicality(List<string> args)
        {
            GetTime(args);
            try
            {
                if (Exist(table_name))
                {
                    var sql = "SELECT * FROM " + table_name + " WHERE time > " + time;
                    if (dB.Query(sql) == "") answer = "Your data is actual";
                    else answer = "Your data is not actual";
                    return true;
                }

                answer = Answers.NotExist;
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Send lastest set of data to client, arguments: table name
        public bool GetLastest(List<string> args)
        {
            Divide(args);
            try
            {
                if (Exist(table_name))
                {
                    var sql_time = "SELECT MAX(time) FROM " + table_name;
                    var max_time = int.Parse(dB.Query(sql_time));
                    var sql = "SELECT * FROM " + table_name + " WHERE time = " + max_time;
                    answer = dB.Query(sql);
                    return true;
                }

                answer = Answers.NotExist;
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Send one category of data to client, arguments: table name, time, name of column
        public bool GetOne(List<string> args)
        {
            GetColumnName(args);
            try
            {
                if (Exist(table_name))
                {
                    var sql = "SELECT " + column_name + " FROM " + table_name;
                    answer = dB.Query(sql);
                    return true;
                }

                answer = Answers.NotExist;
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Send set of columns in table, arguments: table name
        public bool GetColumns(List<string> args)
        {
            Divide(args);
            try
            {
                if (Exist(table_name))
                {
                    var sql = "SELECT Columns FROM TABLES WHERE Table_Name = '" + table_name + "'";
                    var structure = dB.Query(sql);
                    var columns = structure.Split(' ');
                    for (var i = 0; i < columns.Length; i++) answer += columns[i] + "\n";
                    return true;
                }

                answer = Answers.NotExist;
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Send list of missions
        public bool List()
        {
            try
            {
                var sql = "SELECT Table_Name FROM TABLES";
                answer = dB.Query(sql);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}