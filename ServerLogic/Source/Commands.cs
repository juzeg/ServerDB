using System.Collections.Generic;

namespace ServerLogic.Source
{
  public  class Commands
    {
        static Logic _logic;

        public Commands(DataBase dB)
        {
            _logic = new Logic(dB);
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
                if (!_logic.Create(args)) return "Something went wrong";
                else return _logic.ToAnswer(_logic.Answer);
            }

            else if (command == "SendUpdate")
            {
                if (!_logic.SendUpdate(args)) return "Something went wrong";
                else return _logic.ToAnswer(_logic.Answer);
            }

            else if (command == "ReciveUpdate")
            {
                if (!_logic.ReciveUpdate(args)) return "Something went wrong";
                else return _logic.ToAnswer(_logic.Answer);
            }

            else if (command == "GetLastImage")
            {
                if (!_logic.GetLastImage(args)) return "Something went wrong";
                else return _logic.ToAnswer(_logic.Answer);
            }

            else if (command == "CheckTopicality")
            {
                if (!_logic.CheckTopicality(args)) return "Something went wrong";
                else return _logic.ToAnswer(_logic.Answer);
            }

            else if (command == "GetLastest")
            {
                if (!_logic.GetLastest(args)) return "Something went wrong";
                else return _logic.ToAnswer(_logic.Answer);
            }

            else if (command == "GetOne")
            {
                if (!_logic.GetOne(args)) return "Something went wrong";
                else return _logic.ToAnswer(_logic.Answer);
            }

            else if (command == "GetColumns")
            {
                if (!_logic.GetColumns(args)) return "Something went wrong";
                else return _logic.ToAnswer(_logic.Answer);
            }

            else if (command == "List")
            {
                if (!_logic.List()) return "Something went wrong";
                else return _logic.ToAnswer(_logic.Answer);
            }

            else
                return "Command does not exsist";
        }
    }
}