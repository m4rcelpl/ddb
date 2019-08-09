using System;
using System.Collections;

namespace ddb
{
    public class EVariables
    {
        public EVariables()
        {

            MYSQL_ADRESS = Environment.GetEnvironmentVariable("MYSQL_ADRESS");
            MYSQL_PORT = Environment.GetEnvironmentVariable("MYSQL_PORT");
            MYSQL_USERNAME = Environment.GetEnvironmentVariable("MYSQL_USERNAME");
            MYSQL_PASSWORD = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");
            DB_DUMP_FREQ = Environment.GetEnvironmentVariable("DB_DUMP_FREQ");
            DB_DUMP_BEGIN = Environment.GetEnvironmentVariable("DB_DUMP_BEGIN");
        }

        public string MYSQL_ADRESS { get; set; }
        public string MYSQL_PORT { get; set; }
        public string MYSQL_USERNAME { get; set; }
        public string MYSQL_PASSWORD { get; set; }
        public string DB_DUMP_FREQ { get; set; }
        public string DB_DUMP_BEGIN { get; set; }
    }
}