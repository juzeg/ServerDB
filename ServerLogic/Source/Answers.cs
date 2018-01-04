namespace ServerLogic.Source
{
    public static class Answers
    {
        public static readonly string Check = "\nYou can check list of missions by command 'List'";
        public static readonly string NotExist = "Mission does not exist or was deleted" + Check;
        public static readonly string Exist = "Mission is already exist" + Check;
        public static readonly string CreateSuccesful = "New mission created";
        public static readonly string Succesful = "Whatever have you done - it works";
    }
}