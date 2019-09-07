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
            MYSQL_DB_NAMES = Environment.GetEnvironmentVariable("MYSQL_DB_NAMES") ?? "";
            DB_DUMP_BEGIN = Environment.GetEnvironmentVariable("DB_DUMP_BEGIN") ?? "";
            MYSQL_EXTRA_OPTION = Environment.GetEnvironmentVariable("MYSQL_EXTRA_OPTION") ?? "";

            if (Int32.TryParse(Environment.GetEnvironmentVariable("DB_DUMP_FREQ") ?? "", out int db_dump_freq))
                DB_DUMP_FREQ = db_dump_freq;
            else
                DB_DUMP_FREQ = 1440;

            if (Int32.TryParse(Environment.GetEnvironmentVariable("FILES_TO_KEEP") ?? "", out int file_to_keep))
                FILES_TO_KEEP = file_to_keep;
            else
                FILES_TO_KEEP = 0;

            TZ = Environment.GetEnvironmentVariable("TZ") ?? "";
        }

        public string MYSQL_ADRESS { get; set; }
        public string MYSQL_PORT { get; set; }
        public string MYSQL_USERNAME { get; set; }
        public string MYSQL_PASSWORD { get; set; }
        public string MYSQL_DB_NAMES { get; set; }
        public string MYSQL_EXTRA_OPTION { get; set; }
        public int DB_DUMP_FREQ { get; set; }
        public string DB_DUMP_BEGIN { get; set; }
        public int FILES_TO_KEEP { get; set; }
        public string TZ { get; set; }
    }
}