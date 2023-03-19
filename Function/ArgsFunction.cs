namespace FileBackupApp.Function
{
    public class ArgsFunction
    {
        private Dictionary<string, string> argsDictionary;

        public ArgsFunction(string[] args)
        {
            argsDictionary = new Dictionary<string, string>
            {
                {"/fromDir", ""},
                {"/toDir", ""},
                {"/logDir", ""}
            };
            argsDictionary["/logDir"] = Environment.CurrentDirectory;
            MakeDictionary(args);
            CheckRequiredArgs();
        }

        private void MakeDictionary(string[] args)
        {
            if(args.Length % 2 == 0)
            {
                throw new Exception("invalid argument");
            }

            for(int i=1;i<args.Length;i+=2)
            {
                if(!argsDictionary.ContainsKey(args[i]))
                {
                    throw new Exception("invalid argument " + args[i]);
                }
                argsDictionary[args[i]] = args[i+1];
            }
        }

        private void CheckRequiredArgs()
        {
            if(argsDictionary["/fromDir"] == "" || argsDictionary["/toDir"] == "")
            {
                throw new Exception("Required argument not entered");
            }
        }

        public string GetDictionaryValue(string key)
        {
            return argsDictionary[key];
        }
    }
}