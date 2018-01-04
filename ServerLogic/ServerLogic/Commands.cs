using Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server;

namespace ServerLogic
{
  public  class Commands
    {
        static Logic logic = null;

        public Commands(DataBase dB)
        {
            logic = new Logic(dB);
        }

        public static string Do(string main)
        {
            string[] tmp = main.Split(' ');

            //Cut command off arguments
            string command = null;
            List<string> args = new List<string>();

            for (int i = 0; i < tmp.Length; i++)
            {
                if (i == 0) command = tmp[i];
                else args.Add(tmp[i]);
            }

            //In case of first word of command try to do or return error
            if (command == "Create")
            {
                if (!logic.Create(args)) return "Something went wrong";
                else return logic.ToAnswer(logic.answer);
            }

            else if (command == "SendUpdate")
            {
                if (!logic.SendUpdate(args)) return "Something went wrong";
                else return logic.ToAnswer(logic.answer);
            }

            else if (command == "ReciveUpdate")
            {
                if (!logic.ReciveUpdate(args)) return "Something went wrong";
                else return logic.ToAnswer(logic.answer);
            }

            else if (command == "GetLastImage")
            {
                if (!logic.GetLastImage(args)) return "Something went wrong";
                else return logic.ToAnswer(logic.answer);
            }

            else if (command == "CheckTopicality")
            {
                if (!logic.CheckTopicality(args)) return "Something went wrong";
                else return logic.ToAnswer(logic.answer);
            }

            else if (command == "GetLastest")
            {
                if (!logic.GetLastest(args)) return "Something went wrong";
                else return logic.ToAnswer(logic.answer);
            }

            else if (command == "GetOne")
            {
                if (!logic.GetOne(args)) return "Something went wrong";
                else return logic.ToAnswer(logic.answer);
            }

            else if (command == "GetColumns")
            {
                if (!logic.GetColumns(args)) return "Something went wrong";
                else return logic.ToAnswer(logic.answer);
            }

            else if (command == "List")
            {
                if (!logic.List()) return "Something went wrong";
                else return logic.ToAnswer(logic.answer);
            }

            else
                return "Command does not exsist";
        }
    }
}