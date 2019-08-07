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
            Console.WriteLine("All environment variables:");
            foreach (DictionaryEntry item in Environment.GetEnvironmentVariables())
            {
                Console.WriteLine($"{item.Key} | {item.Value}");
            }

            if ("mysqldump".Bash().Contains("command not found"))
            {
                Console.WriteLine("There is no mysqldump in system");
            }
            else
            {
                Console.WriteLine("Start making backup....");
                //Console.WriteLine(@"mysqldump -h${MYSQL_HOST} -P${MYSQL_PORT} -u${MYSQL_USER} -p${MYSQL_PASS} ${EXTRA_OPTS} \${MYSQL_DB}".Bash());
                Console.WriteLine(@"mysqldump -hdatabase.platnicyvat.pl -P3445 -uroot -pjrmgqj2xf1e24tgx ${EXTRA_OPTS} \--all-databases | gzip -9 -c > /app/backup/dmp.sql.gz".Bash());

                Console.WriteLine("Done file is save.");

            }


            while (!stoppingToken.IsCancellationRequested)
            {
                
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
