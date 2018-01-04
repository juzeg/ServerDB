#region

using System;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;

#endregion

namespace ServerLogic.Source
{
    public class DataBase
    {
        private readonly SQLiteConnection _mDbConnection;

        public DataBase(string source)
        {
            _mDbConnection = new SQLiteConnection("Data Source=" + source + ";Version=3;");
            _mDbConnection.Open();
        }

        private string Compute(string txt)
        {
            using (var md5Hash = MD5.Create())
            {
                return GetMd5Hash(md5Hash, txt);
            }
        }

        private string GetMd5Hash(MD5 md5Hash, string input)
        {
            var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            var sBuilder = new StringBuilder();
            for (var i = 0; i < data.Length; i++) sBuilder.Append(data[i].ToString("x2"));
            return sBuilder.ToString();
        }

        private bool VerifyMd5Hash(string input, string hash)
        {
            var hashOfInput = Compute(input);
            Console.WriteLine("z veryfiy hash");
            Console.WriteLine(hashOfInput);
            Console.WriteLine(hash);
            var comparer = StringComparer.OrdinalIgnoreCase;
            if (0 == comparer.Compare(hashOfInput, hash))
                return true;
            return false;
        }

        public bool VerifyPassword(string login, string password)
        {
            password = Compute(password);
            login = login.ToUpper();
            login = Compute(login);
            var d = Query(
                "Select ID from accounts_list where login = '" + login + "' and password = '" + password + "'");
            Console.WriteLine(d);
            if (d != "")
                return true;
            return false;
        }

        public void CreateNewUser(string login, string password)
        {
            using (var md5Hash = MD5.Create())
            {
                login = login.ToUpper();
                password = Compute(password);
                login = Compute(login);
            }

            var sql = "insert into accounts_list (login,password) values ('" + login + "', '" + password + "')";
            Query("insert into accounts_list (login,password) values ('" + login + "', '" + password + "')");
        }

        public int get_ID(string login)
        {
            int ID;
            login = login.ToUpper();
            login = Compute(login);
            var sql = "select ID from accounts_list where login = '" + login + "'";
            var getId = new SQLiteCommand(sql, _mDbConnection);
            var id = getId.ExecuteReader();
            id.Read();
            ID = id.GetInt32(0);
            return ID;
        }

        public string Query(string sql)
        {
            var wynik = "";
            var i = 0;
            var command = new SQLiteCommand(sql, _mDbConnection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                while (reader.FieldCount > i)
                {
                    wynik += reader[i].ToString();
                    wynik += " ";
                    i++;
                }

                i = 0;
                wynik += Environment.NewLine;
            }

            return wynik;
        }

        public int Getsize()
        {
            var i = 0;
            var command = new SQLiteCommand("get * from structure", _mDbConnection);
            var reader = command.ExecuteReader();
            while (reader.Read()) i++;
            return i;
        }

        public int CountResults(string sql)
        {
            var i = 0;
            var command = new SQLiteCommand(sql, _mDbConnection);
            var reader = command.ExecuteReader();
            while (reader.Read()) i++;
            return i;
        }
    }
}