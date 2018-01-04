#region

using System;
using System.Collections.Generic;

#endregion

namespace ServerLogic.Source
{
    internal class Logic
    {
        public static List<string> Commands = new List<string>();
        private readonly DataBase _dB;
        private List<string> _arguments;
        private string _columnName;
        private string _img;

        //global variables using in method Devide(), GetTime() or GetIMG()
        private string _tableName = "";
        private uint _time;

        //global variable using to send answer to client
        public string Answer = "";

        public Logic(DataBase tmp)
        {
            _dB = tmp;
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
            Answer = "";
            return ans;
        }

        private bool Exist(string table)
        {
            var sql = "SELECT * FROM TABLES WHERE Table_Name = '" + _tableName + "'";
            if (_dB.Query(sql) == "") return false;
            return true;
        }

        //Cut table name off rest of arguments
        private void Divide(List<string> args)
        {
            _tableName = "";
            _arguments = new List<string>();

            for (var i = 0; i < args.Count; i++)
                if (i == 0)
                    _tableName = args[i];
                else
                    _arguments.Add(args[i]);
        }

        //Cut table name off image (in string)
        private void GetImg(List<string> args)
        {
            _tableName = "";
            _arguments = new List<string>();

            for (var i = 0; i < args.Count; i++)
                if (i == 0)
                    _tableName = args[i];
                else if (i + 1 == args.Count)
                    _img = args[i];
                else
                    _arguments.Add(args[i]);
        }

        //Cut table name off column name (using in GetOne)
        private void GetColumnName(List<string> args)
        {
            _tableName = args[0];
            _columnName = args[1];
        }

        //Cut table name off time (using in ReciveUpdate)
        private void GetTime(List<string> args)
        {
            int time_0;
            _tableName = args[0];
            int.TryParse(args[1], out time_0);
            _time = Convert.ToUInt32(time_0);
        }

        //Create new table, arguments: table name and array of names of columns
        public bool Create(List<string> args)
        {
            Divide(args);
            try
            {
                if (!Exist(_tableName))
                {
                    var structure = "";
                    var sql = "CREATE TABLE " + _tableName + " (time int";
                    for (var i = 0; i < _arguments.Count; i++)
                    {
                        if (i + 1 == _arguments.Count) structure += _arguments[i];
                        else structure += _arguments[i] + " ";
                        sql += ", " + _arguments[i] + " real";
                    }

                    sql += ")";

                    //Create record in table of structures of tables
                    var sqlStructure = "INSERT INTO TABLES VALUES ('" + _tableName + "', '" + structure + "')";
                    _dB.Query(sqlStructure);

                    //Create record in images table where images from satelite will be stored
                    var sqlImg = "INSERT INTO IMGS VALUES ('" + _tableName + "', '')";

                    Answer = Answers.CreateSuccesful;

                    _dB.Query(sql);
                    _dB.Query(sqlImg);
                    return true;
                }

                Answer = Answers.Exist;
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
                if (Exist(_tableName))
                {
                    int time_0;
                    if (!int.TryParse(_arguments[0], out time_0)) return false;
                    var sql = "INSERT INTO " + _tableName + " VALUES (";
                    for (var i = 0; i < _arguments.Count; i++)
                    {
                        sql += _arguments[i];
                        if (i + 1 != _arguments.Count) sql += ", ";
                    }

                    sql += ")";
                    _dB.Query(sql);

                    //Updating last image in table of images
                    var sqlImg = "UPDATE IMGS SET IMG = '" + _img + "' WHERE Mission_Name = '" + _tableName + "'";
                    _dB.Query(sqlImg);
                    Answer = Answers.Succesful;
                    return true;
                }

                Answer = Answers.NotExist;
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
                if (Exist(_tableName))
                {
                    var sql = "SELECT * FROM " + _tableName + " WHERE time > " + _time;
                    Answer = _dB.Query(sql);
                    return true;
                }

                Answer = Answers.NotExist;
                return true;
            }
        }

        //Send lastest image of mission: arguments: table name
        public bool GetLastImage(List<string> args)
        {
            try
            {
                if (Exist(_tableName))
                {
                    var sql = "SELECT IMG FROM IMGS WHERE Mission_Name = '" + _tableName + "'";
                    Answer = _dB.Query(sql);
                    return true;
                }

                Answer = Answers.NotExist;
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
                if (Exist(_tableName))
                {
                    var sql = "SELECT * FROM " + _tableName + " WHERE time > " + _time;
                    if (_dB.Query(sql) == "") Answer = "Your data is actual";
                    else Answer = "Your data is not actual";
                    return true;
                }

                Answer = Answers.NotExist;
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
                if (Exist(_tableName))
                {
                    var sqlTime = "SELECT MAX(time) FROM " + _tableName;
                    var maxTime = int.Parse(_dB.Query(sqlTime));
                    var sql = "SELECT * FROM " + _tableName + " WHERE time = " + maxTime;
                    Answer = _dB.Query(sql);
                    return true;
                }

                Answer = Answers.NotExist;
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
                if (Exist(_tableName))
                {
                    var sql = "SELECT " + _columnName + " FROM " + _tableName;
                    Answer = _dB.Query(sql);
                    return true;
                }

                Answer = Answers.NotExist;
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
                if (Exist(_tableName))
                {
                    var sql = "SELECT Columns FROM TABLES WHERE Table_Name = '" + _tableName + "'";
                    var structure = _dB.Query(sql);
                    var columns = structure.Split(' ');
                    for (var i = 0; i < columns.Length; i++) Answer += columns[i] + "\n";
                    return true;
                }

                Answer = Answers.NotExist;
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
                Answer = _dB.Query(sql);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}