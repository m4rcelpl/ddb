using System;
using System.Collections;

namespace ddb
{
    public class EnvironmentVariables
    {
        public EnvironmentVariables()
        {
            foreach (DictionaryEntry item in Environment.GetEnvironmentVariables())
            {
                switch (item.Key.ToString())
                {
                    case "MYSQL_ADRESS":
                        MYSQL_ADRESS = item.Value.ToString();
                        break;
                    case "MYSQL_PORT":
                        MYSQL_PORT = item.Value.ToString();
                        break;
                    case "MYSQL_USERNAME":
                        MYSQL_USERNAME = item.Value.ToString();
                        break;
                    case "MYSQL_PASSWORD":
                        MYSQL_PASSWORD = item.Value.ToString();
                        break;
                    case "DB_DUMP_FREQ":
                        DB_DUMP_FREQ = item.Value.ToString();
                        break;
                    case "DB_DUMP_BEGIN":
                        DB_DUMP_BEGIN = item.Value.ToString();
                        break;
                };
            }
        }

        public string MYSQL_ADRESS { get; set; }
        public string MYSQL_PORT { get; set; }
        public string MYSQL_USERNAME { get; set; }
        public string MYSQL_PASSWORD { get; set; }   
        public string DB_DUMP_FREQ { get; set; }
        public string DB_DUMP_BEGIN { get; set; }
    }
}