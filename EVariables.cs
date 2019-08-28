using System;

namespace ddb
{
    public class EVariables
    {
        public EVariables()
        {

            MYSQL_ADRESS = Environment.GetEnvironmentVariable("MYSQL_ADRESS") ?? "";
            MYSQL_PORT = Environment.GetEnvironmentVariable("MYSQL_PORT") ?? "3306";
            MYSQL_USERNAME = Environment.GetEnvironmentVariable("MYSQL_USERNAME") ?? "";
            MYSQL_PASSWORD = Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ?? "";
            DB_DUMP_FREQ = Environment.GetEnvironmentVariable("DB_DUMP_FREQ") ?? "1440";
            DB_DUMP_BEGIN = Environment.GetEnvironmentVariable("DB_DUMP_BEGIN") ?? "";
            MYSQL_DB_NAMES = Environment.GetEnvironmentVariable("MYSQL_DB_NAMES") ?? "";
            TZ = Environment.GetEnvironmentVariable("TZ") ?? "";
        }

        public string MYSQL_ADRESS { get; set; }
        public string MYSQL_PORT { get; set; }
        public string MYSQL_USERNAME { get; set; }
        public string MYSQL_PASSWORD { get; set; }
        public string MYSQL_DB_NAMES { get; set; }
        public string DB_DUMP_FREQ { get; set; }
        public string DB_DUMP_BEGIN { get; set; }
        public string TZ { get; set; }
    }
}