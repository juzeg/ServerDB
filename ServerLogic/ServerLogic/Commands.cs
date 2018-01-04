using System.Collections.Generic;
using Login;

namespace Server
{
    public class Server_Commands
    {
        private readonly Logic logic;

        public Server_Commands(DB dB)
        {
            logic = new Logic(dB);
        }

        public string Do(string main)
        {
            var tmp = main.Split(' ');

            //Cut command off arguments
            string command = null;
            var args = new List<string>();

            for (var i = 0; i < tmp.Length; i++)
                if (i == 0)
                    command = tmp[i];
                else
                    args.Add(tmp[i]);

            //In case of first word of command try to do or return error
            if (command == "Create")
                if (!logic.Create(args))
                    return "Something went wrong";
                else
                    return logic.ToAnswer(logic.answer);

            if (command == "SendUpdate")
                if (!logic.SendUpdate(args))
                    return "Something went wrong";
                else
                    return logic.ToAnswer(logic.answer);

            if (command == "ReciveUpdate")
                if (!logic.ReciveUpdate(args))
                    return "Something went wrong";
                else
                    return logic.ToAnswer(logic.answer);

            if (command == "GetLastImage")
                if (!logic.GetLastImage(args))
                    return "Something went wrong";
                else
                    return logic.ToAnswer(logic.answer);

            if (command == "CheckTopicality")
                if (!logic.CheckTopicality(args))
                    return "Something went wrong";
                else
                    return logic.ToAnswer(logic.answer);

            if (command == "GetLastest")
                if (!logic.GetLastest(args))
                    return "Something went wrong";
                else
                    return logic.ToAnswer(logic.answer);

            if (command == "GetOne")
                if (!logic.GetOne(args))
                    return "Something went wrong";
                else
                    return logic.ToAnswer(logic.answer);

            if (command == "GetColumns")
                if (!logic.GetColumns(args))
                    return "Something went wrong";
                else
                    return logic.ToAnswer(logic.answer);

            if (command == "List")
                if (!logic.List())
                    return "Something went wrong";
                else
                    return logic.ToAnswer(logic.answer);

            return "Command does not exsist";
        }
    }
}