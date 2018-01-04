using System;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;

namespace Login
{
    public class DB
    {
        public SQLiteConnection m_dbConnection;

        public DB(string source)
        {
            try
            {
                m_dbConnection = new SQLiteConnection("Data Source=" + source + ";Version=3;");
                m_dbConnection.Open();
            }
            catch
            {
            }
        }

        public string Compute(string txt)
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
            var get_id = new SQLiteCommand(sql, m_dbConnection);
            var id = get_id.ExecuteReader();
            id.Read();
            ID = id.GetInt32(0);
            return ID;
        }

        public string Query(string sql)
        {
            var wynik = "";
            var i = 0;
            var command = new SQLiteCommand(sql, m_dbConnection);
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

        public int getsize()
        {
            var i = 0;
            var command = new SQLiteCommand("get * from structure", m_dbConnection);
            var reader = command.ExecuteReader();
            while (reader.Read()) i++;
            return i;
        }

        public int CountResults(string sql)
        {
            var i = 0;
            var command = new SQLiteCommand(sql, m_dbConnection);
            var reader = command.ExecuteReader();
            while (reader.Read()) i++;
            return i;
        }
    }
}