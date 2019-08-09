using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ddb
{
    public class Worker : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            EVariables eVariables = new EVariables();
            string filename = DateTime.Now.ToString("ddMMyyyy_HHmmss");

            Console.WriteLine($"MYSQL_ADRESS: {eVariables.MYSQL_ADRESS}{Environment.NewLine}MYSQL_PORT: {eVariables.MYSQL_PORT}{Environment.NewLine}MYSQL_USERNAME: {eVariables.MYSQL_USERNAME}{Environment.NewLine}MYSQL_PASSWORD: {eVariables.MYSQL_PASSWORD}{Environment.NewLine}DB_DUMP_BEGIN: {eVariables.DB_DUMP_BEGIN}{Environment.NewLine}DB_DUMP_FREQ: {eVariables.DB_DUMP_FREQ}");

            Int32.TryParse(eVariables.DB_DUMP_FREQ, out int DB_DUMP_FREQ);


            try
            {
                if ("mysqldump".Bash().Contains("command not found"))
                {
                    Console.WriteLine("There is no mysqldump in system");
                    Environment.Exit(-1);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine($"Error while searching for mysqldump: {ex.Message} | {ex.InnerException?.Message}");
                Environment.Exit(-1);
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(DB_DUMP_FREQ * 1000, stoppingToken);

                try
                {
                    Console.WriteLine("Start making backup....");
                    Console.WriteLine($"mysqldump -h{eVariables.MYSQL_ADRESS} -P{eVariables.MYSQL_PORT} -u{eVariables.MYSQL_USERNAME} -p{eVariables.MYSQL_PASSWORD} --all-databases | gzip -9 -c > /app/backup/{filename}.sql.gz".Bash());
                    Console.WriteLine($"Done file: {filename} is save");
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine($"Error while making backup: {ex.Message} | {ex.InnerException?.Message}");
                }

            }
        }
    }
}