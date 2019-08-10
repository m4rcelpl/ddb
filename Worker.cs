using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace ddb
{
    public class Worker : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            EVariables eVariables = new EVariables();
            StringBuilder filename = new StringBuilder();
            StringBuilder command = new StringBuilder();

            Console.WriteLine($"MYSQL_ADRESS: {eVariables.MYSQL_ADRESS}{Environment.NewLine}MYSQL_PORT: {eVariables.MYSQL_PORT}{Environment.NewLine}MYSQL_USERNAME: {eVariables.MYSQL_USERNAME}{Environment.NewLine}MYSQL_PASSWORD: (***)🔐{Environment.NewLine}DB_DUMP_BEGIN: {eVariables.DB_DUMP_BEGIN}{Environment.NewLine}DB_DUMP_FREQ: {eVariables.DB_DUMP_FREQ}");

            Int32.TryParse(eVariables.DB_DUMP_FREQ, out int DB_DUMP_FREQ);
            if (DB_DUMP_FREQ <= 0)
                DB_DUMP_FREQ = 1;

            command.Append("mysqldump");
            try
            {
                if (command.Bash().Contains("command not found"))
                {
                    Console.WriteLine("[ERROR] 🤔 There is no mysqldump in system");
                    return;
                }

                Console.WriteLine("[INFO] 😊 Mysqldump... Found");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] 🤔 While searching for mysqldump: {ex.Message} | {ex.InnerException?.Message}");
                return;
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine($"[INFO] ⏱ Next backup is set to: {DateTime.Now.AddMilliseconds(DB_DUMP_FREQ * 60000)}");

                await Task.Delay(DB_DUMP_FREQ * 60000, stoppingToken);

                filename.Clear();
                filename.Append(DateTime.Now.ToString("ddMMyyyy_HHmmss"));
                command.Clear();
                command.Append($"mysqldump -h{eVariables.MYSQL_ADRESS} -P{eVariables.MYSQL_PORT} -u{eVariables.MYSQL_USERNAME} -p{eVariables.MYSQL_PASSWORD} --all-databases | gzip -9 -c > /app/backup/{filename}.sql.gz");

                try
                {
                    Console.WriteLine("[INFO] 🐱‍👤 Start making backup...");
                    Console.WriteLine(command.Bash());
                    if (File.Exists($"/app/backup/{filename}.sql.gz"))
                    {
                        Console.WriteLine($"[INFO] 💾 Files is save in: /app/backup/{filename}.sql.gz");
                    }
                    else
                    {
                        Console.WriteLine($"[ERROR] 🤔 Something went wrong. File not found.");
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ERROR] 🤔 While making backup: {ex.Message} | {ex.InnerException?.Message}");
                }

            }
        }
    }
}