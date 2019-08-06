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
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

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
                //Console.WriteLine(@"mysqldump -h${MYSQL_HOST} -P${MYSQL_PORT} -u${MYSQL_USER} -p${MYSQL_PASS} ${EXTRA_OPTS} \${MYSQL_DB}".Bash());
                Console.WriteLine(@"mysqldump -hdatabase.platnicyvat.pl -P3445 -uroot -pjrmgqj2xf1e24tgx -r/app/dmp.sql ${EXTRA_OPTS} \--all-databases".Bash());

            }


            while (!stoppingToken.IsCancellationRequested)
            {
                
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
